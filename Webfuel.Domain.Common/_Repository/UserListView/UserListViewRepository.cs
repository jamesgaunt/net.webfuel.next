using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.Common
{
    internal partial interface IUserListViewRepository
    {
        Task<QueryResult<UserListView>> QueryUserListViewAsync(RepositoryQuery query);
        Task<UserListView?> GetUserListViewAsync(Guid id);
        Task<UserListView> RequireUserListViewAsync(Guid id);
        Task<int> CountUserListViewAsync();
        Task<List<UserListView>> SelectUserListViewAsync();
        Task<List<UserListView>> SelectUserListViewWithPageAsync(int skip, int take);
    }
    internal partial class UserListViewRepository: IUserListViewRepository
    {
        private readonly IRepositoryService RepositoryService;
        private readonly IRepositoryQueryService RepositoryQueryService;
        public UserListViewRepository(IRepositoryService repositoryService, IRepositoryQueryService repositoryQueryService)
        {
            RepositoryService = repositoryService;
            RepositoryQueryService = repositoryQueryService;
        }
        public async Task<QueryResult<UserListView>> QueryUserListViewAsync(RepositoryQuery query)
        {
            return await RepositoryQueryService.ExecuteQueryAsync(query, new UserListViewRepositoryAccessor());
        }
        public async Task<UserListView?> GetUserListViewAsync(Guid id)
        {
            var sql = @"SELECT * FROM [next].[UserListView] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await RepositoryService.ExecuteReaderAsync<UserListView>(sql, parameters)).SingleOrDefault();
        }
        public async Task<UserListView> RequireUserListViewAsync(Guid id)
        {
            return await GetUserListViewAsync(id) ?? throw new InvalidOperationException("The specified UserListView does not exist");
        }
        public async Task<int> CountUserListViewAsync()
        {
            var sql = @"SELECT COUNT(Id) FROM [next].[UserListView]";
            return (int)((await RepositoryService.ExecuteScalarAsync(sql))!);
        }
        public async Task<List<UserListView>> SelectUserListViewAsync()
        {
            var sql = @"SELECT * FROM [next].[UserListView] ORDER BY Id ASC";
            return await RepositoryService.ExecuteReaderAsync<UserListView>(sql);
        }
        public async Task<List<UserListView>> SelectUserListViewWithPageAsync(int skip, int take)
        {
            var sql = @"SELECT * FROM [next].[UserListView] ORDER BY Id ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await RepositoryService.ExecuteReaderAsync<UserListView>(sql, parameters);
        }
    }
}

