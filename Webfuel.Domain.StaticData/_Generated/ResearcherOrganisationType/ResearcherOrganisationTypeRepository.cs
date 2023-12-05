using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IResearcherOrganisationTypeRepository
    {
        Task<ResearcherOrganisationType> InsertResearcherOrganisationType(ResearcherOrganisationType entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<ResearcherOrganisationType> UpdateResearcherOrganisationType(ResearcherOrganisationType entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<ResearcherOrganisationType> UpdateResearcherOrganisationType(ResearcherOrganisationType updated, ResearcherOrganisationType original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteResearcherOrganisationType(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<ResearcherOrganisationType>> QueryResearcherOrganisationType(Query query, bool selectItems = true, bool countTotal = true);
        Task<ResearcherOrganisationType?> GetResearcherOrganisationType(Guid id);
        Task<ResearcherOrganisationType> RequireResearcherOrganisationType(Guid id);
        Task<int> CountResearcherOrganisationType();
        Task<List<ResearcherOrganisationType>> SelectResearcherOrganisationType();
        Task<List<ResearcherOrganisationType>> SelectResearcherOrganisationTypeWithPage(int skip, int take);
        Task<ResearcherOrganisationType?> GetResearcherOrganisationTypeByName(string name);
        Task<ResearcherOrganisationType> RequireResearcherOrganisationTypeByName(string name);
    }
    [Service(typeof(IResearcherOrganisationTypeRepository))]
    internal partial class ResearcherOrganisationTypeRepository: IResearcherOrganisationTypeRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public ResearcherOrganisationTypeRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<ResearcherOrganisationType> InsertResearcherOrganisationType(ResearcherOrganisationType entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            ResearcherOrganisationTypeMetadata.Validate(entity);
            var sql = ResearcherOrganisationTypeMetadata.InsertSQL();
            var parameters = ResearcherOrganisationTypeMetadata.ExtractParameters(entity, ResearcherOrganisationTypeMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<ResearcherOrganisationType> UpdateResearcherOrganisationType(ResearcherOrganisationType entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            ResearcherOrganisationTypeMetadata.Validate(entity);
            var sql = ResearcherOrganisationTypeMetadata.UpdateSQL();
            var parameters = ResearcherOrganisationTypeMetadata.ExtractParameters(entity, ResearcherOrganisationTypeMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<ResearcherOrganisationType> UpdateResearcherOrganisationType(ResearcherOrganisationType updated, ResearcherOrganisationType original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateResearcherOrganisationType(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteResearcherOrganisationType(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = ResearcherOrganisationTypeMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<ResearcherOrganisationType>> QueryResearcherOrganisationType(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<ResearcherOrganisationType, ResearcherOrganisationTypeMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<ResearcherOrganisationType?> GetResearcherOrganisationType(Guid id)
        {
            var sql = @"SELECT * FROM [ResearcherOrganisationType] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<ResearcherOrganisationType, ResearcherOrganisationTypeMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<ResearcherOrganisationType> RequireResearcherOrganisationType(Guid id)
        {
            return await GetResearcherOrganisationType(id) ?? throw new InvalidOperationException("The specified ResearcherOrganisationType does not exist");
        }
        public async Task<int> CountResearcherOrganisationType()
        {
            var sql = @"SELECT COUNT(Id) FROM [ResearcherOrganisationType]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<ResearcherOrganisationType>> SelectResearcherOrganisationType()
        {
            var sql = @"SELECT * FROM [ResearcherOrganisationType] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<ResearcherOrganisationType, ResearcherOrganisationTypeMetadata>(sql);
        }
        public async Task<List<ResearcherOrganisationType>> SelectResearcherOrganisationTypeWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [ResearcherOrganisationType] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<ResearcherOrganisationType, ResearcherOrganisationTypeMetadata>(sql, parameters);
        }
        public async Task<ResearcherOrganisationType?> GetResearcherOrganisationTypeByName(string name)
        {
            var sql = @"SELECT * FROM [ResearcherOrganisationType] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<ResearcherOrganisationType, ResearcherOrganisationTypeMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<ResearcherOrganisationType> RequireResearcherOrganisationTypeByName(string name)
        {
            return await GetResearcherOrganisationTypeByName(name) ?? throw new InvalidOperationException("The specified ResearcherOrganisationType does not exist");
        }
    }
}

