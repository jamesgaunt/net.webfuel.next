using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain
{
    internal partial interface IUserGroupRepository
    {
        Task<UserGroup> InsertUserGroup(UserGroup entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<UserGroup> UpdateUserGroup(UserGroup entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<UserGroup> UpdateUserGroup(UserGroup updated, UserGroup original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteUserGroup(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<UserGroup>> QueryUserGroup(Query query, bool selectItems = true, bool countTotal = true);
        Task<UserGroup?> GetUserGroup(Guid id);
        Task<UserGroup> RequireUserGroup(Guid id);
        Task<int> CountUserGroup();
        Task<List<UserGroup>> SelectUserGroup();
        Task<List<UserGroup>> SelectUserGroupWithPage(int skip, int take);
        Task<UserGroup?> GetUserGroupByName(string name);
        Task<UserGroup> RequireUserGroupByName(string name);
    }
    [Service(typeof(IUserGroupRepository))]
    internal partial class UserGroupRepository: IUserGroupRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public UserGroupRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<UserGroup> InsertUserGroup(UserGroup entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            UserGroupMetadata.Validate(entity);
            var sql = UserGroupMetadata.InsertSQL();
            var parameters = UserGroupMetadata.ExtractParameters(entity, UserGroupMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<UserGroup> UpdateUserGroup(UserGroup entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            UserGroupMetadata.Validate(entity);
            var sql = UserGroupMetadata.UpdateSQL();
            var parameters = UserGroupMetadata.ExtractParameters(entity, UserGroupMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<UserGroup> UpdateUserGroup(UserGroup updated, UserGroup original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateUserGroup(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteUserGroup(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = UserGroupMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<UserGroup>> QueryUserGroup(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<UserGroup, UserGroupMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<UserGroup?> GetUserGroup(Guid id)
        {
            var sql = @"SELECT * FROM [UserGroup] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<UserGroup, UserGroupMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<UserGroup> RequireUserGroup(Guid id)
        {
            return await GetUserGroup(id) ?? throw new InvalidOperationException("The specified UserGroup does not exist");
        }
        public async Task<int> CountUserGroup()
        {
            var sql = @"SELECT COUNT(Id) FROM [UserGroup]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<UserGroup>> SelectUserGroup()
        {
            var sql = @"SELECT * FROM [UserGroup] ORDER BY Id ASC";
            return await _connection.ExecuteReader<UserGroup, UserGroupMetadata>(sql);
        }
        public async Task<List<UserGroup>> SelectUserGroupWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [UserGroup] ORDER BY Id ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<UserGroup, UserGroupMetadata>(sql, parameters);
        }
        public async Task<UserGroup?> GetUserGroupByName(string name)
        {
            var sql = @"SELECT * FROM [UserGroup] WHERE Name = @Name ORDER BY Id ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<UserGroup, UserGroupMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<UserGroup> RequireUserGroupByName(string name)
        {
            return await GetUserGroupByName(name) ?? throw new InvalidOperationException("The specified UserGroup does not exist");
        }
    }
}

