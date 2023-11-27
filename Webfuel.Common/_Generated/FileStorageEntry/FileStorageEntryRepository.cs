using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Common
{
    internal partial interface IFileStorageEntryRepository
    {
        Task<FileStorageEntry> InsertFileStorageEntry(FileStorageEntry entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<FileStorageEntry> UpdateFileStorageEntry(FileStorageEntry entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<FileStorageEntry> UpdateFileStorageEntry(FileStorageEntry updated, FileStorageEntry original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteFileStorageEntry(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<FileStorageEntry>> QueryFileStorageEntry(Query query, bool countTotal = true);
        Task<FileStorageEntry?> GetFileStorageEntry(Guid id);
        Task<FileStorageEntry> RequireFileStorageEntry(Guid id);
        Task<int> CountFileStorageEntry();
        Task<List<FileStorageEntry>> SelectFileStorageEntry();
        Task<List<FileStorageEntry>> SelectFileStorageEntryWithPage(int skip, int take);
        Task<List<FileStorageEntry>> SelectFileStorageEntryByFileStorageGroupId(Guid fileStorageGroupId);
    }
    [Service(typeof(IFileStorageEntryRepository))]
    internal partial class FileStorageEntryRepository: IFileStorageEntryRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public FileStorageEntryRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<FileStorageEntry> InsertFileStorageEntry(FileStorageEntry entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            FileStorageEntryMetadata.Validate(entity);
            var sql = FileStorageEntryMetadata.InsertSQL();
            var parameters = FileStorageEntryMetadata.ExtractParameters(entity, FileStorageEntryMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<FileStorageEntry> UpdateFileStorageEntry(FileStorageEntry entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            FileStorageEntryMetadata.Validate(entity);
            var sql = FileStorageEntryMetadata.UpdateSQL();
            var parameters = FileStorageEntryMetadata.ExtractParameters(entity, FileStorageEntryMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<FileStorageEntry> UpdateFileStorageEntry(FileStorageEntry updated, FileStorageEntry original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateFileStorageEntry(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteFileStorageEntry(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = FileStorageEntryMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<FileStorageEntry>> QueryFileStorageEntry(Query query, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<FileStorageEntry, FileStorageEntryMetadata>(query, countTotal);
        }
        public async Task<FileStorageEntry?> GetFileStorageEntry(Guid id)
        {
            var sql = @"SELECT * FROM [FileStorageEntry] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<FileStorageEntry, FileStorageEntryMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<FileStorageEntry> RequireFileStorageEntry(Guid id)
        {
            return await GetFileStorageEntry(id) ?? throw new InvalidOperationException("The specified FileStorageEntry does not exist");
        }
        public async Task<int> CountFileStorageEntry()
        {
            var sql = @"SELECT COUNT(Id) FROM [FileStorageEntry]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<FileStorageEntry>> SelectFileStorageEntry()
        {
            var sql = @"SELECT * FROM [FileStorageEntry] ORDER BY Id ASC";
            return await _connection.ExecuteReader<FileStorageEntry, FileStorageEntryMetadata>(sql);
        }
        public async Task<List<FileStorageEntry>> SelectFileStorageEntryWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [FileStorageEntry] ORDER BY Id ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<FileStorageEntry, FileStorageEntryMetadata>(sql, parameters);
        }
        public async Task<List<FileStorageEntry>> SelectFileStorageEntryByFileStorageGroupId(Guid fileStorageGroupId)
        {
            var sql = @"SELECT * FROM [FileStorageEntry] WHERE FileStorageGroupId = @FileStorageGroupId ORDER BY Id ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@FileStorageGroupId", fileStorageGroupId),
            };
            return await _connection.ExecuteReader<FileStorageEntry, FileStorageEntryMetadata>(sql, parameters);
        }
    }
}

