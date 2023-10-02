using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IFundingStreamRepository
    {
        Task<FundingStream> InsertFundingStream(FundingStream entity);
        Task<FundingStream> UpdateFundingStream(FundingStream entity);
        Task<FundingStream> UpdateFundingStream(FundingStream updated, FundingStream original);
        Task DeleteFundingStream(Guid key);
        Task<QueryResult<FundingStream>> QueryFundingStream(Query query);
        Task<FundingStream?> GetFundingStream(Guid id);
        Task<FundingStream> RequireFundingStream(Guid id);
        Task<int> CountFundingStream();
        Task<List<FundingStream>> SelectFundingStream();
        Task<List<FundingStream>> SelectFundingStreamWithPage(int skip, int take);
        Task<FundingStream?> GetFundingStreamByCode(string code);
        Task<FundingStream> RequireFundingStreamByCode(string code);
    }
    [Service(typeof(IFundingStreamRepository))]
    internal partial class FundingStreamRepository: IFundingStreamRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public FundingStreamRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<FundingStream> InsertFundingStream(FundingStream entity)
        {
            if (entity.Id == Guid.Empty)
            entity.Id = GuidGenerator.NewComb();
            var sql = FundingStreamMetadata.InsertSQL();
            var parameters = FundingStreamMetadata.ExtractParameters(entity, FundingStreamMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters);
            return entity;
        }
        public async Task<FundingStream> UpdateFundingStream(FundingStream entity)
        {
            var sql = FundingStreamMetadata.UpdateSQL();
            var parameters = FundingStreamMetadata.ExtractParameters(entity, FundingStreamMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters);
            return entity;
        }
        public async Task<FundingStream> UpdateFundingStream(FundingStream updated, FundingStream original)
        {
            await UpdateFundingStream(updated);
            return updated;
        }
        public async Task DeleteFundingStream(Guid id)
        {
            var sql = FundingStreamMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters);
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
            var sql = @"SELECT * FROM [FundingStream] ORDER BY Id ASC";
            return await _connection.ExecuteReader<FundingStream, FundingStreamMetadata>(sql);
        }
        public async Task<List<FundingStream>> SelectFundingStreamWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [FundingStream] ORDER BY Id ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<FundingStream, FundingStreamMetadata>(sql, parameters);
        }
        public async Task<FundingStream?> GetFundingStreamByCode(string code)
        {
            var sql = @"SELECT * FROM [FundingStream] WHERE Code = @Code ORDER BY Id ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Code", code),
            };
            return (await _connection.ExecuteReader<FundingStream, FundingStreamMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<FundingStream> RequireFundingStreamByCode(string code)
        {
            return await GetFundingStreamByCode(code) ?? throw new InvalidOperationException("The specified FundingStream does not exist");
        }
    }
}

