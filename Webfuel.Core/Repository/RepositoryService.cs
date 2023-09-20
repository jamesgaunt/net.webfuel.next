#define SQL_DEBUG

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace Webfuel
{
    public interface IRepositoryService
    {
        Task<object?> ExecuteScalarAsync(string sql, IEnumerable<SqlParameter>? parameters = null, CancellationToken? cancellationToken = null);

        Task<int> ExecuteNonQueryAsync(string sql, IEnumerable<SqlParameter>? parameters = null, CancellationToken? cancellationToken = null);

        Task<List<TEntity>> ExecuteReaderAsync<TEntity>(string sql, IEnumerable<SqlParameter>? parameters = null, CancellationToken? cancellationToken = null) where TEntity : class;

        Task<TEntity> ExecuteInsertAsync<TEntity>(TEntity entity, IEnumerable<string>? properties = null, CancellationToken? cancellationToken = null) where TEntity : class;

        Task<TEntity> ExecuteUpdateAsync<TEntity>(TEntity entity, IEnumerable<string>? properties = null, CancellationToken? cancellationToken = null) where TEntity : class;

        Task ExecuteDeleteAsync<TEntity>(object key, CancellationToken? cancellationToken = null) where TEntity : class;
    }

    [ServiceImplementation(typeof(IRepositoryService))]
    internal class RepositoryService : IRepositoryService
    {
        private readonly IServiceProvider ServiceProvider;
        private readonly IRepositoryConfiguration RepositoryConfiguration;
        private readonly ITenantAccessor TenantAccessor;

        public RepositoryService(
            IServiceProvider serviceProvider, 
            IRepositoryConfiguration repositoryConfiguration, 
            ITenantAccessor tenantAccessor)
        {
            ServiceProvider = serviceProvider;
            RepositoryConfiguration = repositoryConfiguration;
            TenantAccessor = tenantAccessor;
        }

        public async Task<object?> ExecuteScalarAsync(string sql, IEnumerable<SqlParameter>? parameters = null, CancellationToken? cancellationToken = null)
        {
            for (int retryCount = 0; retryCount < TransientErrorRetryCount; retryCount++)
            {
                try
                {
                    using (var connection = OpenSqlConnection())
                    using (var command = BuildSqlCommand(connection, sql, parameters))
                        return cancellationToken == null ? await command.ExecuteScalarAsync() : await command.ExecuteScalarAsync(cancellationToken.Value);
                }
                catch (Exception ex)
                {
                    var retryDelay = TransientErrorRetryDelay(ex, retryCount);
                    if (retryDelay == 0)
                        throw;
                    await Task.Delay(retryDelay);
                }
            }
            throw new InvalidOperationException("Maximum SQL Retry Count Exceeded");
        }

        public async Task<int> ExecuteNonQueryAsync(string sql, IEnumerable<SqlParameter>? parameters = null, CancellationToken? cancellationToken = null)
        {
            for (int retryCount = 0; retryCount < TransientErrorRetryCount; retryCount++)
            {
                try
                {
                    using (var connection = OpenSqlConnection())
                    using (var command = BuildSqlCommand(connection, sql, parameters))
                        return cancellationToken == null ? await command.ExecuteNonQueryAsync() : await command.ExecuteNonQueryAsync(cancellationToken.Value);
                }
                catch (Exception ex)
                {
                    var retryDelay = TransientErrorRetryDelay(ex, retryCount);
                    if (retryDelay == 0)
                        throw;
                    await Task.Delay(retryDelay);
                }
            }
            throw new InvalidOperationException("Maximum SQL Retry Count Exceeded");
        }

        public async Task<List<TEntity>> ExecuteReaderAsync<TEntity>(string sql, IEnumerable<SqlParameter>? parameters = null, CancellationToken? cancellationToken = null) where TEntity : class
        {

            var mapper = GetMapper<TEntity>();
            for (int retryCount = 0; retryCount < TransientErrorRetryCount; retryCount++)
            {
                try
                {
                    using (var connection = OpenSqlConnection())
                    using (var command = BuildSqlCommand(connection, sql, parameters))
                    using (var dr = cancellationToken == null ? await command.ExecuteReaderAsync() : await command.ExecuteReaderAsync(cancellationToken.Value))
                    {
                        List<TEntity> result = new List<TEntity>();
                        while (await dr.ReadAsync())
                            result.Add((TEntity)mapper.ActivateEntity(dr));
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    var retryDelay = TransientErrorRetryDelay(ex, retryCount);
                    if (retryDelay == 0)
                        throw;
                    await Task.Delay(retryDelay);
                }
            }
            throw new InvalidOperationException("Maximum SQL Retry Count Exceeded");
        }

        public async Task<TEntity> ExecuteInsertAsync<TEntity>(TEntity entity, IEnumerable<string>? properties = null, CancellationToken? cancellationToken = null) where TEntity : class
        {
            return await GetMapper<TEntity>().ExecuteInsertAsync(this, entity, properties, cancellationToken);
        }

        public async Task<TEntity> ExecuteUpdateAsync<TEntity>(TEntity entity, IEnumerable<string>? properties = null, CancellationToken? cancellationToken = null) where TEntity : class
        {
            return await GetMapper<TEntity>().ExecuteUpdateAsync(this, entity, properties, cancellationToken);
        }

        public async Task ExecuteDeleteAsync<TEntity>(object key, CancellationToken? cancellationToken = null) where TEntity : class
        {
            await GetMapper<TEntity>().ExecuteDeleteAsync(this, key, cancellationToken);
        }

        // Transient Error Retry Logic

        private readonly int TransientErrorRetryCount = 5;

        private readonly int[] TransientErrorNumbers = { 4060, 10928, 10929, 40197, 40501, 40613 };

        private int[] TransientErrorRetryDelays = { 500, 5000, 10000, 20000, 30000, 30000, 30000, 30000 };

        private int TransientErrorRetryDelay(Exception ex, int retryCount)
        {
            var sqlEx = ex as SqlException;
            if (sqlEx == null)
                return 0;

            if (!TransientErrorNumbers.Contains(sqlEx.Number))
                return 0;

            if (retryCount >= TransientErrorRetryCount)
                return 0;

            return TransientErrorRetryDelays[retryCount];
        }

        // Private Helpers

        SqlConnection OpenSqlConnection()
        {
            var tenant = TenantAccessor.Tenant;
            var connectionString = RepositoryConfiguration.ConnectionString + $"User ID=login_{tenant.DatabaseSchema};Password={tenant.DatabasePassword}";
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        SqlCommand BuildSqlCommand(SqlConnection connection, string sql, IEnumerable<SqlParameter>? parameters = null)
        {
            var command = new SqlCommand(sql, connection);
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                    command.Parameters.Add(parameter);
            }
            return command;
        }

        IRepositoryMapper<TEntity> GetMapper<TEntity>() where TEntity : class
        {
            return ServiceProvider.GetRequiredService<IRepositoryMapper<TEntity>>();
        }
    }
}
