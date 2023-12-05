using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Common
{
    internal partial interface IErrorLogRepository
    {
        Task<ErrorLog> InsertErrorLog(ErrorLog entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<ErrorLog> UpdateErrorLog(ErrorLog entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<ErrorLog> UpdateErrorLog(ErrorLog updated, ErrorLog original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteErrorLog(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<ErrorLog>> QueryErrorLog(Query query, bool selectItems = true, bool countTotal = true);
        Task<ErrorLog?> GetErrorLog(Guid id);
        Task<ErrorLog> RequireErrorLog(Guid id);
        Task<int> CountErrorLog();
        Task<List<ErrorLog>> SelectErrorLog();
        Task<List<ErrorLog>> SelectErrorLogWithPage(int skip, int take);
        Task<List<ErrorLog>> SelectErrorLogByEntityId(Guid entityId);
    }
    [Service(typeof(IErrorLogRepository))]
    internal partial class ErrorLogRepository: IErrorLogRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public ErrorLogRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<ErrorLog> InsertErrorLog(ErrorLog entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            ErrorLogMetadata.Validate(entity);
            var sql = ErrorLogMetadata.InsertSQL();
            var parameters = ErrorLogMetadata.ExtractParameters(entity, ErrorLogMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<ErrorLog> UpdateErrorLog(ErrorLog entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            ErrorLogMetadata.Validate(entity);
            var sql = ErrorLogMetadata.UpdateSQL();
            var parameters = ErrorLogMetadata.ExtractParameters(entity, ErrorLogMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<ErrorLog> UpdateErrorLog(ErrorLog updated, ErrorLog original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateErrorLog(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteErrorLog(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = ErrorLogMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<ErrorLog>> QueryErrorLog(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<ErrorLog, ErrorLogMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<ErrorLog?> GetErrorLog(Guid id)
        {
            var sql = @"SELECT * FROM [ErrorLog] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<ErrorLog, ErrorLogMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<ErrorLog> RequireErrorLog(Guid id)
        {
            return await GetErrorLog(id) ?? throw new InvalidOperationException("The specified ErrorLog does not exist");
        }
        public async Task<int> CountErrorLog()
        {
            var sql = @"SELECT COUNT(Id) FROM [ErrorLog]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<ErrorLog>> SelectErrorLog()
        {
            var sql = @"SELECT * FROM [ErrorLog] ORDER BY Id DESC";
            return await _connection.ExecuteReader<ErrorLog, ErrorLogMetadata>(sql);
        }
        public async Task<List<ErrorLog>> SelectErrorLogWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [ErrorLog] ORDER BY Id DESC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<ErrorLog, ErrorLogMetadata>(sql, parameters);
        }
        public async Task<List<ErrorLog>> SelectErrorLogByEntityId(Guid entityId)
        {
            var sql = @"SELECT * FROM [ErrorLog] WHERE EntityId = @EntityId ORDER BY Id DESC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@EntityId", entityId),
            };
            return await _connection.ExecuteReader<ErrorLog, ErrorLogMetadata>(sql, parameters);
        }
    }
}

