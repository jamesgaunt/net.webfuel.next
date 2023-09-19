using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.Common
{
    internal partial interface IWidgetQueryViewRepository
    {
        Task<QueryResult<WidgetQueryView>> QueryWidgetQueryViewAsync(RepositoryQuery query);
        Task<WidgetQueryView?> GetWidgetQueryViewAsync(Guid id);
        Task<WidgetQueryView> RequireWidgetQueryViewAsync(Guid id);
        Task<int> CountWidgetQueryViewAsync();
        Task<List<WidgetQueryView>> SelectWidgetQueryViewAsync();
        Task<List<WidgetQueryView>> SelectWidgetQueryViewWithPageAsync(int skip, int take);
    }
    internal partial class WidgetQueryViewRepository: IWidgetQueryViewRepository
    {
        private readonly IRepositoryService RepositoryService;
        private readonly IRepositoryQueryService RepositoryQueryService;
        public WidgetQueryViewRepository(IRepositoryService repositoryService, IRepositoryQueryService repositoryQueryService)
        {
            RepositoryService = repositoryService;
            RepositoryQueryService = repositoryQueryService;
        }
        public async Task<QueryResult<WidgetQueryView>> QueryWidgetQueryViewAsync(RepositoryQuery query)
        {
            return await RepositoryQueryService.ExecuteQueryAsync(query, new WidgetQueryViewRepositoryAccessor());
        }
        public async Task<WidgetQueryView?> GetWidgetQueryViewAsync(Guid id)
        {
            var sql = @"SELECT * FROM [next].[WidgetQueryView] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await RepositoryService.ExecuteReaderAsync<WidgetQueryView>(sql, parameters)).SingleOrDefault();
        }
        public async Task<WidgetQueryView> RequireWidgetQueryViewAsync(Guid id)
        {
            return await GetWidgetQueryViewAsync(id) ?? throw new InvalidOperationException("The specified WidgetQueryView does not exist");
        }
        public async Task<int> CountWidgetQueryViewAsync()
        {
            var sql = @"SELECT COUNT(Id) FROM [next].[WidgetQueryView]";
            return (int)((await RepositoryService.ExecuteScalarAsync(sql))!);
        }
        public async Task<List<WidgetQueryView>> SelectWidgetQueryViewAsync()
        {
            var sql = @"SELECT * FROM [next].[WidgetQueryView] ORDER BY Id ASC";
            return await RepositoryService.ExecuteReaderAsync<WidgetQueryView>(sql);
        }
        public async Task<List<WidgetQueryView>> SelectWidgetQueryViewWithPageAsync(int skip, int take)
        {
            var sql = @"SELECT * FROM [next].[WidgetQueryView] ORDER BY Id ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await RepositoryService.ExecuteReaderAsync<WidgetQueryView>(sql, parameters);
        }
    }
}

