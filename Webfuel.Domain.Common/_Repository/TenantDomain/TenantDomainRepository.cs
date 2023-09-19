using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.Common
{
    internal partial interface ITenantDomainRepository
    {
        Task<TenantDomain> InsertTenantDomainAsync(TenantDomain entity);
        Task<TenantDomain> UpdateTenantDomainAsync(TenantDomain entity);
        Task<TenantDomain> UpdateTenantDomainAsync(TenantDomain entity, IEnumerable<string> properties);
        Task<TenantDomain> UpdateTenantDomainAsync(TenantDomain updated, TenantDomain original);
        Task<TenantDomain> UpdateTenantDomainAsync(TenantDomain updated, TenantDomain original, IEnumerable<string> properties);
        Task DeleteTenantDomainAsync(Guid key);
        Task<QueryResult<TenantDomain>> QueryTenantDomainAsync(RepositoryQuery query);
        Task<TenantDomain?> GetTenantDomainAsync(Guid id);
        Task<TenantDomain> RequireTenantDomainAsync(Guid id);
        Task<int> CountTenantDomainAsync();
        Task<List<TenantDomain>> SelectTenantDomainAsync();
        Task<List<TenantDomain>> SelectTenantDomainWithPageAsync(int skip, int take);
        Task<List<TenantDomain>> SelectTenantDomainByTenantIdAsync(Guid tenantId);
        Task<TenantDomain?> GetTenantDomainByDomainAsync(string domain);
        Task<TenantDomain> RequireTenantDomainByDomainAsync(string domain);
    }
    internal partial class TenantDomainRepository: ITenantDomainRepository
    {
        private readonly IRepositoryService RepositoryService;
        private readonly IRepositoryQueryService RepositoryQueryService;
        public TenantDomainRepository(IRepositoryService repositoryService, IRepositoryQueryService repositoryQueryService)
        {
            RepositoryService = repositoryService;
            RepositoryQueryService = repositoryQueryService;
        }
        public async Task<TenantDomain> InsertTenantDomainAsync(TenantDomain entity)
        {
            return await RepositoryService.ExecuteInsertAsync(entity);
        }
        public async Task<TenantDomain> UpdateTenantDomainAsync(TenantDomain entity)
        {
            return await RepositoryService.ExecuteUpdateAsync(entity);
        }
        public async Task<TenantDomain> UpdateTenantDomainAsync(TenantDomain entity, IEnumerable<string> properties)
        {
            return await RepositoryService.ExecuteUpdateAsync(entity, properties);
        }
        public async Task<TenantDomain> UpdateTenantDomainAsync(TenantDomain updated, TenantDomain original)
        {
            if(updated.Id != original.Id) throw new InvalidOperationException("UpdateTenantDomainAsync: Entity keys do not match.");
            var _properties = new List<string>();
            if(updated.Domain != original.Domain) _properties.Add("Domain");
            if(updated.RedirectTo != original.RedirectTo) _properties.Add("RedirectTo");
            if(updated.TenantId != original.TenantId) _properties.Add("TenantId");
            if(_properties.Count == 0) return updated;
            return await RepositoryService.ExecuteUpdateAsync(updated, _properties);
        }
        public async Task<TenantDomain> UpdateTenantDomainAsync(TenantDomain updated, TenantDomain original, IEnumerable<string> properties)
        {
            if(updated.Id != original.Id) throw new InvalidOperationException("UpdateTenantDomainAsync: Entity keys do not match.");
            var _properties = new List<string>();
            if(properties.Contains("Domain") && updated.Domain != original.Domain) _properties.Add("Domain");
            if(properties.Contains("RedirectTo") && updated.RedirectTo != original.RedirectTo) _properties.Add("RedirectTo");
            if(properties.Contains("TenantId") && updated.TenantId != original.TenantId) _properties.Add("TenantId");
            if(_properties.Count == 0) return updated;
            return await RepositoryService.ExecuteUpdateAsync(updated, _properties);
        }
        public async Task DeleteTenantDomainAsync(Guid key)
        {
            await RepositoryService.ExecuteDeleteAsync<TenantDomain>(key);
        }
        public async Task<QueryResult<TenantDomain>> QueryTenantDomainAsync(RepositoryQuery query)
        {
            return await RepositoryQueryService.ExecuteQueryAsync(query, new TenantDomainRepositoryAccessor());
        }
        public async Task<TenantDomain?> GetTenantDomainAsync(Guid id)
        {
            var sql = @"SELECT * FROM [next].[TenantDomain] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await RepositoryService.ExecuteReaderAsync<TenantDomain>(sql, parameters)).SingleOrDefault();
        }
        public async Task<TenantDomain> RequireTenantDomainAsync(Guid id)
        {
            return await GetTenantDomainAsync(id) ?? throw new InvalidOperationException("The specified TenantDomain does not exist");
        }
        public async Task<int> CountTenantDomainAsync()
        {
            var sql = @"SELECT COUNT(Id) FROM [next].[TenantDomain]";
            return (int)((await RepositoryService.ExecuteScalarAsync(sql))!);
        }
        public async Task<List<TenantDomain>> SelectTenantDomainAsync()
        {
            var sql = @"SELECT * FROM [next].[TenantDomain] ORDER BY Id ASC";
            return await RepositoryService.ExecuteReaderAsync<TenantDomain>(sql);
        }
        public async Task<List<TenantDomain>> SelectTenantDomainWithPageAsync(int skip, int take)
        {
            var sql = @"SELECT * FROM [next].[TenantDomain] ORDER BY Id ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await RepositoryService.ExecuteReaderAsync<TenantDomain>(sql, parameters);
        }
        public async Task<List<TenantDomain>> SelectTenantDomainByTenantIdAsync(Guid tenantId)
        {
            var sql = @"SELECT * FROM [next].[TenantDomain] WHERE TenantId = @TenantId ORDER BY Id ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@TenantId", tenantId),
            };
            return await RepositoryService.ExecuteReaderAsync<TenantDomain>(sql, parameters);
        }
        public async Task<TenantDomain?> GetTenantDomainByDomainAsync(string domain)
        {
            var sql = @"SELECT * FROM [next].[TenantDomain] WHERE Domain = @Domain ORDER BY Id ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Domain", domain),
            };
            return (await RepositoryService.ExecuteReaderAsync<TenantDomain>(sql, parameters)).SingleOrDefault();
        }
        public async Task<TenantDomain> RequireTenantDomainByDomainAsync(string domain)
        {
            return await GetTenantDomainByDomainAsync(domain) ?? throw new InvalidOperationException("The specified TenantDomain does not exist");
        }
    }
}

