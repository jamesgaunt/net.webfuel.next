using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Common
{
    internal partial interface IFileStorageGroupRepository
    {
        Task<FileStorageGroup> InsertFileStorageGroup(FileStorageGroup entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<FileStorageGroup> UpdateFileStorageGroup(FileStorageGroup entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<FileStorageGroup> UpdateFileStorageGroup(FileStorageGroup updated, FileStorageGroup original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteFileStorageGroup(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<FileStorageGroup>> QueryFileStorageGroup(Query query);
        Task<FileStorageGroup?> GetFileStorageGroup(Guid id);
        Task<FileStorageGroup> RequireFileStorageGroup(Guid id);
        Task<int> CountFileStorageGroup();
        Task<List<FileStorageGroup>> SelectFileStorageGroup();
        Task<List<FileStorageGroup>> SelectFileStorageGroupWithPage(int skip, int take);
    }
    [Service(typeof(IFileStorageGroupRepository))]
    internal partial class FileStorageGroupRepository: IFileStorageGroupRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public FileStorageGroupRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<FileStorageGroup> InsertFileStorageGroup(FileStorageGroup entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            FileStorageGroupMetadata.Validate(entity);
            var sql = FileStorageGroupMetadata.InsertSQL();
            var parameters = FileStorageGroupMetadata.ExtractParameters(entity, FileStorageGroupMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<FileStorageGroup> UpdateFileStorageGroup(FileStorageGroup entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            FileStorageGroupMetadata.Validate(entity);
            var sql = FileStorageGroupMetadata.UpdateSQL();
            var parameters = FileStorageGroupMetadata.ExtractParameters(entity, FileStorageGroupMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<FileStorageGroup> UpdateFileStorageGroup(FileStorageGroup updated, FileStorageGroup original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateFileStorageGroup(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteFileStorageGroup(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = FileStorageGroupMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<FileStorageGroup>> QueryFileStorageGroup(Query query)
        {
            return await _connection.ExecuteQuery<FileStorageGroup, FileStorageGroupMetadata>(query);
        }
        public async Task<FileStorageGroup?> GetFileStorageGroup(Guid id)
        {
            var sql = @"SELECT * FROM [FileStorageGroup] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<FileStorageGroup, FileStorageGroupMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<FileStorageGroup> RequireFileStorageGroup(Guid id)
        {
            return await GetFileStorageGroup(id) ?? throw new InvalidOperationException("The specified FileStorageGroup does not exist");
        }
        public async Task<int> CountFileStorageGroup()
        {
            var sql = @"SELECT COUNT(Id) FROM [FileStorageGroup]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<FileStorageGroup>> SelectFileStorageGroup()
        {
            var sql = @"SELECT * FROM [FileStorageGroup] ORDER BY Id ASC";
            return await _connection.ExecuteReader<FileStorageGroup, FileStorageGroupMetadata>(sql);
        }
        public async Task<List<FileStorageGroup>> SelectFileStorageGroupWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [FileStorageGroup] ORDER BY Id ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<FileStorageGroup, FileStorageGroupMetadata>(sql, parameters);
        }
    }
}

