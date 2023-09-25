using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain
{
    internal partial interface IFundingStreamRepository
    {
        Task<FundingStream> InsertFundingStream(FundingStream entity);
        Task<FundingStream> UpdateFundingStream(FundingStream entity);
        Task<FundingStream> UpdateFundingStream(FundingStream entity, IEnumerable<string> properties);
        Task<FundingStream> UpdateFundingStream(FundingStream updated, FundingStream original);
        Task<FundingStream> UpdateFundingStream(FundingStream updated, FundingStream original, IEnumerable<string> properties);
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
    internal partial class FundingStreamRepository: IFundingStreamRepository
    {
        private readonly IRepositoryService RepositoryService;
        private readonly IRepositoryQueryService RepositoryQueryService;
        public FundingStreamRepository(IRepositoryService repositoryService, IRepositoryQueryService repositoryQueryService)
        {
            RepositoryService = repositoryService;
            RepositoryQueryService = repositoryQueryService;
        }
        public async Task<FundingStream> InsertFundingStream(FundingStream entity)
        {
            return await RepositoryService.ExecuteInsert(entity);
        }
        public async Task<FundingStream> UpdateFundingStream(FundingStream entity)
        {
            return await RepositoryService.ExecuteUpdate(entity);
        }
        public async Task<FundingStream> UpdateFundingStream(FundingStream entity, IEnumerable<string> properties)
        {
            return await RepositoryService.ExecuteUpdate(entity, properties);
        }
        public async Task<FundingStream> UpdateFundingStream(FundingStream updated, FundingStream original)
        {
            if(updated.Id != original.Id) throw new InvalidOperationException("UpdateFundingStream: Entity keys do not match.");
            var _properties = new List<string>();
            if(updated.Name != original.Name) _properties.Add("Name");
            if(updated.Code != original.Code) _properties.Add("Code");
            if(updated.SortOrder != original.SortOrder) _properties.Add("SortOrder");
            if(updated.Default != original.Default) _properties.Add("Default");
            if(updated.Hidden != original.Hidden) _properties.Add("Hidden");
            if(_properties.Count == 0) return updated;
            return await RepositoryService.ExecuteUpdate(updated, _properties);
        }
        public async Task<FundingStream> UpdateFundingStream(FundingStream updated, FundingStream original, IEnumerable<string> properties)
        {
            if(updated.Id != original.Id) throw new InvalidOperationException("UpdateFundingStream: Entity keys do not match.");
            var _properties = new List<string>();
            if(properties.Contains("Name") && updated.Name != original.Name) _properties.Add("Name");
            if(properties.Contains("Code") && updated.Code != original.Code) _properties.Add("Code");
            if(properties.Contains("SortOrder") && updated.SortOrder != original.SortOrder) _properties.Add("SortOrder");
            if(properties.Contains("Default") && updated.Default != original.Default) _properties.Add("Default");
            if(properties.Contains("Hidden") && updated.Hidden != original.Hidden) _properties.Add("Hidden");
            if(_properties.Count == 0) return updated;
            return await RepositoryService.ExecuteUpdate(updated, _properties);
        }
        public async Task DeleteFundingStream(Guid key)
        {
            await RepositoryService.ExecuteDelete<FundingStream>(key);
        }
        public async Task<QueryResult<FundingStream>> QueryFundingStream(Query query)
        {
            return await RepositoryQueryService.ExecuteQuery(query, new FundingStreamRepositoryAccessor());
        }
        public async Task<FundingStream?> GetFundingStream(Guid id)
        {
            var sql = @"SELECT * FROM [FundingStream] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await RepositoryService.ExecuteReader<FundingStream>(sql, parameters)).SingleOrDefault();
        }
        public async Task<FundingStream> RequireFundingStream(Guid id)
        {
            return await GetFundingStream(id) ?? throw new InvalidOperationException("The specified FundingStream does not exist");
        }
        public async Task<int> CountFundingStream()
        {
            var sql = @"SELECT COUNT(Id) FROM [FundingStream]";
            return (int)((await RepositoryService.ExecuteScalar(sql))!);
        }
        public async Task<List<FundingStream>> SelectFundingStream()
        {
            var sql = @"SELECT * FROM [FundingStream] ORDER BY Id ASC";
            return await RepositoryService.ExecuteReader<FundingStream>(sql);
        }
        public async Task<List<FundingStream>> SelectFundingStreamWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [FundingStream] ORDER BY Id ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await RepositoryService.ExecuteReader<FundingStream>(sql, parameters);
        }
        public async Task<FundingStream?> GetFundingStreamByCode(string code)
        {
            var sql = @"SELECT * FROM [FundingStream] WHERE Code = @Code ORDER BY Id ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Code", code),
            };
            return (await RepositoryService.ExecuteReader<FundingStream>(sql, parameters)).SingleOrDefault();
        }
        public async Task<FundingStream> RequireFundingStreamByCode(string code)
        {
            return await GetFundingStreamByCode(code) ?? throw new InvalidOperationException("The specified FundingStream does not exist");
        }
    }
}

