using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel
{
    internal partial interface IWidgetRepository
    {
        Task<Widget> InsertWidgetAsync(Widget entity);
        Task<Widget> UpdateWidgetAsync(Widget entity);
        Task<Widget> UpdateWidgetAsync(Widget entity, IEnumerable<string> properties);
        Task<Widget> UpdateWidgetAsync(Widget updated, Widget original);
        Task<Widget> UpdateWidgetAsync(Widget updated, Widget original, IEnumerable<string> properties);
        Task DeleteWidgetAsync(Guid key);
        Task<RepositoryQueryResult<Widget>> QueryWidgetAsync(RepositoryQuery query);
        Task<Widget?> GetWidgetAsync(Guid id);
        Task<Widget> RequireWidgetAsync(Guid id);
        Task<int> CountWidgetAsync();
        Task<List<Widget>> SelectWidgetAsync();
        Task<List<Widget>> SelectWidgetWithPageAsync(int skip, int take);
    }
    internal partial class WidgetRepository: IWidgetRepository
    {
        private readonly IRepositoryService RepositoryService;
        private readonly IRepositoryQueryService RepositoryQueryService;
        public WidgetRepository(IRepositoryService repositoryService, IRepositoryQueryService repositoryQueryService)
        {
            RepositoryService = repositoryService;
            RepositoryQueryService = repositoryQueryService;
        }
        public async Task<Widget> InsertWidgetAsync(Widget entity)
        {
            return await RepositoryService.ExecuteInsertAsync("InsertWidget", entity);
        }
        public async Task<Widget> UpdateWidgetAsync(Widget entity)
        {
            return await RepositoryService.ExecuteUpdateAsync("UpdateWidget", entity);
        }
        public async Task<Widget> UpdateWidgetAsync(Widget entity, IEnumerable<string> properties)
        {
            return await RepositoryService.ExecuteUpdateAsync("UpdateWidget", entity, properties);
        }
        public async Task<Widget> UpdateWidgetAsync(Widget updated, Widget original)
        {
            if(updated.Id != original.Id) throw new InvalidOperationException("UpdateWidgetAsync: Entity keys do not match.");
            var _properties = new List<string>();
            if(updated.Name != original.Name) _properties.Add("Name");
            if(updated.Age != original.Age) _properties.Add("Age");
            if(_properties.Count == 0) return updated;
            return await RepositoryService.ExecuteUpdateAsync("UpdateWidget", updated, _properties);
        }
        public async Task<Widget> UpdateWidgetAsync(Widget updated, Widget original, IEnumerable<string> properties)
        {
            if(updated.Id != original.Id) throw new InvalidOperationException("UpdateWidgetAsync: Entity keys do not match.");
            var _properties = new List<string>();
            if(properties.Contains("Name") && updated.Name != original.Name) _properties.Add("Name");
            if(properties.Contains("Age") && updated.Age != original.Age) _properties.Add("Age");
            if(_properties.Count == 0) return updated;
            return await RepositoryService.ExecuteUpdateAsync("UpdateWidget", updated, _properties);
        }
        public async Task DeleteWidgetAsync(Guid key)
        {
            await RepositoryService.ExecuteDeleteAsync<Widget>("DeleteWidget", key);
        }
        public async Task<RepositoryQueryResult<Widget>> QueryWidgetAsync(RepositoryQuery query)
        {
            return await RepositoryQueryService.ExecuteQueryAsync("RepositoryQueryWidget", query, new WidgetRepositoryAccessor());
        }
        public async Task<Widget?> GetWidgetAsync(Guid id)
        {
            var sql = @"SELECT * FROM [next].[Widget] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await RepositoryService.ExecuteReaderAsync<Widget>("GetWidget", sql, parameters)).SingleOrDefault();
        }
        public async Task<Widget> RequireWidgetAsync(Guid id)
        {
            return await GetWidgetAsync(id) ?? throw new InvalidOperationException("The specified Widget does not exist");
        }
        public async Task<int> CountWidgetAsync()
        {
            var sql = @"SELECT COUNT(Id) FROM [next].[Widget]";
            return (int)((await RepositoryService.ExecuteScalarAsync("CountWidget", sql))!);
        }
        public async Task<List<Widget>> SelectWidgetAsync()
        {
            var sql = @"SELECT * FROM [next].[Widget] ORDER BY Id ASC";
            return await RepositoryService.ExecuteReaderAsync<Widget>("SelectWidget", sql);
        }
        public async Task<List<Widget>> SelectWidgetWithPageAsync(int skip, int take)
        {
            var sql = @"SELECT * FROM [next].[Widget] ORDER BY Id ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await RepositoryService.ExecuteReaderAsync<Widget>("SelectWidgetWithPage", sql, parameters);
        }
    }
}

