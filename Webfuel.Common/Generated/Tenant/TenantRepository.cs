using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Common
{
    internal partial interface ITenantRepository
    {
        Task<Tenant> InsertTenantAsync(Tenant entity);
        Task<Tenant> UpdateTenantAsync(Tenant entity);
        Task<Tenant> UpdateTenantAsync(Tenant entity, IEnumerable<string> properties);
        Task<Tenant> UpdateTenantAsync(Tenant updated, Tenant original);
        Task<Tenant> UpdateTenantAsync(Tenant updated, Tenant original, IEnumerable<string> properties);
        Task DeleteTenantAsync(Guid key);
        Task<QueryResult<Tenant>> QueryTenantAsync(RepositoryQuery query);
        Task<Tenant?> GetTenantAsync(Guid id);
        Task<Tenant> RequireTenantAsync(Guid id);
        Task<int> CountTenantAsync();
        Task<List<Tenant>> SelectTenantAsync();
        Task<List<Tenant>> SelectTenantWithPageAsync(int skip, int take);
        Task<Tenant?> GetTenantByNameAsync(string name);
        Task<Tenant> RequireTenantByNameAsync(string name);
    }
    internal partial class TenantRepository: ITenantRepository
    {
        private readonly IRepositoryService RepositoryService;
        private readonly IRepositoryQueryService RepositoryQueryService;
        public TenantRepository(IRepositoryService repositoryService, IRepositoryQueryService repositoryQueryService)
        {
            RepositoryService = repositoryService;
            RepositoryQueryService = repositoryQueryService;
        }
        public async Task<Tenant> InsertTenantAsync(Tenant entity)
        {
            return await RepositoryService.ExecuteInsertAsync("InsertTenant", entity);
        }
        public async Task<Tenant> UpdateTenantAsync(Tenant entity)
        {
            return await RepositoryService.ExecuteUpdateAsync("UpdateTenant", entity);
        }
        public async Task<Tenant> UpdateTenantAsync(Tenant entity, IEnumerable<string> properties)
        {
            return await RepositoryService.ExecuteUpdateAsync("UpdateTenant", entity, properties);
        }
        public async Task<Tenant> UpdateTenantAsync(Tenant updated, Tenant original)
        {
            if(updated.Id != original.Id) throw new InvalidOperationException("UpdateTenantAsync: Entity keys do not match.");
            var _properties = new List<string>();
            if(updated.Name != original.Name) _properties.Add("Name");
            if(updated.Live != original.Live) _properties.Add("Live");
            if(_properties.Count == 0) return updated;
            return await RepositoryService.ExecuteUpdateAsync("UpdateTenant", updated, _properties);
        }
        public async Task<Tenant> UpdateTenantAsync(Tenant updated, Tenant original, IEnumerable<string> properties)
        {
            if(updated.Id != original.Id) throw new InvalidOperationException("UpdateTenantAsync: Entity keys do not match.");
            var _properties = new List<string>();
            if(properties.Contains("Name") && updated.Name != original.Name) _properties.Add("Name");
            if(properties.Contains("Live") && updated.Live != original.Live) _properties.Add("Live");
            if(_properties.Count == 0) return updated;
            return await RepositoryService.ExecuteUpdateAsync("UpdateTenant", updated, _properties);
        }
        public async Task DeleteTenantAsync(Guid key)
        {
            await RepositoryService.ExecuteDeleteAsync<Tenant>("DeleteTenant", key);
        }
        public async Task<QueryResult<Tenant>> QueryTenantAsync(RepositoryQuery query)
        {
            return await RepositoryQueryService.ExecuteQueryAsync("RepositoryQueryTenant", query, new TenantRepositoryAccessor());
        }
        public async Task<Tenant?> GetTenantAsync(Guid id)
        {
            var sql = @"SELECT * FROM [next].[Tenant] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await RepositoryService.ExecuteReaderAsync<Tenant>("GetTenant", sql, parameters)).SingleOrDefault();
        }
        public async Task<Tenant> RequireTenantAsync(Guid id)
        {
            return await GetTenantAsync(id) ?? throw new InvalidOperationException("The specified Tenant does not exist");
        }
        public async Task<int> CountTenantAsync()
        {
            var sql = @"SELECT COUNT(Id) FROM [next].[Tenant]";
            return (int)((await RepositoryService.ExecuteScalarAsync("CountTenant", sql))!);
        }
        public async Task<List<Tenant>> SelectTenantAsync()
        {
            var sql = @"SELECT * FROM [next].[Tenant] ORDER BY Id ASC";
            return await RepositoryService.ExecuteReaderAsync<Tenant>("SelectTenant", sql);
        }
        public async Task<List<Tenant>> SelectTenantWithPageAsync(int skip, int take)
        {
            var sql = @"SELECT * FROM [next].[Tenant] ORDER BY Id ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await RepositoryService.ExecuteReaderAsync<Tenant>("SelectTenantWithPage", sql, parameters);
        }
        public async Task<Tenant?> GetTenantByNameAsync(string name)
        {
            var sql = @"SELECT * FROM [next].[Tenant] WHERE Name = @Name ORDER BY Id ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await RepositoryService.ExecuteReaderAsync<Tenant>("GetTenantByName", sql, parameters)).SingleOrDefault();
        }
        public async Task<Tenant> RequireTenantByNameAsync(string name)
        {
            return await GetTenantByNameAsync(name) ?? throw new InvalidOperationException("The specified Tenant does not exist");
        }
    }
}

