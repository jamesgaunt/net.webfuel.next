#define SQL_DEBUG

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Webfuel
{
    public interface IRepositoryService
    {
        Task<object?> ExecuteScalarAsync(string spanName, string sql, IEnumerable<SqlParameter>? parameters = null);

        Task<int> ExecuteNonQueryAsync(string spanName, string sql, IEnumerable<SqlParameter>? parameters = null);

        Task<List<TEntity>> ExecuteReaderAsync<TEntity>(string spanName, string sql, IEnumerable<SqlParameter>? parameters = null) where TEntity : class;

        Task<TEntity> ExecuteInsertAsync<TEntity>(string spanName, TEntity entity, IEnumerable<string>? properties = null) where TEntity : class;

        Task<TEntity> ExecuteUpdateAsync<TEntity>(string spanName, TEntity entity, IEnumerable<string>? properties = null) where TEntity : class;

        Task ExecuteDeleteAsync<TEntity>(string spanName, object key) where TEntity : class;
    }

    [ServiceImplementation(typeof(IRepositoryService))]
    internal class RepositoryService : IRepositoryService
    {
        private readonly IServiceProvider ServiceProvider;
        private readonly IRepositoryConfiguration RepositoryConfiguration;

        public RepositoryService(IServiceProvider serviceProvider, IRepositoryConfiguration repositoryConfiguration)
        {
            ServiceProvider = serviceProvider;
            RepositoryConfiguration = repositoryConfiguration;
        }

        public async Task<object?> ExecuteScalarAsync(string spanName, string sql, IEnumerable<SqlParameter>? parameters = null)
        {
            // using (var span = Log.Segment("SQL", BuildSegmentName(spanName, sql, parameters)))
            {
                // span.DbStatement = sql;

                for (int retryCount = 0; retryCount < TransientErrorRetryCount; retryCount++)
                {
                    try
                    {
                        using (var connection = OpenSqlConnection())
                        using (var command = BuildSqlCommand(connection, sql, parameters))
                            return await command.ExecuteScalarAsync();
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
        }

        public async Task<int> ExecuteNonQueryAsync(string spanName, string sql, IEnumerable<SqlParameter>? parameters = null)
        {
            // using (var span = Log.Segment("SQL", BuildSegmentName(spanName, sql, parameters)))
            {
                // span.DbStatement = sql;

                for (int retryCount = 0; retryCount < TransientErrorRetryCount; retryCount++)
                {
                    try
                    {
                        using (var connection = OpenSqlConnection())
                        using (var command = BuildSqlCommand(connection, sql, parameters))
                            return await command.ExecuteNonQueryAsync();
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

        }

        public async Task<List<TEntity>> ExecuteReaderAsync<TEntity>(string spanName, string sql, IEnumerable<SqlParameter>? parameters = null) where TEntity : class
        {
            // using (var span = Log.Segment("SQL", BuildSegmentName(spanName, sql, parameters)))
            {
                // span.DbStatement = sql;

                var mapper = GetMapper<TEntity>();
                for (int retryCount = 0; retryCount < TransientErrorRetryCount; retryCount++)
                {
                    try
                    {
                        using (var connection = OpenSqlConnection())
                        using (var command = BuildSqlCommand(connection, sql, parameters))
                        using (var dr = await command.ExecuteReaderAsync())
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
        }

        public async Task<TEntity> ExecuteInsertAsync<TEntity>(string spanName, TEntity entity, IEnumerable<string>? properties = null) where TEntity : class
        {
            return await GetMapper<TEntity>().ExecuteInsertAsync(spanName, this, entity, properties);
        }

        public async Task<TEntity> ExecuteUpdateAsync<TEntity>(string spanName, TEntity entity, IEnumerable<string>? properties = null) where TEntity : class
        {
            return await GetMapper<TEntity>().ExecuteUpdateAsync(spanName, this, entity, properties);
        }

        public async Task ExecuteDeleteAsync<TEntity>(string spanName, object key) where TEntity : class
        {
            await GetMapper<TEntity>().ExecuteDeleteAsync(spanName, this, key);
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
            var connection = new SqlConnection(RepositoryConfiguration.ConnectionString);
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

        string BuildSegmentName(string spanName, string sql, IEnumerable<SqlParameter>? parameters = null)
        {
#if !SQL_DEBUG
            return spanName;
#else
            var sb = new StringBuilder();
            sb.Append(spanName).Append(" ");

            sb.Append(sql).Append(" ");

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    sb.Append(parameter.ParameterName).Append(": ").Append(FormatParameterValue(parameter.Value)).Append(", ");
                }
            }

            return sb.ToString();
#endif
        }

        string FormatParameterValue(object? value)
        {
            if (value == null)
                return "NULL";

            var s = value.ToString();
            if (s == null)
                return "NULL";

            if (s.Length > 32)
                s = s.Substring(0, 32);
            return s;
        }
    }
}
