using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.Common
{
    internal partial interface IUserGroupListViewRepository
    {
        Task<QueryResult<UserGroupListView>> QueryUserGroupListViewAsync(RepositoryQuery query);
        Task<UserGroupListView?> GetUserGroupListViewAsync(Guid id);
        Task<UserGroupListView> RequireUserGroupListViewAsync(Guid id);
        Task<int> CountUserGroupListViewAsync();
        Task<List<UserGroupListView>> SelectUserGroupListViewAsync();
        Task<List<UserGroupListView>> SelectUserGroupListViewWithPageAsync(int skip, int take);
    }
    internal partial class UserGroupListViewRepository: IUserGroupListViewRepository
    {
        private readonly IRepositoryService RepositoryService;
        private readonly IRepositoryQueryService RepositoryQueryService;
        public UserGroupListViewRepository(IRepositoryService repositoryService, IRepositoryQueryService repositoryQueryService)
        {
            RepositoryService = repositoryService;
            RepositoryQueryService = repositoryQueryService;
        }
        public async Task<QueryResult<UserGroupListView>> QueryUserGroupListViewAsync(RepositoryQuery query)
        {
            return await RepositoryQueryService.ExecuteQueryAsync(query, new UserGroupListViewRepositoryAccessor());
        }
        public async Task<UserGroupListView?> GetUserGroupListViewAsync(Guid id)
        {
            var sql = @"SELECT * FROM [UserGroupListView] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await RepositoryService.ExecuteReaderAsync<UserGroupListView>(sql, parameters)).SingleOrDefault();
        }
        public async Task<UserGroupListView> RequireUserGroupListViewAsync(Guid id)
        {
            return await GetUserGroupListViewAsync(id) ?? throw new InvalidOperationException("The specified UserGroupListView does not exist");
        }
        public async Task<int> CountUserGroupListViewAsync()
        {
            var sql = @"SELECT COUNT(Id) FROM [UserGroupListView]";
            return (int)((await RepositoryService.ExecuteScalarAsync(sql))!);
        }
        public async Task<List<UserGroupListView>> SelectUserGroupListViewAsync()
        {
            var sql = @"SELECT * FROM [UserGroupListView] ORDER BY Id ASC";
            return await RepositoryService.ExecuteReaderAsync<UserGroupListView>(sql);
        }
        public async Task<List<UserGroupListView>> SelectUserGroupListViewWithPageAsync(int skip, int take)
        {
            var sql = @"SELECT * FROM [UserGroupListView] ORDER BY Id ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await RepositoryService.ExecuteReaderAsync<UserGroupListView>(sql, parameters);
        }
    }
}

