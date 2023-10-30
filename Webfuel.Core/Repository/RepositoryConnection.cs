using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Webfuel
{
    public interface IRepositoryConnection
    {
        Task ExecuteNonQuery(
            string sql,
            IEnumerable<SqlParameter>? parameters = null,
            RepositoryCommandBuffer? commandBuffer = null,
            CancellationToken? cancellationToken = null);

        Task<object> ExecuteScalar(
            string sql,
            IEnumerable<SqlParameter>? parameters = null,
            CancellationToken? cancellationToken = null);

        Task<List<TEntity>> ExecuteReader<TEntity, TEntityMetadata>(
            string sql,
            IEnumerable<SqlParameter>? parameters = null,
            CancellationToken? cancellationToken = null)
            where TEntity : class
            where TEntityMetadata : IRepositoryMetadata<TEntity>;

        Task<QueryResult<TEntity>> ExecuteQuery<TEntity, TEntityMetadata>(Query query)
            where TEntity : class
            where TEntityMetadata : IRepositoryMetadata<TEntity>;
    }

    [Service(typeof(IRepositoryConnection))]
    internal class RepositoryConnection : IRepositoryConnection
    {
        private readonly string _connectionString;

        public RepositoryConnection(ITenantAccessor tenantAccessor, IRepositoryConfiguration repositoryConfiguration)
        {
            var tenant = tenantAccessor.Tenant;
            _connectionString = repositoryConfiguration.ConnectionString + $"User ID={tenant.DatabaseLogin};Password={tenant.DatabasePassword}";
        }

        public async Task ExecuteNonQuery(
            string sql,
            IEnumerable<SqlParameter>? parameters = null,
            RepositoryCommandBuffer? commandBuffer = null,
            CancellationToken? cancellationToken = null)
        {
            if (commandBuffer != null)
            {
                commandBuffer.Connection = this;
                commandBuffer.AddCommand(sql, parameters);
            }
            else
            {
                using (var connection = OpenConnection())
                using (var command = BuildCommand(connection, sql, parameters))
                {
                    await command.ExecuteNonQueryAsync(cancellationToken ?? CancellationToken.None);
                }
            }
        }

        public async Task<object> ExecuteScalar(string sql, IEnumerable<SqlParameter>? parameters = null, CancellationToken? cancellationToken = null)
        {
            using (var connection = OpenConnection())
            using (var command = BuildCommand(connection, sql, parameters))
            {
                return await command.ExecuteScalarAsync(cancellationToken ?? CancellationToken.None);
            }
        }

        public async Task<List<TEntity>> ExecuteReader<TEntity, TEntityMetadata>(string sql, IEnumerable<SqlParameter>? parameters = null, CancellationToken? cancellationToken = null)
            where TEntity : class
            where TEntityMetadata : IRepositoryMetadata<TEntity>
        {
            using (var connection = OpenConnection())
            using (var command = BuildCommand(connection, sql, parameters))
            using (var dr = await command.ExecuteReaderAsync(cancellationToken ?? CancellationToken.None))
            {
                List<TEntity> result = new List<TEntity>();
                while (await dr.ReadAsync())
                    result.Add(TEntityMetadata.DataReader(dr));
                return result;
            }
        }

        public async Task<QueryResult<TEntity>> ExecuteQuery<TEntity, TEntityMetadata>(Query query)
            where TEntity : class
            where TEntityMetadata : IRepositoryMetadata<TEntity>
        {
            var fields = TEntityMetadata.SelectProperties;

            RepositoryQueryUtility.ValidateFields(query, fields);

            var parameters = new List<RepositoryQueryParameter>();
            var filterSql = RepositoryQueryUtility.FilterSql(query, parameters);

            var selectSql = RepositoryQueryUtility.SelectSql(query, fields);
            var fromSql = $"FROM [{TEntityMetadata.DatabaseTable}]";
            var orderSql = RepositoryQueryUtility.OrderSql(query, fields, TEntityMetadata.DefaultOrderBy);
            var pageSql = RepositoryQueryUtility.PageSql(query);

            var querySql = $"{selectSql} {fromSql} {filterSql} {orderSql} {pageSql}";

            var items = await ExecuteReader<TEntity, TEntityMetadata>(querySql, RepositoryQueryUtility.SqlParameters(parameters));
            int? totalCount = null;

            if (!String.IsNullOrEmpty(pageSql))
            {
                var countSql = RepositoryQueryUtility.CountSql(query, fields);
                querySql = $"{countSql} {fromSql} {filterSql}";
                totalCount = (int)(await ExecuteScalar(querySql, RepositoryQueryUtility.SqlParameters(parameters)))!;
            }

            return new QueryResult<TEntity>(items, totalCount);
        }

        internal async Task ExecuteCommands(IEnumerable<RepositoryCommand> commands, CancellationToken? cancellationToken = null)
        {
            using (var connection = OpenConnection())
            using (var transaction = connection.BeginTransaction())
            {
                foreach (var command in commands)
                {
                    using (var sqlCommand = BuildCommand(connection, command.Sql, command.Parameters))
                    {
                        sqlCommand.Transaction = transaction;
                        await sqlCommand.ExecuteNonQueryAsync(cancellationToken ?? CancellationToken.None);
                    }
                    if (cancellationToken.HasValue && cancellationToken.Value.IsCancellationRequested)
                    {
                        await transaction.RollbackAsync();
                        return;
                    }
                }
                await transaction.CommitAsync();
            }
        }

        SqlConnection OpenConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.RetryLogicProvider = RetryLogicProvider;
            connection.Open();
            return connection;
        }

        SqlCommand BuildCommand(SqlConnection connection, string sql, IEnumerable<SqlParameter>? parameters = null)
        {
            var command = new SqlCommand(sql, connection);
            command.RetryLogicProvider = RetryLogicProvider;
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                    command.Parameters.Add(parameter);
            }
            return command;
        }

        static SqlRetryLogicBaseProvider RetryLogicProvider = SqlConfigurableRetryFactory.CreateExponentialRetryProvider(new SqlRetryLogicOption
        {
            NumberOfTries = 5,
            DeltaTime = TimeSpan.FromSeconds(1),
            MaxTimeInterval = TimeSpan.FromSeconds(20),
        });
    }
}
