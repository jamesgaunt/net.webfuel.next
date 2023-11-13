using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Common
{
    internal partial interface IActivityLogRepository
    {
        Task<ActivityLog> InsertActivityLog(ActivityLog entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<ActivityLog> UpdateActivityLog(ActivityLog entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<ActivityLog> UpdateActivityLog(ActivityLog updated, ActivityLog original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteActivityLog(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<ActivityLog>> QueryActivityLog(Query query);
        Task<ActivityLog?> GetActivityLog(Guid id);
        Task<ActivityLog> RequireActivityLog(Guid id);
        Task<int> CountActivityLog();
        Task<List<ActivityLog>> SelectActivityLog();
        Task<List<ActivityLog>> SelectActivityLogWithPage(int skip, int take);
        Task<List<ActivityLog>> SelectActivityLogByEntityId(Guid entityId);
    }
    [Service(typeof(IActivityLogRepository))]
    internal partial class ActivityLogRepository: IActivityLogRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public ActivityLogRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<ActivityLog> InsertActivityLog(ActivityLog entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            ActivityLogMetadata.Validate(entity);
            var sql = ActivityLogMetadata.InsertSQL();
            var parameters = ActivityLogMetadata.ExtractParameters(entity, ActivityLogMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<ActivityLog> UpdateActivityLog(ActivityLog entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            ActivityLogMetadata.Validate(entity);
            var sql = ActivityLogMetadata.UpdateSQL();
            var parameters = ActivityLogMetadata.ExtractParameters(entity, ActivityLogMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<ActivityLog> UpdateActivityLog(ActivityLog updated, ActivityLog original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateActivityLog(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteActivityLog(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = ActivityLogMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<ActivityLog>> QueryActivityLog(Query query)
        {
            return await _connection.ExecuteQuery<ActivityLog, ActivityLogMetadata>(query);
        }
        public async Task<ActivityLog?> GetActivityLog(Guid id)
        {
            var sql = @"SELECT * FROM [ActivityLog] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<ActivityLog, ActivityLogMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<ActivityLog> RequireActivityLog(Guid id)
        {
            return await GetActivityLog(id) ?? throw new InvalidOperationException("The specified ActivityLog does not exist");
        }
        public async Task<int> CountActivityLog()
        {
            var sql = @"SELECT COUNT(Id) FROM [ActivityLog]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<ActivityLog>> SelectActivityLog()
        {
            var sql = @"SELECT * FROM [ActivityLog] ORDER BY Id DESC";
            return await _connection.ExecuteReader<ActivityLog, ActivityLogMetadata>(sql);
        }
        public async Task<List<ActivityLog>> SelectActivityLogWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [ActivityLog] ORDER BY Id DESC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<ActivityLog, ActivityLogMetadata>(sql, parameters);
        }
        public async Task<List<ActivityLog>> SelectActivityLogByEntityId(Guid entityId)
        {
            var sql = @"SELECT * FROM [ActivityLog] WHERE EntityId = @EntityId ORDER BY Id DESC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@EntityId", entityId),
            };
            return await _connection.ExecuteReader<ActivityLog, ActivityLogMetadata>(sql, parameters);
        }
    }
}

