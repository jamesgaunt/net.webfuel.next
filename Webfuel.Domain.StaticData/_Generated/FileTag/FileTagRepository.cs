using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IFileTagRepository
    {
        Task<FileTag> InsertFileTag(FileTag entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<FileTag> UpdateFileTag(FileTag entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<FileTag> UpdateFileTag(FileTag updated, FileTag original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteFileTag(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<FileTag>> QueryFileTag(Query query, bool selectItems = true, bool countTotal = true);
        Task<FileTag?> GetFileTag(Guid id);
        Task<FileTag> RequireFileTag(Guid id);
        Task<int> CountFileTag();
        Task<List<FileTag>> SelectFileTag();
        Task<List<FileTag>> SelectFileTagWithPage(int skip, int take);
        Task<FileTag?> GetFileTagByName(string name);
        Task<FileTag> RequireFileTagByName(string name);
    }
    [Service(typeof(IFileTagRepository))]
    internal partial class FileTagRepository: IFileTagRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public FileTagRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<FileTag> InsertFileTag(FileTag entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            FileTagMetadata.Validate(entity);
            var sql = FileTagMetadata.InsertSQL();
            var parameters = FileTagMetadata.ExtractParameters(entity, FileTagMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<FileTag> UpdateFileTag(FileTag entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            FileTagMetadata.Validate(entity);
            var sql = FileTagMetadata.UpdateSQL();
            var parameters = FileTagMetadata.ExtractParameters(entity, FileTagMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<FileTag> UpdateFileTag(FileTag updated, FileTag original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateFileTag(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteFileTag(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = FileTagMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<FileTag>> QueryFileTag(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<FileTag, FileTagMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<FileTag?> GetFileTag(Guid id)
        {
            var sql = @"SELECT * FROM [FileTag] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<FileTag, FileTagMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<FileTag> RequireFileTag(Guid id)
        {
            return await GetFileTag(id) ?? throw new InvalidOperationException("The specified FileTag does not exist");
        }
        public async Task<int> CountFileTag()
        {
            var sql = @"SELECT COUNT(Id) FROM [FileTag]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<FileTag>> SelectFileTag()
        {
            var sql = @"SELECT * FROM [FileTag] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<FileTag, FileTagMetadata>(sql);
        }
        public async Task<List<FileTag>> SelectFileTagWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [FileTag] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<FileTag, FileTagMetadata>(sql, parameters);
        }
        public async Task<FileTag?> GetFileTagByName(string name)
        {
            var sql = @"SELECT * FROM [FileTag] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<FileTag, FileTagMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<FileTag> RequireFileTagByName(string name)
        {
            return await GetFileTagByName(name) ?? throw new InvalidOperationException("The specified FileTag does not exist");
        }
    }
}

