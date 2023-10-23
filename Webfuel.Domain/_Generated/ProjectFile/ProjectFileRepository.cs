using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain
{
    internal partial interface IProjectFileRepository
    {
        Task<ProjectFile> InsertProjectFile(ProjectFile entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<ProjectFile> UpdateProjectFile(ProjectFile entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<ProjectFile> UpdateProjectFile(ProjectFile updated, ProjectFile original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteProjectFile(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<ProjectFile>> QueryProjectFile(Query query);
        Task<ProjectFile?> GetProjectFile(Guid id);
        Task<ProjectFile> RequireProjectFile(Guid id);
        Task<int> CountProjectFile();
        Task<List<ProjectFile>> SelectProjectFile();
        Task<List<ProjectFile>> SelectProjectFileWithPage(int skip, int take);
        Task<List<ProjectFile>> SelectProjectFileByFileGroupId(Guid fileGroupId);
    }
    [Service(typeof(IProjectFileRepository))]
    internal partial class ProjectFileRepository: IProjectFileRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public ProjectFileRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<ProjectFile> InsertProjectFile(ProjectFile entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            var sql = ProjectFileMetadata.InsertSQL();
            var parameters = ProjectFileMetadata.ExtractParameters(entity, ProjectFileMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<ProjectFile> UpdateProjectFile(ProjectFile entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = ProjectFileMetadata.UpdateSQL();
            var parameters = ProjectFileMetadata.ExtractParameters(entity, ProjectFileMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<ProjectFile> UpdateProjectFile(ProjectFile updated, ProjectFile original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateProjectFile(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteProjectFile(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = ProjectFileMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<ProjectFile>> QueryProjectFile(Query query)
        {
            return await _connection.ExecuteQuery<ProjectFile, ProjectFileMetadata>(query);
        }
        public async Task<ProjectFile?> GetProjectFile(Guid id)
        {
            var sql = @"SELECT * FROM [ProjectFile] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<ProjectFile, ProjectFileMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<ProjectFile> RequireProjectFile(Guid id)
        {
            return await GetProjectFile(id) ?? throw new InvalidOperationException("The specified ProjectFile does not exist");
        }
        public async Task<int> CountProjectFile()
        {
            var sql = @"SELECT COUNT(Id) FROM [ProjectFile]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<ProjectFile>> SelectProjectFile()
        {
            var sql = @"SELECT * FROM [ProjectFile] ORDER BY Id ASC";
            return await _connection.ExecuteReader<ProjectFile, ProjectFileMetadata>(sql);
        }
        public async Task<List<ProjectFile>> SelectProjectFileWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [ProjectFile] ORDER BY Id ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<ProjectFile, ProjectFileMetadata>(sql, parameters);
        }
        public async Task<List<ProjectFile>> SelectProjectFileByFileGroupId(Guid fileGroupId)
        {
            var sql = @"SELECT * FROM [ProjectFile] WHERE FileGroupId = @FileGroupId ORDER BY Id ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@FileGroupId", fileGroupId),
            };
            return await _connection.ExecuteReader<ProjectFile, ProjectFileMetadata>(sql, parameters);
        }
    }
}

