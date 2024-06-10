using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain
{
    internal partial interface IProjectSupportRepository
    {
        Task<ProjectSupport> InsertProjectSupport(ProjectSupport entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<ProjectSupport> UpdateProjectSupport(ProjectSupport entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<ProjectSupport> UpdateProjectSupport(ProjectSupport updated, ProjectSupport original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteProjectSupport(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<ProjectSupport>> QueryProjectSupport(Query query, bool selectItems = true, bool countTotal = true);
        Task<ProjectSupport?> GetProjectSupport(Guid id);
        Task<ProjectSupport> RequireProjectSupport(Guid id);
        Task<int> CountProjectSupport();
        Task<List<ProjectSupport>> SelectProjectSupport();
        Task<List<ProjectSupport>> SelectProjectSupportWithPage(int skip, int take);
        Task<List<ProjectSupport>> SelectProjectSupportBySupportRequestedCompletedAt(DateTimeOffset? supportRequestedCompletedAt);
        Task<List<ProjectSupport>> SelectProjectSupportBySupportRequestedTeamId(Guid? supportRequestedTeamId);
        Task<List<ProjectSupport>> SelectProjectSupportByDate(DateOnly date);
        Task<List<ProjectSupport>> SelectProjectSupportByProjectId(Guid projectId);
        Task<int?> SumMinutesByProjectId(Guid projectId);
        Task<List<ProjectSupport>> SelectOpenSupportRequestTeamIdsByProjectId(Guid projectId);
    }
    [Service(typeof(IProjectSupportRepository))]
    internal partial class ProjectSupportRepository: IProjectSupportRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public ProjectSupportRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<ProjectSupport> InsertProjectSupport(ProjectSupport entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            ProjectSupportMetadata.Validate(entity);
            var sql = ProjectSupportMetadata.InsertSQL();
            var parameters = ProjectSupportMetadata.ExtractParameters(entity, ProjectSupportMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<ProjectSupport> UpdateProjectSupport(ProjectSupport entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            ProjectSupportMetadata.Validate(entity);
            var sql = ProjectSupportMetadata.UpdateSQL();
            var parameters = ProjectSupportMetadata.ExtractParameters(entity, ProjectSupportMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<ProjectSupport> UpdateProjectSupport(ProjectSupport updated, ProjectSupport original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateProjectSupport(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteProjectSupport(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = ProjectSupportMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<ProjectSupport>> QueryProjectSupport(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<ProjectSupport, ProjectSupportMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<ProjectSupport?> GetProjectSupport(Guid id)
        {
            var sql = @"SELECT * FROM [ProjectSupport] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<ProjectSupport, ProjectSupportMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<ProjectSupport> RequireProjectSupport(Guid id)
        {
            return await GetProjectSupport(id) ?? throw new InvalidOperationException("The specified ProjectSupport does not exist");
        }
        public async Task<int> CountProjectSupport()
        {
            var sql = @"SELECT COUNT(Id) FROM [ProjectSupport]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<ProjectSupport>> SelectProjectSupport()
        {
            var sql = @"SELECT * FROM [ProjectSupport] ORDER BY Date DESC, Id DESC";
            return await _connection.ExecuteReader<ProjectSupport, ProjectSupportMetadata>(sql);
        }
        public async Task<List<ProjectSupport>> SelectProjectSupportWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [ProjectSupport] ORDER BY Date DESC, Id DESC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<ProjectSupport, ProjectSupportMetadata>(sql, parameters);
        }
        public async Task<List<ProjectSupport>> SelectProjectSupportBySupportRequestedCompletedAt(DateTimeOffset? supportRequestedCompletedAt)
        {
            var sql = @"SELECT * FROM [ProjectSupport] WHERE ((SupportRequestedCompletedAt = @SupportRequestedCompletedAt) OR (SupportRequestedCompletedAt IS NULL AND @SupportRequestedCompletedAt IS NULL)) ORDER BY Date DESC, Id DESC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@SupportRequestedCompletedAt", (object?)supportRequestedCompletedAt ?? DBNull.Value),
            };
            return await _connection.ExecuteReader<ProjectSupport, ProjectSupportMetadata>(sql, parameters);
        }
        public async Task<List<ProjectSupport>> SelectProjectSupportBySupportRequestedTeamId(Guid? supportRequestedTeamId)
        {
            var sql = @"SELECT * FROM [ProjectSupport] WHERE ((SupportRequestedTeamId = @SupportRequestedTeamId) OR (SupportRequestedTeamId IS NULL AND @SupportRequestedTeamId IS NULL)) ORDER BY Date DESC, Id DESC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@SupportRequestedTeamId", (object?)supportRequestedTeamId ?? DBNull.Value),
            };
            return await _connection.ExecuteReader<ProjectSupport, ProjectSupportMetadata>(sql, parameters);
        }
        public async Task<List<ProjectSupport>> SelectProjectSupportByDate(DateOnly date)
        {
            var sql = @"SELECT * FROM [ProjectSupport] WHERE Date = @Date ORDER BY Date DESC, Id DESC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Date", date),
            };
            return await _connection.ExecuteReader<ProjectSupport, ProjectSupportMetadata>(sql, parameters);
        }
        public async Task<List<ProjectSupport>> SelectProjectSupportByProjectId(Guid projectId)
        {
            var sql = @"SELECT * FROM [ProjectSupport] WHERE ProjectId = @ProjectId ORDER BY Date DESC, Id DESC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@ProjectId", projectId),
            };
            return await _connection.ExecuteReader<ProjectSupport, ProjectSupportMetadata>(sql, parameters);
        }
        public async Task<int?> SumMinutesByProjectId(Guid projectId)
        {
            var sql = @"SELECT SUM(CalculatedMinutes) FROM ProjectSupport WHERE ProjectId = @ProjectId";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("ProjectId", projectId),
            };
            var result = await _connection.ExecuteScalar(sql, parameters);
            return result == DBNull.Value ? null : (int)result;
        }
        public async Task<List<ProjectSupport>> SelectOpenSupportRequestTeamIdsByProjectId(Guid projectId)
        {
            var sql = @"SELECT DISTINCT SupportRequestedTeamId FROM ProjectSupport WHERE ProjectId = @ProjectId AND SupportRequestedCompletedAt IS NULL AND SupportRequestedTeamId IS NOT NULL";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("ProjectId", projectId),
            };
            return await _connection.ExecuteReader<ProjectSupport, ProjectSupportMetadata>(sql, parameters);
        }
    }
}

