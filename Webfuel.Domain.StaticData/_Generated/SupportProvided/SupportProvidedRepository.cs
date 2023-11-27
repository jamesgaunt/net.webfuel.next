using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface ISupportProvidedRepository
    {
        Task<SupportProvided> InsertSupportProvided(SupportProvided entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<SupportProvided> UpdateSupportProvided(SupportProvided entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<SupportProvided> UpdateSupportProvided(SupportProvided updated, SupportProvided original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteSupportProvided(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<SupportProvided>> QuerySupportProvided(Query query, bool countTotal = true);
        Task<SupportProvided?> GetSupportProvided(Guid id);
        Task<SupportProvided> RequireSupportProvided(Guid id);
        Task<int> CountSupportProvided();
        Task<List<SupportProvided>> SelectSupportProvided();
        Task<List<SupportProvided>> SelectSupportProvidedWithPage(int skip, int take);
        Task<SupportProvided?> GetSupportProvidedByName(string name);
        Task<SupportProvided> RequireSupportProvidedByName(string name);
    }
    [Service(typeof(ISupportProvidedRepository))]
    internal partial class SupportProvidedRepository: ISupportProvidedRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public SupportProvidedRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<SupportProvided> InsertSupportProvided(SupportProvided entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            SupportProvidedMetadata.Validate(entity);
            var sql = SupportProvidedMetadata.InsertSQL();
            var parameters = SupportProvidedMetadata.ExtractParameters(entity, SupportProvidedMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<SupportProvided> UpdateSupportProvided(SupportProvided entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            SupportProvidedMetadata.Validate(entity);
            var sql = SupportProvidedMetadata.UpdateSQL();
            var parameters = SupportProvidedMetadata.ExtractParameters(entity, SupportProvidedMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<SupportProvided> UpdateSupportProvided(SupportProvided updated, SupportProvided original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateSupportProvided(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteSupportProvided(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = SupportProvidedMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<SupportProvided>> QuerySupportProvided(Query query, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<SupportProvided, SupportProvidedMetadata>(query, countTotal);
        }
        public async Task<SupportProvided?> GetSupportProvided(Guid id)
        {
            var sql = @"SELECT * FROM [SupportProvided] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<SupportProvided, SupportProvidedMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<SupportProvided> RequireSupportProvided(Guid id)
        {
            return await GetSupportProvided(id) ?? throw new InvalidOperationException("The specified SupportProvided does not exist");
        }
        public async Task<int> CountSupportProvided()
        {
            var sql = @"SELECT COUNT(Id) FROM [SupportProvided]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<SupportProvided>> SelectSupportProvided()
        {
            var sql = @"SELECT * FROM [SupportProvided] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<SupportProvided, SupportProvidedMetadata>(sql);
        }
        public async Task<List<SupportProvided>> SelectSupportProvidedWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [SupportProvided] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<SupportProvided, SupportProvidedMetadata>(sql, parameters);
        }
        public async Task<SupportProvided?> GetSupportProvidedByName(string name)
        {
            var sql = @"SELECT * FROM [SupportProvided] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<SupportProvided, SupportProvidedMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<SupportProvided> RequireSupportProvidedByName(string name)
        {
            return await GetSupportProvidedByName(name) ?? throw new InvalidOperationException("The specified SupportProvided does not exist");
        }
    }
}

