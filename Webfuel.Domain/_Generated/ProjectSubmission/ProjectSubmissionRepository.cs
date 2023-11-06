using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain
{
    internal partial interface IProjectSubmissionRepository
    {
        Task<ProjectSubmission> InsertProjectSubmission(ProjectSubmission entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<ProjectSubmission> UpdateProjectSubmission(ProjectSubmission entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<ProjectSubmission> UpdateProjectSubmission(ProjectSubmission updated, ProjectSubmission original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteProjectSubmission(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<ProjectSubmission>> QueryProjectSubmission(Query query);
        Task<ProjectSubmission?> GetProjectSubmission(Guid id);
        Task<ProjectSubmission> RequireProjectSubmission(Guid id);
        Task<int> CountProjectSubmission();
        Task<List<ProjectSubmission>> SelectProjectSubmission();
        Task<List<ProjectSubmission>> SelectProjectSubmissionWithPage(int skip, int take);
        Task<List<ProjectSubmission>> SelectProjectSubmissionBySubmissionDate(DateOnly submissionDate);
        Task<List<ProjectSubmission>> SelectProjectSubmissionByProjectId(Guid projectId);
    }
    [Service(typeof(IProjectSubmissionRepository))]
    internal partial class ProjectSubmissionRepository: IProjectSubmissionRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public ProjectSubmissionRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<ProjectSubmission> InsertProjectSubmission(ProjectSubmission entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            var sql = ProjectSubmissionMetadata.InsertSQL();
            var parameters = ProjectSubmissionMetadata.ExtractParameters(entity, ProjectSubmissionMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<ProjectSubmission> UpdateProjectSubmission(ProjectSubmission entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = ProjectSubmissionMetadata.UpdateSQL();
            var parameters = ProjectSubmissionMetadata.ExtractParameters(entity, ProjectSubmissionMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<ProjectSubmission> UpdateProjectSubmission(ProjectSubmission updated, ProjectSubmission original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateProjectSubmission(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteProjectSubmission(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = ProjectSubmissionMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<ProjectSubmission>> QueryProjectSubmission(Query query)
        {
            return await _connection.ExecuteQuery<ProjectSubmission, ProjectSubmissionMetadata>(query);
        }
        public async Task<ProjectSubmission?> GetProjectSubmission(Guid id)
        {
            var sql = @"SELECT * FROM [ProjectSubmission] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<ProjectSubmission, ProjectSubmissionMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<ProjectSubmission> RequireProjectSubmission(Guid id)
        {
            return await GetProjectSubmission(id) ?? throw new InvalidOperationException("The specified ProjectSubmission does not exist");
        }
        public async Task<int> CountProjectSubmission()
        {
            var sql = @"SELECT COUNT(Id) FROM [ProjectSubmission]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<ProjectSubmission>> SelectProjectSubmission()
        {
            var sql = @"SELECT * FROM [ProjectSubmission] ORDER BY SubmissionDate DESC";
            return await _connection.ExecuteReader<ProjectSubmission, ProjectSubmissionMetadata>(sql);
        }
        public async Task<List<ProjectSubmission>> SelectProjectSubmissionWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [ProjectSubmission] ORDER BY SubmissionDate DESC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<ProjectSubmission, ProjectSubmissionMetadata>(sql, parameters);
        }
        public async Task<List<ProjectSubmission>> SelectProjectSubmissionBySubmissionDate(DateOnly submissionDate)
        {
            var sql = @"SELECT * FROM [ProjectSubmission] WHERE SubmissionDate = @SubmissionDate ORDER BY SubmissionDate DESC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@SubmissionDate", submissionDate),
            };
            return await _connection.ExecuteReader<ProjectSubmission, ProjectSubmissionMetadata>(sql, parameters);
        }
        public async Task<List<ProjectSubmission>> SelectProjectSubmissionByProjectId(Guid projectId)
        {
            var sql = @"SELECT * FROM [ProjectSubmission] WHERE ProjectId = @ProjectId ORDER BY SubmissionDate DESC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@ProjectId", projectId),
            };
            return await _connection.ExecuteReader<ProjectSubmission, ProjectSubmissionMetadata>(sql, parameters);
        }
    }
}

