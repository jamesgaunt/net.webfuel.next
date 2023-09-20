using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.Common
{
    internal partial interface IUserRepository
    {
        Task<User> InsertUserAsync(User entity);
        Task<User> UpdateUserAsync(User entity);
        Task<User> UpdateUserAsync(User entity, IEnumerable<string> properties);
        Task<User> UpdateUserAsync(User updated, User original);
        Task<User> UpdateUserAsync(User updated, User original, IEnumerable<string> properties);
        Task DeleteUserAsync(Guid key);
        Task<QueryResult<User>> QueryUserAsync(RepositoryQuery query);
        Task<User?> GetUserAsync(Guid id);
        Task<User> RequireUserAsync(Guid id);
        Task<int> CountUserAsync();
        Task<List<User>> SelectUserAsync();
        Task<List<User>> SelectUserWithPageAsync(int skip, int take);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User> RequireUserByEmailAsync(string email);
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
        public async Task<User> InsertUserAsync(User entity)
        {
            return await RepositoryService.ExecuteInsertAsync(entity);
        }
        public async Task<User> UpdateUserAsync(User entity)
        {
            return await RepositoryService.ExecuteUpdateAsync(entity);
        }
        public async Task<User> UpdateUserAsync(User entity, IEnumerable<string> properties)
        {
            return await RepositoryService.ExecuteUpdateAsync(entity, properties);
        }
        public async Task<User> UpdateUserAsync(User updated, User original)
        {
            if(updated.Id != original.Id) throw new InvalidOperationException("UpdateUserAsync: Entity keys do not match.");
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
            if(_properties.Count == 0) return updated;
            return await RepositoryService.ExecuteUpdateAsync(updated, _properties);
        }
        public async Task<User> UpdateUserAsync(User updated, User original, IEnumerable<string> properties)
        {
            if(updated.Id != original.Id) throw new InvalidOperationException("UpdateUserAsync: Entity keys do not match.");
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
            if(_properties.Count == 0) return updated;
            return await RepositoryService.ExecuteUpdateAsync(updated, _properties);
        }
        public async Task DeleteUserAsync(Guid key)
        {
            await RepositoryService.ExecuteDeleteAsync<User>(key);
        }
        public async Task<QueryResult<User>> QueryUserAsync(RepositoryQuery query)
        {
            return await RepositoryQueryService.ExecuteQueryAsync(query, new UserRepositoryAccessor());
        }
        public async Task<User?> GetUserAsync(Guid id)
        {
            var sql = @"SELECT * FROM [next].[User] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await RepositoryService.ExecuteReaderAsync<User>(sql, parameters)).SingleOrDefault();
        }
        public async Task<User> RequireUserAsync(Guid id)
        {
            return await GetUserAsync(id) ?? throw new InvalidOperationException("The specified User does not exist");
        }
        public async Task<int> CountUserAsync()
        {
            var sql = @"SELECT COUNT(Id) FROM [next].[User]";
            return (int)((await RepositoryService.ExecuteScalarAsync(sql))!);
        }
        public async Task<List<User>> SelectUserAsync()
        {
            var sql = @"SELECT * FROM [next].[User] ORDER BY Id ASC";
            return await RepositoryService.ExecuteReaderAsync<User>(sql);
        }
        public async Task<List<User>> SelectUserWithPageAsync(int skip, int take)
        {
            var sql = @"SELECT * FROM [next].[User] ORDER BY Id ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await RepositoryService.ExecuteReaderAsync<User>(sql, parameters);
        }
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var sql = @"SELECT * FROM [next].[User] WHERE Email = @Email ORDER BY Id ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Email", email),
            };
            return (await RepositoryService.ExecuteReaderAsync<User>(sql, parameters)).SingleOrDefault();
        }
        public async Task<User> RequireUserByEmailAsync(string email)
        {
            return await GetUserByEmailAsync(email) ?? throw new InvalidOperationException("The specified User does not exist");
        }
    }
}

