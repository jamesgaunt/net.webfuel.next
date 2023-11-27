using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain
{
    internal partial interface IUserRepository
    {
        Task<User> InsertUser(User entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<User> UpdateUser(User entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<User> UpdateUser(User updated, User original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteUser(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<User>> QueryUser(Query query, bool countTotal = true);
        Task<User?> GetUser(Guid id);
        Task<User> RequireUser(Guid id);
        Task<int> CountUser();
        Task<List<User>> SelectUser();
        Task<List<User>> SelectUserWithPage(int skip, int take);
        Task<User?> GetUserByEmail(string email);
        Task<User> RequireUserByEmail(string email);
    }
    [Service(typeof(IUserRepository))]
    internal partial class UserRepository: IUserRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public UserRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<User> InsertUser(User entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            UserMetadata.Validate(entity);
            var sql = UserMetadata.InsertSQL();
            var parameters = UserMetadata.ExtractParameters(entity, UserMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<User> UpdateUser(User entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            UserMetadata.Validate(entity);
            var sql = UserMetadata.UpdateSQL();
            var parameters = UserMetadata.ExtractParameters(entity, UserMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<User> UpdateUser(User updated, User original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateUser(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteUser(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = UserMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<User>> QueryUser(Query query, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<User, UserMetadata>(query, countTotal);
        }
        public async Task<User?> GetUser(Guid id)
        {
            var sql = @"SELECT * FROM [User] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<User, UserMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<User> RequireUser(Guid id)
        {
            return await GetUser(id) ?? throw new InvalidOperationException("The specified User does not exist");
        }
        public async Task<int> CountUser()
        {
            var sql = @"SELECT COUNT(Id) FROM [User]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<User>> SelectUser()
        {
            var sql = @"SELECT * FROM [User] ORDER BY Id ASC";
            return await _connection.ExecuteReader<User, UserMetadata>(sql);
        }
        public async Task<List<User>> SelectUserWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [User] ORDER BY Id ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<User, UserMetadata>(sql, parameters);
        }
        public async Task<User?> GetUserByEmail(string email)
        {
            var sql = @"SELECT * FROM [User] WHERE Email = @Email ORDER BY Id ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Email", email),
            };
            return (await _connection.ExecuteReader<User, UserMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<User> RequireUserByEmail(string email)
        {
            return await GetUserByEmail(email) ?? throw new InvalidOperationException("The specified User does not exist");
        }
    }
}

