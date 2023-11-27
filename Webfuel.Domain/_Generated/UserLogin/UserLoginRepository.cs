using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain
{
    internal partial interface IUserLoginRepository
    {
        Task<UserLogin> InsertUserLogin(UserLogin entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<UserLogin> UpdateUserLogin(UserLogin entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<UserLogin> UpdateUserLogin(UserLogin updated, UserLogin original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteUserLogin(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<UserLogin>> QueryUserLogin(Query query, bool countTotal = true);
        Task<UserLogin?> GetUserLogin(Guid id);
        Task<UserLogin> RequireUserLogin(Guid id);
        Task<int> CountUserLogin();
        Task<List<UserLogin>> SelectUserLogin();
        Task<List<UserLogin>> SelectUserLoginWithPage(int skip, int take);
    }
    [Service(typeof(IUserLoginRepository))]
    internal partial class UserLoginRepository: IUserLoginRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public UserLoginRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<UserLogin> InsertUserLogin(UserLogin entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            UserLoginMetadata.Validate(entity);
            var sql = UserLoginMetadata.InsertSQL();
            var parameters = UserLoginMetadata.ExtractParameters(entity, UserLoginMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<UserLogin> UpdateUserLogin(UserLogin entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            UserLoginMetadata.Validate(entity);
            var sql = UserLoginMetadata.UpdateSQL();
            var parameters = UserLoginMetadata.ExtractParameters(entity, UserLoginMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<UserLogin> UpdateUserLogin(UserLogin updated, UserLogin original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateUserLogin(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteUserLogin(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = UserLoginMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<UserLogin>> QueryUserLogin(Query query, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<UserLogin, UserLoginMetadata>(query, countTotal);
        }
        public async Task<UserLogin?> GetUserLogin(Guid id)
        {
            var sql = @"SELECT * FROM [UserLogin] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<UserLogin, UserLoginMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<UserLogin> RequireUserLogin(Guid id)
        {
            return await GetUserLogin(id) ?? throw new InvalidOperationException("The specified UserLogin does not exist");
        }
        public async Task<int> CountUserLogin()
        {
            var sql = @"SELECT COUNT(Id) FROM [UserLogin]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<UserLogin>> SelectUserLogin()
        {
            var sql = @"SELECT * FROM [UserLogin] ORDER BY Id DESC";
            return await _connection.ExecuteReader<UserLogin, UserLoginMetadata>(sql);
        }
        public async Task<List<UserLogin>> SelectUserLoginWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [UserLogin] ORDER BY Id DESC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<UserLogin, UserLoginMetadata>(sql, parameters);
        }
    }
}

