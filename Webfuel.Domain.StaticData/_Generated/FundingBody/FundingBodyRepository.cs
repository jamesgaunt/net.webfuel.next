using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IFundingBodyRepository
    {
        Task<FundingBody> InsertFundingBody(FundingBody entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<FundingBody> UpdateFundingBody(FundingBody entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<FundingBody> UpdateFundingBody(FundingBody updated, FundingBody original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteFundingBody(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<FundingBody>> QueryFundingBody(Query query);
        Task<FundingBody?> GetFundingBody(Guid id);
        Task<FundingBody> RequireFundingBody(Guid id);
        Task<int> CountFundingBody();
        Task<List<FundingBody>> SelectFundingBody();
        Task<List<FundingBody>> SelectFundingBodyWithPage(int skip, int take);
        Task<FundingBody?> GetFundingBodyByCode(string code);
        Task<FundingBody> RequireFundingBodyByCode(string code);
    }
    [Service(typeof(IFundingBodyRepository))]
    internal partial class FundingBodyRepository: IFundingBodyRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public FundingBodyRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<FundingBody> InsertFundingBody(FundingBody entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            var sql = FundingBodyMetadata.InsertSQL();
            var parameters = FundingBodyMetadata.ExtractParameters(entity, FundingBodyMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<FundingBody> UpdateFundingBody(FundingBody entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = FundingBodyMetadata.UpdateSQL();
            var parameters = FundingBodyMetadata.ExtractParameters(entity, FundingBodyMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<FundingBody> UpdateFundingBody(FundingBody updated, FundingBody original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateFundingBody(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteFundingBody(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = FundingBodyMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<FundingBody>> QueryFundingBody(Query query)
        {
            return await _connection.ExecuteQuery<FundingBody, FundingBodyMetadata>(query);
        }
        public async Task<FundingBody?> GetFundingBody(Guid id)
        {
            var sql = @"SELECT * FROM [FundingBody] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<FundingBody, FundingBodyMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<FundingBody> RequireFundingBody(Guid id)
        {
            return await GetFundingBody(id) ?? throw new InvalidOperationException("The specified FundingBody does not exist");
        }
        public async Task<int> CountFundingBody()
        {
            var sql = @"SELECT COUNT(Id) FROM [FundingBody]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<FundingBody>> SelectFundingBody()
        {
            var sql = @"SELECT * FROM [FundingBody] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<FundingBody, FundingBodyMetadata>(sql);
        }
        public async Task<List<FundingBody>> SelectFundingBodyWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [FundingBody] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<FundingBody, FundingBodyMetadata>(sql, parameters);
        }
        public async Task<FundingBody?> GetFundingBodyByCode(string code)
        {
            var sql = @"SELECT * FROM [FundingBody] WHERE Code = @Code ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Code", code),
            };
            return (await _connection.ExecuteReader<FundingBody, FundingBodyMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<FundingBody> RequireFundingBodyByCode(string code)
        {
            return await GetFundingBodyByCode(code) ?? throw new InvalidOperationException("The specified FundingBody does not exist");
        }
    }
}

