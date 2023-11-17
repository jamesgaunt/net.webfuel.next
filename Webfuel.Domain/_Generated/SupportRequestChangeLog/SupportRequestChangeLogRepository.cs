using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain
{
    internal partial interface ISupportRequestChangeLogRepository
    {
        Task<SupportRequestChangeLog> InsertSupportRequestChangeLog(SupportRequestChangeLog entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<SupportRequestChangeLog> UpdateSupportRequestChangeLog(SupportRequestChangeLog entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<SupportRequestChangeLog> UpdateSupportRequestChangeLog(SupportRequestChangeLog updated, SupportRequestChangeLog original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteSupportRequestChangeLog(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<SupportRequestChangeLog>> QuerySupportRequestChangeLog(Query query);
        Task<SupportRequestChangeLog?> GetSupportRequestChangeLog(Guid id);
        Task<SupportRequestChangeLog> RequireSupportRequestChangeLog(Guid id);
        Task<int> CountSupportRequestChangeLog();
        Task<List<SupportRequestChangeLog>> SelectSupportRequestChangeLog();
        Task<List<SupportRequestChangeLog>> SelectSupportRequestChangeLogWithPage(int skip, int take);
        Task<List<SupportRequestChangeLog>> SelectSupportRequestChangeLogBySupportRequestId(Guid supportRequestId);
    }
    [Service(typeof(ISupportRequestChangeLogRepository))]
    internal partial class SupportRequestChangeLogRepository: ISupportRequestChangeLogRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public SupportRequestChangeLogRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<SupportRequestChangeLog> InsertSupportRequestChangeLog(SupportRequestChangeLog entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            SupportRequestChangeLogMetadata.Validate(entity);
            var sql = SupportRequestChangeLogMetadata.InsertSQL();
            var parameters = SupportRequestChangeLogMetadata.ExtractParameters(entity, SupportRequestChangeLogMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<SupportRequestChangeLog> UpdateSupportRequestChangeLog(SupportRequestChangeLog entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            SupportRequestChangeLogMetadata.Validate(entity);
            var sql = SupportRequestChangeLogMetadata.UpdateSQL();
            var parameters = SupportRequestChangeLogMetadata.ExtractParameters(entity, SupportRequestChangeLogMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<SupportRequestChangeLog> UpdateSupportRequestChangeLog(SupportRequestChangeLog updated, SupportRequestChangeLog original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateSupportRequestChangeLog(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteSupportRequestChangeLog(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = SupportRequestChangeLogMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<SupportRequestChangeLog>> QuerySupportRequestChangeLog(Query query)
        {
            return await _connection.ExecuteQuery<SupportRequestChangeLog, SupportRequestChangeLogMetadata>(query);
        }
        public async Task<SupportRequestChangeLog?> GetSupportRequestChangeLog(Guid id)
        {
            var sql = @"SELECT * FROM [SupportRequestChangeLog] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<SupportRequestChangeLog, SupportRequestChangeLogMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<SupportRequestChangeLog> RequireSupportRequestChangeLog(Guid id)
        {
            return await GetSupportRequestChangeLog(id) ?? throw new InvalidOperationException("The specified SupportRequestChangeLog does not exist");
        }
        public async Task<int> CountSupportRequestChangeLog()
        {
            var sql = @"SELECT COUNT(Id) FROM [SupportRequestChangeLog]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<SupportRequestChangeLog>> SelectSupportRequestChangeLog()
        {
            var sql = @"SELECT * FROM [SupportRequestChangeLog] ORDER BY Id DESC";
            return await _connection.ExecuteReader<SupportRequestChangeLog, SupportRequestChangeLogMetadata>(sql);
        }
        public async Task<List<SupportRequestChangeLog>> SelectSupportRequestChangeLogWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [SupportRequestChangeLog] ORDER BY Id DESC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<SupportRequestChangeLog, SupportRequestChangeLogMetadata>(sql, parameters);
        }
        public async Task<List<SupportRequestChangeLog>> SelectSupportRequestChangeLogBySupportRequestId(Guid supportRequestId)
        {
            var sql = @"SELECT * FROM [SupportRequestChangeLog] WHERE SupportRequestId = @SupportRequestId ORDER BY Id DESC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@SupportRequestId", supportRequestId),
            };
            return await _connection.ExecuteReader<SupportRequestChangeLog, SupportRequestChangeLogMetadata>(sql, parameters);
        }
    }
}

