using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IFundingStreamRepository
    {
        Task<FundingStream> InsertFundingStream(FundingStream entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<FundingStream> UpdateFundingStream(FundingStream entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<FundingStream> UpdateFundingStream(FundingStream updated, FundingStream original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteFundingStream(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<FundingStream>> QueryFundingStream(Query query);
        Task<FundingStream?> GetFundingStream(Guid id);
        Task<FundingStream> RequireFundingStream(Guid id);
        Task<int> CountFundingStream();
        Task<List<FundingStream>> SelectFundingStream();
        Task<List<FundingStream>> SelectFundingStreamWithPage(int skip, int take);
        Task<FundingStream?> GetFundingStreamByName(string name);
        Task<FundingStream> RequireFundingStreamByName(string name);
    }
    [Service(typeof(IFundingStreamRepository))]
    internal partial class FundingStreamRepository: IFundingStreamRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public FundingStreamRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<FundingStream> InsertFundingStream(FundingStream entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            var sql = FundingStreamMetadata.InsertSQL();
            var parameters = FundingStreamMetadata.ExtractParameters(entity, FundingStreamMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<FundingStream> UpdateFundingStream(FundingStream entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = FundingStreamMetadata.UpdateSQL();
            var parameters = FundingStreamMetadata.ExtractParameters(entity, FundingStreamMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<FundingStream> UpdateFundingStream(FundingStream updated, FundingStream original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateFundingStream(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteFundingStream(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = FundingStreamMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<FundingStream>> QueryFundingStream(Query query)
        {
            return await _connection.ExecuteQuery<FundingStream, FundingStreamMetadata>(query);
        }
        public async Task<FundingStream?> GetFundingStream(Guid id)
        {
            var sql = @"SELECT * FROM [FundingStream] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<FundingStream, FundingStreamMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<FundingStream> RequireFundingStream(Guid id)
        {
            return await GetFundingStream(id) ?? throw new InvalidOperationException("The specified FundingStream does not exist");
        }
        public async Task<int> CountFundingStream()
        {
            var sql = @"SELECT COUNT(Id) FROM [FundingStream]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<FundingStream>> SelectFundingStream()
        {
            var sql = @"SELECT * FROM [FundingStream] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<FundingStream, FundingStreamMetadata>(sql);
        }
        public async Task<List<FundingStream>> SelectFundingStreamWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [FundingStream] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<FundingStream, FundingStreamMetadata>(sql, parameters);
        }
        public async Task<FundingStream?> GetFundingStreamByName(string name)
        {
            var sql = @"SELECT * FROM [FundingStream] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<FundingStream, FundingStreamMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<FundingStream> RequireFundingStreamByName(string name)
        {
            return await GetFundingStreamByName(name) ?? throw new InvalidOperationException("The specified FundingStream does not exist");
        }
    }
}

