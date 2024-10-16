using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain
{
    internal partial interface ITriageTemplateRepository
    {
        Task<TriageTemplate> InsertTriageTemplate(TriageTemplate entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<TriageTemplate> UpdateTriageTemplate(TriageTemplate entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<TriageTemplate> UpdateTriageTemplate(TriageTemplate updated, TriageTemplate original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteTriageTemplate(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<TriageTemplate>> QueryTriageTemplate(Query query, bool selectItems = true, bool countTotal = true);
        Task<TriageTemplate?> GetTriageTemplate(Guid id);
        Task<TriageTemplate> RequireTriageTemplate(Guid id);
        Task<int> CountTriageTemplate();
        Task<List<TriageTemplate>> SelectTriageTemplate();
        Task<List<TriageTemplate>> SelectTriageTemplateWithPage(int skip, int take);
        Task<TriageTemplate?> GetTriageTemplateByName(string name);
        Task<TriageTemplate> RequireTriageTemplateByName(string name);
    }
    [Service(typeof(ITriageTemplateRepository))]
    internal partial class TriageTemplateRepository: ITriageTemplateRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public TriageTemplateRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<TriageTemplate> InsertTriageTemplate(TriageTemplate entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            TriageTemplateMetadata.Validate(entity);
            var sql = TriageTemplateMetadata.InsertSQL();
            var parameters = TriageTemplateMetadata.ExtractParameters(entity, TriageTemplateMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<TriageTemplate> UpdateTriageTemplate(TriageTemplate entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            TriageTemplateMetadata.Validate(entity);
            var sql = TriageTemplateMetadata.UpdateSQL();
            var parameters = TriageTemplateMetadata.ExtractParameters(entity, TriageTemplateMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<TriageTemplate> UpdateTriageTemplate(TriageTemplate updated, TriageTemplate original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateTriageTemplate(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteTriageTemplate(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = TriageTemplateMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<TriageTemplate>> QueryTriageTemplate(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<TriageTemplate, TriageTemplateMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<TriageTemplate?> GetTriageTemplate(Guid id)
        {
            var sql = @"SELECT * FROM [TriageTemplate] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<TriageTemplate, TriageTemplateMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<TriageTemplate> RequireTriageTemplate(Guid id)
        {
            return await GetTriageTemplate(id) ?? throw new InvalidOperationException("The specified TriageTemplate does not exist");
        }
        public async Task<int> CountTriageTemplate()
        {
            var sql = @"SELECT COUNT(Id) FROM [TriageTemplate]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<TriageTemplate>> SelectTriageTemplate()
        {
            var sql = @"SELECT * FROM [TriageTemplate] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<TriageTemplate, TriageTemplateMetadata>(sql);
        }
        public async Task<List<TriageTemplate>> SelectTriageTemplateWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [TriageTemplate] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<TriageTemplate, TriageTemplateMetadata>(sql, parameters);
        }
        public async Task<TriageTemplate?> GetTriageTemplateByName(string name)
        {
            var sql = @"SELECT * FROM [TriageTemplate] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<TriageTemplate, TriageTemplateMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<TriageTemplate> RequireTriageTemplateByName(string name)
        {
            return await GetTriageTemplateByName(name) ?? throw new InvalidOperationException("The specified TriageTemplate does not exist");
        }
    }
}

