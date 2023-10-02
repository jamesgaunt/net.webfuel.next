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

namespace Webfuel.Repository.New
{
    public interface IRepositoryConnection
    {
        Task<object> ExecuteScalar(string sql, IEnumerable<SqlParameter>? parameters = null, CancellationToken? cancellationToken = null);

        Task<int> ExecuteNonQuery(string sql, IEnumerable<SqlParameter>? parameters = null, CancellationToken? cancellationToken = null);

        Task<List<TEntity>> ExecuteReader<TEntity>(Func<SqlDataReader, TEntity> activator, string sql, IEnumerable<SqlParameter>? parameters = null, CancellationToken? cancellationToken = null);
    }

    internal class RepositoryConnection: IRepositoryConnection
    {
        private readonly string _connectionString;

        internal RepositoryConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Task<object> ExecuteScalar(string sql, IEnumerable<SqlParameter>? parameters = null, CancellationToken? cancellationToken = null)
        {
            using (var connection = OpenConnection())
            using (var command = BuildCommand(connection, sql, parameters))
            {
                return command.ExecuteScalarAsync(cancellationToken ?? CancellationToken.None);
            }
        }

        public Task<int> ExecuteNonQuery(string sql, IEnumerable<SqlParameter>? parameters = null, CancellationToken? cancellationToken = null)
        {
            using (var connection = OpenConnection())
            using (var command = BuildCommand(connection, sql, parameters))
            {
                return command.ExecuteNonQueryAsync(cancellationToken ?? CancellationToken.None);
            }
        }

        public async Task<List<TEntity>> ExecuteReader<TEntity>(Func<SqlDataReader, TEntity> activator, string sql, IEnumerable<SqlParameter>? parameters = null, CancellationToken? cancellationToken = null)
        {
            using (var connection = OpenConnection())
            using (var command = BuildCommand(connection, sql, parameters))
            using (var dr = await command.ExecuteReaderAsync(cancellationToken ?? CancellationToken.None))
            {
                List<TEntity> result = new List<TEntity>();
                while (await dr.ReadAsync())
                    result.Add(activator(dr));
                return result;
            }
        }

        internal async Task ExecuteCommands(IEnumerable<RepositoryCommand> commands, CancellationToken? cancellationToken = null)
        {
            using (var connection = OpenConnection())
            using (var transaction = connection.BeginTransaction())
            {
                foreach(var command in commands)
                {
                    using (var sqlCommand = BuildCommand(connection, command.Sql, command.Parameters))
                    {
                        sqlCommand.Transaction = transaction;
                        await sqlCommand.ExecuteNonQueryAsync(cancellationToken ?? CancellationToken.None);
                    }
                    if(cancellationToken.HasValue && cancellationToken.Value.IsCancellationRequested)
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
