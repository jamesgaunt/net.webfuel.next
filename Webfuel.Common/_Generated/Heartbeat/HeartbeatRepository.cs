using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Common
{
    internal partial interface IHeartbeatRepository
    {
        Task<Heartbeat> InsertHeartbeat(Heartbeat entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<Heartbeat> UpdateHeartbeat(Heartbeat entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<Heartbeat> UpdateHeartbeat(Heartbeat updated, Heartbeat original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteHeartbeat(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<Heartbeat>> QueryHeartbeat(Query query, bool selectItems = true, bool countTotal = true);
        Task<Heartbeat?> GetHeartbeat(Guid id);
        Task<Heartbeat> RequireHeartbeat(Guid id);
        Task<int> CountHeartbeat();
        Task<List<Heartbeat>> SelectHeartbeat();
        Task<List<Heartbeat>> SelectHeartbeatWithPage(int skip, int take);
    }
    [Service(typeof(IHeartbeatRepository))]
    internal partial class HeartbeatRepository: IHeartbeatRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public HeartbeatRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<Heartbeat> InsertHeartbeat(Heartbeat entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            HeartbeatMetadata.Validate(entity);
            var sql = HeartbeatMetadata.InsertSQL();
            var parameters = HeartbeatMetadata.ExtractParameters(entity, HeartbeatMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<Heartbeat> UpdateHeartbeat(Heartbeat entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            HeartbeatMetadata.Validate(entity);
            var sql = HeartbeatMetadata.UpdateSQL();
            var parameters = HeartbeatMetadata.ExtractParameters(entity, HeartbeatMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<Heartbeat> UpdateHeartbeat(Heartbeat updated, Heartbeat original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateHeartbeat(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteHeartbeat(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = HeartbeatMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<Heartbeat>> QueryHeartbeat(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<Heartbeat, HeartbeatMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<Heartbeat?> GetHeartbeat(Guid id)
        {
            var sql = @"SELECT * FROM [Heartbeat] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<Heartbeat, HeartbeatMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<Heartbeat> RequireHeartbeat(Guid id)
        {
            return await GetHeartbeat(id) ?? throw new InvalidOperationException("The specified Heartbeat does not exist");
        }
        public async Task<int> CountHeartbeat()
        {
            var sql = @"SELECT COUNT(Id) FROM [Heartbeat]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<Heartbeat>> SelectHeartbeat()
        {
            var sql = @"SELECT * FROM [Heartbeat] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<Heartbeat, HeartbeatMetadata>(sql);
        }
        public async Task<List<Heartbeat>> SelectHeartbeatWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [Heartbeat] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<Heartbeat, HeartbeatMetadata>(sql, parameters);
        }
    }
}

