using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain
{
    internal partial interface IProjectRepository
    {
        Task<Project> InsertProject(Project entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<Project> UpdateProject(Project entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<Project> UpdateProject(Project updated, Project original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteProject(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<Project>> QueryProject(Query query, bool selectItems = true, bool countTotal = true);
        Task<Project?> GetProject(Guid id);
        Task<Project> RequireProject(Guid id);
        Task<int> CountProject();
        Task<List<Project>> SelectProject();
        Task<List<Project>> SelectProjectWithPage(int skip, int take);
        Task<Project?> GetProjectByProjectSupportGroupId(Guid projectSupportGroupId);
        Task<Project> RequireProjectByProjectSupportGroupId(Guid projectSupportGroupId);
        Task<List<Project>> SelectProjectBySupportRequestId(Guid? supportRequestId);
        Task<List<Project>> SelectProjectByDateOfRequest(DateOnly dateOfRequest);
        Task<List<Project>> SelectProjectByLocked(bool locked);
        Task<List<Project>> SelectProjectByStatusId(Guid statusId);
        Task<List<Project>> SelectProjectByLeadAdviserUserId(Guid? leadAdviserUserId);
        Task<List<Project>> SelectProjectByNumber(int number);
    }
    [Service(typeof(IProjectRepository))]
    internal partial class ProjectRepository: IProjectRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public ProjectRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<Project> InsertProject(Project entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            ProjectMetadata.Validate(entity);
            var sql = ProjectMetadata.InsertSQL();
            var parameters = ProjectMetadata.ExtractParameters(entity, ProjectMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<Project> UpdateProject(Project entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            ProjectMetadata.Validate(entity);
            var sql = ProjectMetadata.UpdateSQL();
            var parameters = ProjectMetadata.ExtractParameters(entity, ProjectMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<Project> UpdateProject(Project updated, Project original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateProject(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteProject(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = ProjectMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<Project>> QueryProject(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<Project, ProjectMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<Project?> GetProject(Guid id)
        {
            var sql = @"SELECT * FROM [Project] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<Project, ProjectMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<Project> RequireProject(Guid id)
        {
            return await GetProject(id) ?? throw new InvalidOperationException("The specified Project does not exist");
        }
        public async Task<int> CountProject()
        {
            var sql = @"SELECT COUNT(Id) FROM [Project]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<Project>> SelectProject()
        {
            var sql = @"SELECT * FROM [Project] ORDER BY Number DESC";
            return await _connection.ExecuteReader<Project, ProjectMetadata>(sql);
        }
        public async Task<List<Project>> SelectProjectWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [Project] ORDER BY Number DESC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<Project, ProjectMetadata>(sql, parameters);
        }
        public async Task<Project?> GetProjectByProjectSupportGroupId(Guid projectSupportGroupId)
        {
            var sql = @"SELECT * FROM [Project] WHERE ProjectSupportGroupId = @ProjectSupportGroupId ORDER BY Number DESC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@ProjectSupportGroupId", projectSupportGroupId),
            };
            return (await _connection.ExecuteReader<Project, ProjectMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<Project> RequireProjectByProjectSupportGroupId(Guid projectSupportGroupId)
        {
            return await GetProjectByProjectSupportGroupId(projectSupportGroupId) ?? throw new InvalidOperationException("The specified Project does not exist");
        }
        public async Task<List<Project>> SelectProjectBySupportRequestId(Guid? supportRequestId)
        {
            var sql = @"SELECT * FROM [Project] WHERE ((SupportRequestId = @SupportRequestId) OR (SupportRequestId IS NULL AND @SupportRequestId IS NULL)) ORDER BY Number DESC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@SupportRequestId", (object?)supportRequestId ?? DBNull.Value),
            };
            return await _connection.ExecuteReader<Project, ProjectMetadata>(sql, parameters);
        }
        public async Task<List<Project>> SelectProjectByDateOfRequest(DateOnly dateOfRequest)
        {
            var sql = @"SELECT * FROM [Project] WHERE DateOfRequest = @DateOfRequest ORDER BY Number DESC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@DateOfRequest", dateOfRequest),
            };
            return await _connection.ExecuteReader<Project, ProjectMetadata>(sql, parameters);
        }
        public async Task<List<Project>> SelectProjectByLocked(bool locked)
        {
            var sql = @"SELECT * FROM [Project] WHERE Locked = @Locked ORDER BY Number DESC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Locked", locked),
            };
            return await _connection.ExecuteReader<Project, ProjectMetadata>(sql, parameters);
        }
        public async Task<List<Project>> SelectProjectByStatusId(Guid statusId)
        {
            var sql = @"SELECT * FROM [Project] WHERE StatusId = @StatusId ORDER BY Number DESC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@StatusId", statusId),
            };
            return await _connection.ExecuteReader<Project, ProjectMetadata>(sql, parameters);
        }
        public async Task<List<Project>> SelectProjectByLeadAdviserUserId(Guid? leadAdviserUserId)
        {
            var sql = @"SELECT * FROM [Project] WHERE ((LeadAdviserUserId = @LeadAdviserUserId) OR (LeadAdviserUserId IS NULL AND @LeadAdviserUserId IS NULL)) ORDER BY Number DESC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@LeadAdviserUserId", (object?)leadAdviserUserId ?? DBNull.Value),
            };
            return await _connection.ExecuteReader<Project, ProjectMetadata>(sql, parameters);
        }
        public async Task<List<Project>> SelectProjectByNumber(int number)
        {
            var sql = @"SELECT * FROM [Project] WHERE Number = @Number ORDER BY Number DESC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Number", number),
            };
            return await _connection.ExecuteReader<Project, ProjectMetadata>(sql, parameters);
        }
    }
}

