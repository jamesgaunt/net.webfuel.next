using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IWorkActivityRepository
    {
        Task<WorkActivity> InsertWorkActivity(WorkActivity entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<WorkActivity> UpdateWorkActivity(WorkActivity entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<WorkActivity> UpdateWorkActivity(WorkActivity updated, WorkActivity original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteWorkActivity(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<WorkActivity>> QueryWorkActivity(Query query);
        Task<WorkActivity?> GetWorkActivity(Guid id);
        Task<WorkActivity> RequireWorkActivity(Guid id);
        Task<int> CountWorkActivity();
        Task<List<WorkActivity>> SelectWorkActivity();
        Task<List<WorkActivity>> SelectWorkActivityWithPage(int skip, int take);
        Task<WorkActivity?> GetWorkActivityByName(string name);
        Task<WorkActivity> RequireWorkActivityByName(string name);
    }
    [Service(typeof(IWorkActivityRepository))]
    internal partial class WorkActivityRepository: IWorkActivityRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public WorkActivityRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<WorkActivity> InsertWorkActivity(WorkActivity entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            var sql = WorkActivityMetadata.InsertSQL();
            var parameters = WorkActivityMetadata.ExtractParameters(entity, WorkActivityMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<WorkActivity> UpdateWorkActivity(WorkActivity entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = WorkActivityMetadata.UpdateSQL();
            var parameters = WorkActivityMetadata.ExtractParameters(entity, WorkActivityMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<WorkActivity> UpdateWorkActivity(WorkActivity updated, WorkActivity original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateWorkActivity(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteWorkActivity(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = WorkActivityMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<WorkActivity>> QueryWorkActivity(Query query)
        {
            return await _connection.ExecuteQuery<WorkActivity, WorkActivityMetadata>(query);
        }
        public async Task<WorkActivity?> GetWorkActivity(Guid id)
        {
            var sql = @"SELECT * FROM [WorkActivity] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<WorkActivity, WorkActivityMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<WorkActivity> RequireWorkActivity(Guid id)
        {
            return await GetWorkActivity(id) ?? throw new InvalidOperationException("The specified WorkActivity does not exist");
        }
        public async Task<int> CountWorkActivity()
        {
            var sql = @"SELECT COUNT(Id) FROM [WorkActivity]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<WorkActivity>> SelectWorkActivity()
        {
            var sql = @"SELECT * FROM [WorkActivity] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<WorkActivity, WorkActivityMetadata>(sql);
        }
        public async Task<List<WorkActivity>> SelectWorkActivityWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [WorkActivity] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<WorkActivity, WorkActivityMetadata>(sql, parameters);
        }
        public async Task<WorkActivity?> GetWorkActivityByName(string name)
        {
            var sql = @"SELECT * FROM [WorkActivity] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<WorkActivity, WorkActivityMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<WorkActivity> RequireWorkActivityByName(string name)
        {
            return await GetWorkActivityByName(name) ?? throw new InvalidOperationException("The specified WorkActivity does not exist");
        }
    }
}

