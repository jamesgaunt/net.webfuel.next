using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain
{
    internal partial interface IProjectAdviserRepository
    {
        Task<ProjectAdviser> InsertProjectAdviser(ProjectAdviser entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<ProjectAdviser> UpdateProjectAdviser(ProjectAdviser entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<ProjectAdviser> UpdateProjectAdviser(ProjectAdviser updated, ProjectAdviser original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteProjectAdviser(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<ProjectAdviser>> QueryProjectAdviser(Query query, bool selectItems = true, bool countTotal = true);
        Task<ProjectAdviser?> GetProjectAdviser(Guid id);
        Task<ProjectAdviser> RequireProjectAdviser(Guid id);
        Task<int> CountProjectAdviser();
        Task<List<ProjectAdviser>> SelectProjectAdviser();
        Task<List<ProjectAdviser>> SelectProjectAdviserWithPage(int skip, int take);
        Task<ProjectAdviser?> GetProjectAdviserByProjectIdAndUserId(Guid projectId, Guid userId);
        Task<ProjectAdviser> RequireProjectAdviserByProjectIdAndUserId(Guid projectId, Guid userId);
        Task<List<ProjectAdviser>> SelectProjectAdviserByProjectId(Guid projectId);
    }
    [Service(typeof(IProjectAdviserRepository))]
    internal partial class ProjectAdviserRepository: IProjectAdviserRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public ProjectAdviserRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<ProjectAdviser> InsertProjectAdviser(ProjectAdviser entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            ProjectAdviserMetadata.Validate(entity);
            var sql = ProjectAdviserMetadata.InsertSQL();
            var parameters = ProjectAdviserMetadata.ExtractParameters(entity, ProjectAdviserMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<ProjectAdviser> UpdateProjectAdviser(ProjectAdviser entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            ProjectAdviserMetadata.Validate(entity);
            var sql = ProjectAdviserMetadata.UpdateSQL();
            var parameters = ProjectAdviserMetadata.ExtractParameters(entity, ProjectAdviserMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<ProjectAdviser> UpdateProjectAdviser(ProjectAdviser updated, ProjectAdviser original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateProjectAdviser(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteProjectAdviser(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = ProjectAdviserMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<ProjectAdviser>> QueryProjectAdviser(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<ProjectAdviser, ProjectAdviserMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<ProjectAdviser?> GetProjectAdviser(Guid id)
        {
            var sql = @"SELECT * FROM [ProjectAdviser] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<ProjectAdviser, ProjectAdviserMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<ProjectAdviser> RequireProjectAdviser(Guid id)
        {
            return await GetProjectAdviser(id) ?? throw new InvalidOperationException("The specified ProjectAdviser does not exist");
        }
        public async Task<int> CountProjectAdviser()
        {
            var sql = @"SELECT COUNT(Id) FROM [ProjectAdviser]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<ProjectAdviser>> SelectProjectAdviser()
        {
            var sql = @"SELECT * FROM [ProjectAdviser] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<ProjectAdviser, ProjectAdviserMetadata>(sql);
        }
        public async Task<List<ProjectAdviser>> SelectProjectAdviserWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [ProjectAdviser] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<ProjectAdviser, ProjectAdviserMetadata>(sql, parameters);
        }
        public async Task<ProjectAdviser?> GetProjectAdviserByProjectIdAndUserId(Guid projectId, Guid userId)
        {
            var sql = @"SELECT * FROM [ProjectAdviser] WHERE ProjectId = @ProjectId AND UserId = @UserId ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@ProjectId", projectId),
                new SqlParameter("@UserId", userId),
            };
            return (await _connection.ExecuteReader<ProjectAdviser, ProjectAdviserMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<ProjectAdviser> RequireProjectAdviserByProjectIdAndUserId(Guid projectId, Guid userId)
        {
            return await GetProjectAdviserByProjectIdAndUserId(projectId, userId) ?? throw new InvalidOperationException("The specified ProjectAdviser does not exist");
        }
        public async Task<List<ProjectAdviser>> SelectProjectAdviserByProjectId(Guid projectId)
        {
            var sql = @"SELECT * FROM [ProjectAdviser] WHERE ProjectId = @ProjectId ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@ProjectId", projectId),
            };
            return await _connection.ExecuteReader<ProjectAdviser, ProjectAdviserMetadata>(sql, parameters);
        }
    }
}

