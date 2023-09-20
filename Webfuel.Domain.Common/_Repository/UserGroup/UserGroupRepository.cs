using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.Common
{
    internal partial interface IUserGroupRepository
    {
        Task<UserGroup> InsertUserGroupAsync(UserGroup entity);
        Task<UserGroup> UpdateUserGroupAsync(UserGroup entity);
        Task<UserGroup> UpdateUserGroupAsync(UserGroup entity, IEnumerable<string> properties);
        Task<UserGroup> UpdateUserGroupAsync(UserGroup updated, UserGroup original);
        Task<UserGroup> UpdateUserGroupAsync(UserGroup updated, UserGroup original, IEnumerable<string> properties);
        Task DeleteUserGroupAsync(Guid key);
        Task<QueryResult<UserGroup>> QueryUserGroupAsync(RepositoryQuery query);
        Task<UserGroup?> GetUserGroupAsync(Guid id);
        Task<UserGroup> RequireUserGroupAsync(Guid id);
        Task<int> CountUserGroupAsync();
        Task<List<UserGroup>> SelectUserGroupAsync();
        Task<List<UserGroup>> SelectUserGroupWithPageAsync(int skip, int take);
    }
    internal partial class UserGroupRepository: IUserGroupRepository
    {
        private readonly IRepositoryService RepositoryService;
        private readonly IRepositoryQueryService RepositoryQueryService;
        public UserGroupRepository(IRepositoryService repositoryService, IRepositoryQueryService repositoryQueryService)
        {
            RepositoryService = repositoryService;
            RepositoryQueryService = repositoryQueryService;
        }
        public async Task<UserGroup> InsertUserGroupAsync(UserGroup entity)
        {
            return await RepositoryService.ExecuteInsertAsync(entity);
        }
        public async Task<UserGroup> UpdateUserGroupAsync(UserGroup entity)
        {
            return await RepositoryService.ExecuteUpdateAsync(entity);
        }
        public async Task<UserGroup> UpdateUserGroupAsync(UserGroup entity, IEnumerable<string> properties)
        {
            return await RepositoryService.ExecuteUpdateAsync(entity, properties);
        }
        public async Task<UserGroup> UpdateUserGroupAsync(UserGroup updated, UserGroup original)
        {
            if(updated.Id != original.Id) throw new InvalidOperationException("UpdateUserGroupAsync: Entity keys do not match.");
            var _properties = new List<string>();
            if(updated.Name != original.Name) _properties.Add("Name");
            if(_properties.Count == 0) return updated;
            return await RepositoryService.ExecuteUpdateAsync(updated, _properties);
        }
        public async Task<UserGroup> UpdateUserGroupAsync(UserGroup updated, UserGroup original, IEnumerable<string> properties)
        {
            if(updated.Id != original.Id) throw new InvalidOperationException("UpdateUserGroupAsync: Entity keys do not match.");
            var _properties = new List<string>();
            if(properties.Contains("Name") && updated.Name != original.Name) _properties.Add("Name");
            if(_properties.Count == 0) return updated;
            return await RepositoryService.ExecuteUpdateAsync(updated, _properties);
        }
        public async Task DeleteUserGroupAsync(Guid key)
        {
            await RepositoryService.ExecuteDeleteAsync<UserGroup>(key);
        }
        public async Task<QueryResult<UserGroup>> QueryUserGroupAsync(RepositoryQuery query)
        {
            return await RepositoryQueryService.ExecuteQueryAsync(query, new UserGroupRepositoryAccessor());
        }
        public async Task<UserGroup?> GetUserGroupAsync(Guid id)
        {
            var sql = @"SELECT * FROM [UserGroup] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await RepositoryService.ExecuteReaderAsync<UserGroup>(sql, parameters)).SingleOrDefault();
        }
        public async Task<UserGroup> RequireUserGroupAsync(Guid id)
        {
            return await GetUserGroupAsync(id) ?? throw new InvalidOperationException("The specified UserGroup does not exist");
        }
        public async Task<int> CountUserGroupAsync()
        {
            var sql = @"SELECT COUNT(Id) FROM [UserGroup]";
            return (int)((await RepositoryService.ExecuteScalarAsync(sql))!);
        }
        public async Task<List<UserGroup>> SelectUserGroupAsync()
        {
            var sql = @"SELECT * FROM [UserGroup] ORDER BY Id ASC";
            return await RepositoryService.ExecuteReaderAsync<UserGroup>(sql);
        }
        public async Task<List<UserGroup>> SelectUserGroupWithPageAsync(int skip, int take)
        {
            var sql = @"SELECT * FROM [UserGroup] ORDER BY Id ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await RepositoryService.ExecuteReaderAsync<UserGroup>(sql, parameters);
        }
    }
}

