using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain
{
    internal partial interface IProjectSupportGroupRepository
    {
        Task<ProjectSupportGroup> InsertProjectSupportGroup(ProjectSupportGroup entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<ProjectSupportGroup> UpdateProjectSupportGroup(ProjectSupportGroup entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<ProjectSupportGroup> UpdateProjectSupportGroup(ProjectSupportGroup updated, ProjectSupportGroup original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteProjectSupportGroup(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<ProjectSupportGroup>> QueryProjectSupportGroup(Query query, bool selectItems = true, bool countTotal = true);
        Task<ProjectSupportGroup?> GetProjectSupportGroup(Guid id);
        Task<ProjectSupportGroup> RequireProjectSupportGroup(Guid id);
        Task<int> CountProjectSupportGroup();
        Task<List<ProjectSupportGroup>> SelectProjectSupportGroup();
        Task<List<ProjectSupportGroup>> SelectProjectSupportGroupWithPage(int skip, int take);
    }
    [Service(typeof(IProjectSupportGroupRepository))]
    internal partial class ProjectSupportGroupRepository: IProjectSupportGroupRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public ProjectSupportGroupRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<ProjectSupportGroup> InsertProjectSupportGroup(ProjectSupportGroup entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            ProjectSupportGroupMetadata.Validate(entity);
            var sql = ProjectSupportGroupMetadata.InsertSQL();
            var parameters = ProjectSupportGroupMetadata.ExtractParameters(entity, ProjectSupportGroupMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<ProjectSupportGroup> UpdateProjectSupportGroup(ProjectSupportGroup entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            ProjectSupportGroupMetadata.Validate(entity);
            var sql = ProjectSupportGroupMetadata.UpdateSQL();
            var parameters = ProjectSupportGroupMetadata.ExtractParameters(entity, ProjectSupportGroupMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<ProjectSupportGroup> UpdateProjectSupportGroup(ProjectSupportGroup updated, ProjectSupportGroup original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateProjectSupportGroup(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteProjectSupportGroup(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = ProjectSupportGroupMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<ProjectSupportGroup>> QueryProjectSupportGroup(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<ProjectSupportGroup, ProjectSupportGroupMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<ProjectSupportGroup?> GetProjectSupportGroup(Guid id)
        {
            var sql = @"SELECT * FROM [ProjectSupportGroup] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<ProjectSupportGroup, ProjectSupportGroupMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<ProjectSupportGroup> RequireProjectSupportGroup(Guid id)
        {
            return await GetProjectSupportGroup(id) ?? throw new InvalidOperationException("The specified ProjectSupportGroup does not exist");
        }
        public async Task<int> CountProjectSupportGroup()
        {
            var sql = @"SELECT COUNT(Id) FROM [ProjectSupportGroup]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<ProjectSupportGroup>> SelectProjectSupportGroup()
        {
            var sql = @"SELECT * FROM [ProjectSupportGroup] ORDER BY Id ASC";
            return await _connection.ExecuteReader<ProjectSupportGroup, ProjectSupportGroupMetadata>(sql);
        }
        public async Task<List<ProjectSupportGroup>> SelectProjectSupportGroupWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [ProjectSupportGroup] ORDER BY Id ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<ProjectSupportGroup, ProjectSupportGroupMetadata>(sql, parameters);
        }
    }
}

