using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IUserDisciplineRepository
    {
        Task<UserDiscipline> InsertUserDiscipline(UserDiscipline entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<UserDiscipline> UpdateUserDiscipline(UserDiscipline entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<UserDiscipline> UpdateUserDiscipline(UserDiscipline updated, UserDiscipline original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteUserDiscipline(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<UserDiscipline>> QueryUserDiscipline(Query query);
        Task<UserDiscipline?> GetUserDiscipline(Guid id);
        Task<UserDiscipline> RequireUserDiscipline(Guid id);
        Task<int> CountUserDiscipline();
        Task<List<UserDiscipline>> SelectUserDiscipline();
        Task<List<UserDiscipline>> SelectUserDisciplineWithPage(int skip, int take);
        Task<UserDiscipline?> GetUserDisciplineByName(string name);
        Task<UserDiscipline> RequireUserDisciplineByName(string name);
    }
    [Service(typeof(IUserDisciplineRepository))]
    internal partial class UserDisciplineRepository: IUserDisciplineRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public UserDisciplineRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<UserDiscipline> InsertUserDiscipline(UserDiscipline entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            var sql = UserDisciplineMetadata.InsertSQL();
            var parameters = UserDisciplineMetadata.ExtractParameters(entity, UserDisciplineMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<UserDiscipline> UpdateUserDiscipline(UserDiscipline entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = UserDisciplineMetadata.UpdateSQL();
            var parameters = UserDisciplineMetadata.ExtractParameters(entity, UserDisciplineMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<UserDiscipline> UpdateUserDiscipline(UserDiscipline updated, UserDiscipline original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateUserDiscipline(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteUserDiscipline(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = UserDisciplineMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<UserDiscipline>> QueryUserDiscipline(Query query)
        {
            return await _connection.ExecuteQuery<UserDiscipline, UserDisciplineMetadata>(query);
        }
        public async Task<UserDiscipline?> GetUserDiscipline(Guid id)
        {
            var sql = @"SELECT * FROM [UserDiscipline] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<UserDiscipline, UserDisciplineMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<UserDiscipline> RequireUserDiscipline(Guid id)
        {
            return await GetUserDiscipline(id) ?? throw new InvalidOperationException("The specified UserDiscipline does not exist");
        }
        public async Task<int> CountUserDiscipline()
        {
            var sql = @"SELECT COUNT(Id) FROM [UserDiscipline]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<UserDiscipline>> SelectUserDiscipline()
        {
            var sql = @"SELECT * FROM [UserDiscipline] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<UserDiscipline, UserDisciplineMetadata>(sql);
        }
        public async Task<List<UserDiscipline>> SelectUserDisciplineWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [UserDiscipline] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<UserDiscipline, UserDisciplineMetadata>(sql, parameters);
        }
        public async Task<UserDiscipline?> GetUserDisciplineByName(string name)
        {
            var sql = @"SELECT * FROM [UserDiscipline] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<UserDiscipline, UserDisciplineMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<UserDiscipline> RequireUserDisciplineByName(string name)
        {
            return await GetUserDisciplineByName(name) ?? throw new InvalidOperationException("The specified UserDiscipline does not exist");
        }
    }
}

