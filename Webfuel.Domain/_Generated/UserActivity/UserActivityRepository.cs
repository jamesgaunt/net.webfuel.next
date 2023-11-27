using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain
{
    internal partial interface IUserActivityRepository
    {
        Task<UserActivity> InsertUserActivity(UserActivity entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<UserActivity> UpdateUserActivity(UserActivity entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<UserActivity> UpdateUserActivity(UserActivity updated, UserActivity original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteUserActivity(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<UserActivity>> QueryUserActivity(Query query, bool countTotal = true);
        Task<UserActivity?> GetUserActivity(Guid id);
        Task<UserActivity> RequireUserActivity(Guid id);
        Task<int> CountUserActivity();
        Task<List<UserActivity>> SelectUserActivity();
        Task<List<UserActivity>> SelectUserActivityWithPage(int skip, int take);
        Task<List<UserActivity>> SelectUserActivityByDate(DateOnly date);
        Task<List<UserActivity>> SelectUserActivityByProjectSupportId(Guid? projectSupportId);
        Task<List<UserActivity>> SelectUserActivityByProjectId(Guid? projectId);
        Task<List<UserActivity>> SelectUserActivityByUserId(Guid userId);
    }
    [Service(typeof(IUserActivityRepository))]
    internal partial class UserActivityRepository: IUserActivityRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public UserActivityRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<UserActivity> InsertUserActivity(UserActivity entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            UserActivityMetadata.Validate(entity);
            var sql = UserActivityMetadata.InsertSQL();
            var parameters = UserActivityMetadata.ExtractParameters(entity, UserActivityMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<UserActivity> UpdateUserActivity(UserActivity entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            UserActivityMetadata.Validate(entity);
            var sql = UserActivityMetadata.UpdateSQL();
            var parameters = UserActivityMetadata.ExtractParameters(entity, UserActivityMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<UserActivity> UpdateUserActivity(UserActivity updated, UserActivity original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateUserActivity(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteUserActivity(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = UserActivityMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<UserActivity>> QueryUserActivity(Query query, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<UserActivity, UserActivityMetadata>(query, countTotal);
        }
        public async Task<UserActivity?> GetUserActivity(Guid id)
        {
            var sql = @"SELECT * FROM [UserActivity] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<UserActivity, UserActivityMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<UserActivity> RequireUserActivity(Guid id)
        {
            return await GetUserActivity(id) ?? throw new InvalidOperationException("The specified UserActivity does not exist");
        }
        public async Task<int> CountUserActivity()
        {
            var sql = @"SELECT COUNT(Id) FROM [UserActivity]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<UserActivity>> SelectUserActivity()
        {
            var sql = @"SELECT * FROM [UserActivity] ORDER BY Date DESC";
            return await _connection.ExecuteReader<UserActivity, UserActivityMetadata>(sql);
        }
        public async Task<List<UserActivity>> SelectUserActivityWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [UserActivity] ORDER BY Date DESC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<UserActivity, UserActivityMetadata>(sql, parameters);
        }
        public async Task<List<UserActivity>> SelectUserActivityByDate(DateOnly date)
        {
            var sql = @"SELECT * FROM [UserActivity] WHERE Date = @Date ORDER BY Date DESC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Date", date),
            };
            return await _connection.ExecuteReader<UserActivity, UserActivityMetadata>(sql, parameters);
        }
        public async Task<List<UserActivity>> SelectUserActivityByProjectSupportId(Guid? projectSupportId)
        {
            var sql = @"SELECT * FROM [UserActivity] WHERE ((ProjectSupportId = @ProjectSupportId) OR (ProjectSupportId IS NULL AND @ProjectSupportId IS NULL)) ORDER BY Date DESC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@ProjectSupportId", (object?)projectSupportId ?? DBNull.Value),
            };
            return await _connection.ExecuteReader<UserActivity, UserActivityMetadata>(sql, parameters);
        }
        public async Task<List<UserActivity>> SelectUserActivityByProjectId(Guid? projectId)
        {
            var sql = @"SELECT * FROM [UserActivity] WHERE ((ProjectId = @ProjectId) OR (ProjectId IS NULL AND @ProjectId IS NULL)) ORDER BY Date DESC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@ProjectId", (object?)projectId ?? DBNull.Value),
            };
            return await _connection.ExecuteReader<UserActivity, UserActivityMetadata>(sql, parameters);
        }
        public async Task<List<UserActivity>> SelectUserActivityByUserId(Guid userId)
        {
            var sql = @"SELECT * FROM [UserActivity] WHERE UserId = @UserId ORDER BY Date DESC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@UserId", userId),
            };
            return await _connection.ExecuteReader<UserActivity, UserActivityMetadata>(sql, parameters);
        }
    }
}

