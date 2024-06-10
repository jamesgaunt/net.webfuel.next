using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain
{
    internal partial interface ISupportRequestRepository
    {
        Task<SupportRequest> InsertSupportRequest(SupportRequest entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<SupportRequest> UpdateSupportRequest(SupportRequest entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<SupportRequest> UpdateSupportRequest(SupportRequest updated, SupportRequest original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteSupportRequest(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<SupportRequest>> QuerySupportRequest(Query query, bool selectItems = true, bool countTotal = true);
        Task<SupportRequest?> GetSupportRequest(Guid id);
        Task<SupportRequest> RequireSupportRequest(Guid id);
        Task<int> CountSupportRequest();
        Task<List<SupportRequest>> SelectSupportRequest();
        Task<List<SupportRequest>> SelectSupportRequestWithPage(int skip, int take);
    }
    [Service(typeof(ISupportRequestRepository))]
    internal partial class SupportRequestRepository: ISupportRequestRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public SupportRequestRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<SupportRequest> InsertSupportRequest(SupportRequest entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            SupportRequestMetadata.Validate(entity);
            var sql = SupportRequestMetadata.InsertSQL();
            var parameters = SupportRequestMetadata.ExtractParameters(entity, SupportRequestMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<SupportRequest> UpdateSupportRequest(SupportRequest entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            SupportRequestMetadata.Validate(entity);
            var sql = SupportRequestMetadata.UpdateSQL();
            var parameters = SupportRequestMetadata.ExtractParameters(entity, SupportRequestMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<SupportRequest> UpdateSupportRequest(SupportRequest updated, SupportRequest original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateSupportRequest(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteSupportRequest(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = SupportRequestMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<SupportRequest>> QuerySupportRequest(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<SupportRequest, SupportRequestMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<SupportRequest?> GetSupportRequest(Guid id)
        {
            var sql = @"SELECT * FROM [SupportRequest] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<SupportRequest, SupportRequestMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<SupportRequest> RequireSupportRequest(Guid id)
        {
            return await GetSupportRequest(id) ?? throw new InvalidOperationException("The specified SupportRequest does not exist");
        }
        public async Task<int> CountSupportRequest()
        {
            var sql = @"SELECT COUNT(Id) FROM [SupportRequest]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<SupportRequest>> SelectSupportRequest()
        {
            var sql = @"SELECT * FROM [SupportRequest] ORDER BY Number DESC";
            return await _connection.ExecuteReader<SupportRequest, SupportRequestMetadata>(sql);
        }
        public async Task<List<SupportRequest>> SelectSupportRequestWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [SupportRequest] ORDER BY Number DESC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<SupportRequest, SupportRequestMetadata>(sql, parameters);
        }
    }
}

