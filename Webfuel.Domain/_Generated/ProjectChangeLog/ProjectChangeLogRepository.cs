using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain
{
    internal partial interface IProjectChangeLogRepository
    {
        Task<ProjectChangeLog> InsertProjectChangeLog(ProjectChangeLog entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<ProjectChangeLog> UpdateProjectChangeLog(ProjectChangeLog entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<ProjectChangeLog> UpdateProjectChangeLog(ProjectChangeLog updated, ProjectChangeLog original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteProjectChangeLog(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<ProjectChangeLog>> QueryProjectChangeLog(Query query, bool countTotal = true);
        Task<ProjectChangeLog?> GetProjectChangeLog(Guid id);
        Task<ProjectChangeLog> RequireProjectChangeLog(Guid id);
        Task<int> CountProjectChangeLog();
        Task<List<ProjectChangeLog>> SelectProjectChangeLog();
        Task<List<ProjectChangeLog>> SelectProjectChangeLogWithPage(int skip, int take);
        Task<List<ProjectChangeLog>> SelectProjectChangeLogByProjectId(Guid projectId);
    }
    [Service(typeof(IProjectChangeLogRepository))]
    internal partial class ProjectChangeLogRepository: IProjectChangeLogRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public ProjectChangeLogRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<ProjectChangeLog> InsertProjectChangeLog(ProjectChangeLog entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            ProjectChangeLogMetadata.Validate(entity);
            var sql = ProjectChangeLogMetadata.InsertSQL();
            var parameters = ProjectChangeLogMetadata.ExtractParameters(entity, ProjectChangeLogMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<ProjectChangeLog> UpdateProjectChangeLog(ProjectChangeLog entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            ProjectChangeLogMetadata.Validate(entity);
            var sql = ProjectChangeLogMetadata.UpdateSQL();
            var parameters = ProjectChangeLogMetadata.ExtractParameters(entity, ProjectChangeLogMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<ProjectChangeLog> UpdateProjectChangeLog(ProjectChangeLog updated, ProjectChangeLog original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateProjectChangeLog(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteProjectChangeLog(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = ProjectChangeLogMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<ProjectChangeLog>> QueryProjectChangeLog(Query query, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<ProjectChangeLog, ProjectChangeLogMetadata>(query, countTotal);
        }
        public async Task<ProjectChangeLog?> GetProjectChangeLog(Guid id)
        {
            var sql = @"SELECT * FROM [ProjectChangeLog] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<ProjectChangeLog, ProjectChangeLogMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<ProjectChangeLog> RequireProjectChangeLog(Guid id)
        {
            return await GetProjectChangeLog(id) ?? throw new InvalidOperationException("The specified ProjectChangeLog does not exist");
        }
        public async Task<int> CountProjectChangeLog()
        {
            var sql = @"SELECT COUNT(Id) FROM [ProjectChangeLog]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<ProjectChangeLog>> SelectProjectChangeLog()
        {
            var sql = @"SELECT * FROM [ProjectChangeLog] ORDER BY Id DESC";
            return await _connection.ExecuteReader<ProjectChangeLog, ProjectChangeLogMetadata>(sql);
        }
        public async Task<List<ProjectChangeLog>> SelectProjectChangeLogWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [ProjectChangeLog] ORDER BY Id DESC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<ProjectChangeLog, ProjectChangeLogMetadata>(sql, parameters);
        }
        public async Task<List<ProjectChangeLog>> SelectProjectChangeLogByProjectId(Guid projectId)
        {
            var sql = @"SELECT * FROM [ProjectChangeLog] WHERE ProjectId = @ProjectId ORDER BY Id DESC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@ProjectId", projectId),
            };
            return await _connection.ExecuteReader<ProjectChangeLog, ProjectChangeLogMetadata>(sql, parameters);
        }
    }
}

