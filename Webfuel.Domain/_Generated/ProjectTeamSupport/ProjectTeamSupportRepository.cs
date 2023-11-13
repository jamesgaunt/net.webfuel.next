using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain
{
    internal partial interface IProjectTeamSupportRepository
    {
        Task<ProjectTeamSupport> InsertProjectTeamSupport(ProjectTeamSupport entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<ProjectTeamSupport> UpdateProjectTeamSupport(ProjectTeamSupport entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<ProjectTeamSupport> UpdateProjectTeamSupport(ProjectTeamSupport updated, ProjectTeamSupport original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteProjectTeamSupport(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<ProjectTeamSupport>> QueryProjectTeamSupport(Query query);
        Task<ProjectTeamSupport?> GetProjectTeamSupport(Guid id);
        Task<ProjectTeamSupport> RequireProjectTeamSupport(Guid id);
        Task<int> CountProjectTeamSupport();
        Task<List<ProjectTeamSupport>> SelectProjectTeamSupport();
        Task<List<ProjectTeamSupport>> SelectProjectTeamSupportWithPage(int skip, int take);
        Task<List<ProjectTeamSupport>> SelectProjectTeamSupportBySupportTeamId(Guid supportTeamId);
        Task<List<ProjectTeamSupport>> SelectProjectTeamSupportByProjectId(Guid projectId);
    }
    [Service(typeof(IProjectTeamSupportRepository))]
    internal partial class ProjectTeamSupportRepository: IProjectTeamSupportRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public ProjectTeamSupportRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<ProjectTeamSupport> InsertProjectTeamSupport(ProjectTeamSupport entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            ProjectTeamSupportMetadata.Validate(entity);
            var sql = ProjectTeamSupportMetadata.InsertSQL();
            var parameters = ProjectTeamSupportMetadata.ExtractParameters(entity, ProjectTeamSupportMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<ProjectTeamSupport> UpdateProjectTeamSupport(ProjectTeamSupport entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            ProjectTeamSupportMetadata.Validate(entity);
            var sql = ProjectTeamSupportMetadata.UpdateSQL();
            var parameters = ProjectTeamSupportMetadata.ExtractParameters(entity, ProjectTeamSupportMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<ProjectTeamSupport> UpdateProjectTeamSupport(ProjectTeamSupport updated, ProjectTeamSupport original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateProjectTeamSupport(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteProjectTeamSupport(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = ProjectTeamSupportMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<ProjectTeamSupport>> QueryProjectTeamSupport(Query query)
        {
            return await _connection.ExecuteQuery<ProjectTeamSupport, ProjectTeamSupportMetadata>(query);
        }
        public async Task<ProjectTeamSupport?> GetProjectTeamSupport(Guid id)
        {
            var sql = @"SELECT * FROM [ProjectTeamSupport] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<ProjectTeamSupport, ProjectTeamSupportMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<ProjectTeamSupport> RequireProjectTeamSupport(Guid id)
        {
            return await GetProjectTeamSupport(id) ?? throw new InvalidOperationException("The specified ProjectTeamSupport does not exist");
        }
        public async Task<int> CountProjectTeamSupport()
        {
            var sql = @"SELECT COUNT(Id) FROM [ProjectTeamSupport]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<ProjectTeamSupport>> SelectProjectTeamSupport()
        {
            var sql = @"SELECT * FROM [ProjectTeamSupport] ORDER BY CreatedAt DESC";
            return await _connection.ExecuteReader<ProjectTeamSupport, ProjectTeamSupportMetadata>(sql);
        }
        public async Task<List<ProjectTeamSupport>> SelectProjectTeamSupportWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [ProjectTeamSupport] ORDER BY CreatedAt DESC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<ProjectTeamSupport, ProjectTeamSupportMetadata>(sql, parameters);
        }
        public async Task<List<ProjectTeamSupport>> SelectProjectTeamSupportBySupportTeamId(Guid supportTeamId)
        {
            var sql = @"SELECT * FROM [ProjectTeamSupport] WHERE SupportTeamId = @SupportTeamId ORDER BY CreatedAt DESC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@SupportTeamId", supportTeamId),
            };
            return await _connection.ExecuteReader<ProjectTeamSupport, ProjectTeamSupportMetadata>(sql, parameters);
        }
        public async Task<List<ProjectTeamSupport>> SelectProjectTeamSupportByProjectId(Guid projectId)
        {
            var sql = @"SELECT * FROM [ProjectTeamSupport] WHERE ProjectId = @ProjectId ORDER BY CreatedAt DESC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@ProjectId", projectId),
            };
            return await _connection.ExecuteReader<ProjectTeamSupport, ProjectTeamSupportMetadata>(sql, parameters);
        }
    }
}

