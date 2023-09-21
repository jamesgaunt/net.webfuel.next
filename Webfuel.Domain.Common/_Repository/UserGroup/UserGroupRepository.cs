using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.Common
{
    internal partial interface IUserGroupRepository
    {
        Task<UserGroup> InsertUserGroup(UserGroup entity);
        Task<UserGroup> UpdateUserGroup(UserGroup entity);
        Task<UserGroup> UpdateUserGroup(UserGroup entity, IEnumerable<string> properties);
        Task<UserGroup> UpdateUserGroup(UserGroup updated, UserGroup original);
        Task<UserGroup> UpdateUserGroup(UserGroup updated, UserGroup original, IEnumerable<string> properties);
        Task DeleteUserGroup(Guid key);
        Task<QueryResult<UserGroup>> QueryUserGroup(Query query);
        Task<UserGroup?> GetUserGroup(Guid id);
        Task<UserGroup> RequireUserGroup(Guid id);
        Task<int> CountUserGroup();
        Task<List<UserGroup>> SelectUserGroup();
        Task<List<UserGroup>> SelectUserGroupWithPage(int skip, int take);
        Task<UserGroup?> GetUserGroupByName(string name);
        Task<UserGroup> RequireUserGroupByName(string name);
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
        public async Task<UserGroup> InsertUserGroup(UserGroup entity)
        {
            return await RepositoryService.ExecuteInsert(entity);
        }
        public async Task<UserGroup> UpdateUserGroup(UserGroup entity)
        {
            return await RepositoryService.ExecuteUpdate(entity);
        }
        public async Task<UserGroup> UpdateUserGroup(UserGroup entity, IEnumerable<string> properties)
        {
            return await RepositoryService.ExecuteUpdate(entity, properties);
        }
        public async Task<UserGroup> UpdateUserGroup(UserGroup updated, UserGroup original)
        {
            if(updated.Id != original.Id) throw new InvalidOperationException("UpdateUserGroup: Entity keys do not match.");
            var _properties = new List<string>();
            if(updated.Name != original.Name) _properties.Add("Name");
            if(_properties.Count == 0) return updated;
            return await RepositoryService.ExecuteUpdate(updated, _properties);
        }
        public async Task<UserGroup> UpdateUserGroup(UserGroup updated, UserGroup original, IEnumerable<string> properties)
        {
            if(updated.Id != original.Id) throw new InvalidOperationException("UpdateUserGroup: Entity keys do not match.");
            var _properties = new List<string>();
            if(properties.Contains("Name") && updated.Name != original.Name) _properties.Add("Name");
            if(_properties.Count == 0) return updated;
            return await RepositoryService.ExecuteUpdate(updated, _properties);
        }
        public async Task DeleteUserGroup(Guid key)
        {
            await RepositoryService.ExecuteDelete<UserGroup>(key);
        }
        public async Task<QueryResult<UserGroup>> QueryUserGroup(Query query)
        {
            return await RepositoryQueryService.ExecuteQuery(query, new UserGroupRepositoryAccessor());
        }
        public async Task<UserGroup?> GetUserGroup(Guid id)
        {
            var sql = @"SELECT * FROM [UserGroup] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await RepositoryService.ExecuteReader<UserGroup>(sql, parameters)).SingleOrDefault();
        }
        public async Task<UserGroup> RequireUserGroup(Guid id)
        {
            return await GetUserGroup(id) ?? throw new InvalidOperationException("The specified UserGroup does not exist");
        }
        public async Task<int> CountUserGroup()
        {
            var sql = @"SELECT COUNT(Id) FROM [UserGroup]";
            return (int)((await RepositoryService.ExecuteScalar(sql))!);
        }
        public async Task<List<UserGroup>> SelectUserGroup()
        {
            var sql = @"SELECT * FROM [UserGroup] ORDER BY Id ASC";
            return await RepositoryService.ExecuteReader<UserGroup>(sql);
        }
        public async Task<List<UserGroup>> SelectUserGroupWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [UserGroup] ORDER BY Id ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await RepositoryService.ExecuteReader<UserGroup>(sql, parameters);
        }
        public async Task<UserGroup?> GetUserGroupByName(string name)
        {
            var sql = @"SELECT * FROM [UserGroup] WHERE Name = @Name ORDER BY Id ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await RepositoryService.ExecuteReader<UserGroup>(sql, parameters)).SingleOrDefault();
        }
        public async Task<UserGroup> RequireUserGroupByName(string name)
        {
            return await GetUserGroupByName(name) ?? throw new InvalidOperationException("The specified UserGroup does not exist");
        }
    }
}

