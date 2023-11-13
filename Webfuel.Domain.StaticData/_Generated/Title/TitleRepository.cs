using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface ITitleRepository
    {
        Task<Title> InsertTitle(Title entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<Title> UpdateTitle(Title entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<Title> UpdateTitle(Title updated, Title original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteTitle(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<Title>> QueryTitle(Query query);
        Task<Title?> GetTitle(Guid id);
        Task<Title> RequireTitle(Guid id);
        Task<int> CountTitle();
        Task<List<Title>> SelectTitle();
        Task<List<Title>> SelectTitleWithPage(int skip, int take);
        Task<Title?> GetTitleByName(string name);
        Task<Title> RequireTitleByName(string name);
    }
    [Service(typeof(ITitleRepository))]
    internal partial class TitleRepository: ITitleRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public TitleRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<Title> InsertTitle(Title entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            TitleMetadata.Validate(entity);
            var sql = TitleMetadata.InsertSQL();
            var parameters = TitleMetadata.ExtractParameters(entity, TitleMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<Title> UpdateTitle(Title entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            TitleMetadata.Validate(entity);
            var sql = TitleMetadata.UpdateSQL();
            var parameters = TitleMetadata.ExtractParameters(entity, TitleMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<Title> UpdateTitle(Title updated, Title original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateTitle(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteTitle(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = TitleMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<Title>> QueryTitle(Query query)
        {
            return await _connection.ExecuteQuery<Title, TitleMetadata>(query);
        }
        public async Task<Title?> GetTitle(Guid id)
        {
            var sql = @"SELECT * FROM [Title] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<Title, TitleMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<Title> RequireTitle(Guid id)
        {
            return await GetTitle(id) ?? throw new InvalidOperationException("The specified Title does not exist");
        }
        public async Task<int> CountTitle()
        {
            var sql = @"SELECT COUNT(Id) FROM [Title]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<Title>> SelectTitle()
        {
            var sql = @"SELECT * FROM [Title] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<Title, TitleMetadata>(sql);
        }
        public async Task<List<Title>> SelectTitleWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [Title] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<Title, TitleMetadata>(sql, parameters);
        }
        public async Task<Title?> GetTitleByName(string name)
        {
            var sql = @"SELECT * FROM [Title] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<Title, TitleMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<Title> RequireTitleByName(string name)
        {
            return await GetTitleByName(name) ?? throw new InvalidOperationException("The specified Title does not exist");
        }
    }
}

