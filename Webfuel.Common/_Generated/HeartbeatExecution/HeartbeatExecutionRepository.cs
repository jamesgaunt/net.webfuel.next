using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Common
{
    internal partial interface IHeartbeatExecutionRepository
    {
        Task<HeartbeatExecution> InsertHeartbeatExecution(HeartbeatExecution entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<HeartbeatExecution> UpdateHeartbeatExecution(HeartbeatExecution entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<HeartbeatExecution> UpdateHeartbeatExecution(HeartbeatExecution updated, HeartbeatExecution original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteHeartbeatExecution(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<HeartbeatExecution>> QueryHeartbeatExecution(Query query, bool selectItems = true, bool countTotal = true);
        Task<HeartbeatExecution?> GetHeartbeatExecution(Guid id);
        Task<HeartbeatExecution> RequireHeartbeatExecution(Guid id);
        Task<int> CountHeartbeatExecution();
        Task<List<HeartbeatExecution>> SelectHeartbeatExecution();
        Task<List<HeartbeatExecution>> SelectHeartbeatExecutionWithPage(int skip, int take);
        Task<List<HeartbeatExecution>> SelectHeartbeatExecutionByHeartbeatId(Guid heartbeatId);
    }
    [Service(typeof(IHeartbeatExecutionRepository))]
    internal partial class HeartbeatExecutionRepository: IHeartbeatExecutionRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public HeartbeatExecutionRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<HeartbeatExecution> InsertHeartbeatExecution(HeartbeatExecution entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            HeartbeatExecutionMetadata.Validate(entity);
            var sql = HeartbeatExecutionMetadata.InsertSQL();
            var parameters = HeartbeatExecutionMetadata.ExtractParameters(entity, HeartbeatExecutionMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<HeartbeatExecution> UpdateHeartbeatExecution(HeartbeatExecution entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            HeartbeatExecutionMetadata.Validate(entity);
            var sql = HeartbeatExecutionMetadata.UpdateSQL();
            var parameters = HeartbeatExecutionMetadata.ExtractParameters(entity, HeartbeatExecutionMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<HeartbeatExecution> UpdateHeartbeatExecution(HeartbeatExecution updated, HeartbeatExecution original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateHeartbeatExecution(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteHeartbeatExecution(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = HeartbeatExecutionMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<HeartbeatExecution>> QueryHeartbeatExecution(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<HeartbeatExecution, HeartbeatExecutionMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<HeartbeatExecution?> GetHeartbeatExecution(Guid id)
        {
            var sql = @"SELECT * FROM [HeartbeatExecution] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<HeartbeatExecution, HeartbeatExecutionMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<HeartbeatExecution> RequireHeartbeatExecution(Guid id)
        {
            return await GetHeartbeatExecution(id) ?? throw new InvalidOperationException("The specified HeartbeatExecution does not exist");
        }
        public async Task<int> CountHeartbeatExecution()
        {
            var sql = @"SELECT COUNT(Id) FROM [HeartbeatExecution]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<HeartbeatExecution>> SelectHeartbeatExecution()
        {
            var sql = @"SELECT * FROM [HeartbeatExecution] ORDER BY Id DESC";
            return await _connection.ExecuteReader<HeartbeatExecution, HeartbeatExecutionMetadata>(sql);
        }
        public async Task<List<HeartbeatExecution>> SelectHeartbeatExecutionWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [HeartbeatExecution] ORDER BY Id DESC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<HeartbeatExecution, HeartbeatExecutionMetadata>(sql, parameters);
        }
        public async Task<List<HeartbeatExecution>> SelectHeartbeatExecutionByHeartbeatId(Guid heartbeatId)
        {
            var sql = @"SELECT * FROM [HeartbeatExecution] WHERE HeartbeatId = @HeartbeatId ORDER BY Id DESC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@HeartbeatId", heartbeatId),
            };
            return await _connection.ExecuteReader<HeartbeatExecution, HeartbeatExecutionMetadata>(sql, parameters);
        }
    }
}

