using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain
{
    internal partial interface IUserRepository
    {
        Task<User> InsertUser(User entity);
        Task<User> UpdateUser(User entity);
        Task<User> UpdateUser(User entity, IEnumerable<string> properties);
        Task<User> UpdateUser(User updated, User original);
        Task<User> UpdateUser(User updated, User original, IEnumerable<string> properties);
        Task DeleteUser(Guid key);
        Task<QueryResult<User>> QueryUser(Query query);
        Task<User?> GetUser(Guid id);
        Task<User> RequireUser(Guid id);
        Task<int> CountUser();
        Task<List<User>> SelectUser();
        Task<List<User>> SelectUserWithPage(int skip, int take);
        Task<User?> GetUserByEmail(string email);
        Task<User> RequireUserByEmail(string email);
    }
    internal partial class UserRepository: IUserRepository
    {
        private readonly IRepositoryService RepositoryService;
        private readonly IRepositoryQueryService RepositoryQueryService;
        public UserRepository(IRepositoryService repositoryService, IRepositoryQueryService repositoryQueryService)
        {
            RepositoryService = repositoryService;
            RepositoryQueryService = repositoryQueryService;
        }
        public async Task<User> InsertUser(User entity)
        {
            return await RepositoryService.ExecuteInsert(entity);
        }
        public async Task<User> UpdateUser(User entity)
        {
            return await RepositoryService.ExecuteUpdate(entity);
        }
        public async Task<User> UpdateUser(User entity, IEnumerable<string> properties)
        {
            return await RepositoryService.ExecuteUpdate(entity, properties);
        }
        public async Task<User> UpdateUser(User updated, User original)
        {
            if(updated.Id != original.Id) throw new InvalidOperationException("UpdateUser: Entity keys do not match.");
            var _properties = new List<string>();
            if(updated.Email != original.Email) _properties.Add("Email");
            if(updated.FirstName != original.FirstName) _properties.Add("FirstName");
            if(updated.LastName != original.LastName) _properties.Add("LastName");
            if(updated.PasswordHash != original.PasswordHash) _properties.Add("PasswordHash");
            if(updated.PasswordSalt != original.PasswordSalt) _properties.Add("PasswordSalt");
            if(updated.PasswordResetAt != original.PasswordResetAt) _properties.Add("PasswordResetAt");
            if(updated.PasswordResetToken != original.PasswordResetToken) _properties.Add("PasswordResetToken");
            if(updated.PasswordResetValidUntil != original.PasswordResetValidUntil) _properties.Add("PasswordResetValidUntil");
            if(updated.Developer != original.Developer) _properties.Add("Developer");
            if(updated.Birthday != original.Birthday) _properties.Add("Birthday");
            if(updated.CreatedAt != original.CreatedAt) _properties.Add("CreatedAt");
            if(updated.UserGroupId != original.UserGroupId) _properties.Add("UserGroupId");
            if(_properties.Count == 0) return updated;
            return await RepositoryService.ExecuteUpdate(updated, _properties);
        }
        public async Task<User> UpdateUser(User updated, User original, IEnumerable<string> properties)
        {
            if(updated.Id != original.Id) throw new InvalidOperationException("UpdateUser: Entity keys do not match.");
            var _properties = new List<string>();
            if(properties.Contains("Email") && updated.Email != original.Email) _properties.Add("Email");
            if(properties.Contains("FirstName") && updated.FirstName != original.FirstName) _properties.Add("FirstName");
            if(properties.Contains("LastName") && updated.LastName != original.LastName) _properties.Add("LastName");
            if(properties.Contains("PasswordHash") && updated.PasswordHash != original.PasswordHash) _properties.Add("PasswordHash");
            if(properties.Contains("PasswordSalt") && updated.PasswordSalt != original.PasswordSalt) _properties.Add("PasswordSalt");
            if(properties.Contains("PasswordResetAt") && updated.PasswordResetAt != original.PasswordResetAt) _properties.Add("PasswordResetAt");
            if(properties.Contains("PasswordResetToken") && updated.PasswordResetToken != original.PasswordResetToken) _properties.Add("PasswordResetToken");
            if(properties.Contains("PasswordResetValidUntil") && updated.PasswordResetValidUntil != original.PasswordResetValidUntil) _properties.Add("PasswordResetValidUntil");
            if(properties.Contains("Developer") && updated.Developer != original.Developer) _properties.Add("Developer");
            if(properties.Contains("Birthday") && updated.Birthday != original.Birthday) _properties.Add("Birthday");
            if(properties.Contains("CreatedAt") && updated.CreatedAt != original.CreatedAt) _properties.Add("CreatedAt");
            if(properties.Contains("UserGroupId") && updated.UserGroupId != original.UserGroupId) _properties.Add("UserGroupId");
            if(_properties.Count == 0) return updated;
            return await RepositoryService.ExecuteUpdate(updated, _properties);
        }
        public async Task DeleteUser(Guid key)
        {
            await RepositoryService.ExecuteDelete<User>(key);
        }
        public async Task<QueryResult<User>> QueryUser(Query query)
        {
            return await RepositoryQueryService.ExecuteQuery(query, new UserRepositoryAccessor());
        }
        public async Task<User?> GetUser(Guid id)
        {
            var sql = @"SELECT * FROM [User] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await RepositoryService.ExecuteReader<User>(sql, parameters)).SingleOrDefault();
        }
        public async Task<User> RequireUser(Guid id)
        {
            return await GetUser(id) ?? throw new InvalidOperationException("The specified User does not exist");
        }
        public async Task<int> CountUser()
        {
            var sql = @"SELECT COUNT(Id) FROM [User]";
            return (int)((await RepositoryService.ExecuteScalar(sql))!);
        }
        public async Task<List<User>> SelectUser()
        {
            var sql = @"SELECT * FROM [User] ORDER BY Id ASC";
            return await RepositoryService.ExecuteReader<User>(sql);
        }
        public async Task<List<User>> SelectUserWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [User] ORDER BY Id ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await RepositoryService.ExecuteReader<User>(sql, parameters);
        }
        public async Task<User?> GetUserByEmail(string email)
        {
            var sql = @"SELECT * FROM [User] WHERE Email = @Email ORDER BY Id ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Email", email),
            };
            return (await RepositoryService.ExecuteReader<User>(sql, parameters)).SingleOrDefault();
        }
        public async Task<User> RequireUserByEmail(string email)
        {
            return await GetUserByEmail(email) ?? throw new InvalidOperationException("The specified User does not exist");
        }
    }
}

