using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IResearchMethodologyRepository
    {
        Task<ResearchMethodology> InsertResearchMethodology(ResearchMethodology entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<ResearchMethodology> UpdateResearchMethodology(ResearchMethodology entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<ResearchMethodology> UpdateResearchMethodology(ResearchMethodology updated, ResearchMethodology original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteResearchMethodology(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<ResearchMethodology>> QueryResearchMethodology(Query query);
        Task<ResearchMethodology?> GetResearchMethodology(Guid id);
        Task<ResearchMethodology> RequireResearchMethodology(Guid id);
        Task<int> CountResearchMethodology();
        Task<List<ResearchMethodology>> SelectResearchMethodology();
        Task<List<ResearchMethodology>> SelectResearchMethodologyWithPage(int skip, int take);
        Task<ResearchMethodology?> GetResearchMethodologyByCode(string code);
        Task<ResearchMethodology> RequireResearchMethodologyByCode(string code);
    }
    [Service(typeof(IResearchMethodologyRepository))]
    internal partial class ResearchMethodologyRepository: IResearchMethodologyRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public ResearchMethodologyRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<ResearchMethodology> InsertResearchMethodology(ResearchMethodology entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            var sql = ResearchMethodologyMetadata.InsertSQL();
            var parameters = ResearchMethodologyMetadata.ExtractParameters(entity, ResearchMethodologyMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<ResearchMethodology> UpdateResearchMethodology(ResearchMethodology entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = ResearchMethodologyMetadata.UpdateSQL();
            var parameters = ResearchMethodologyMetadata.ExtractParameters(entity, ResearchMethodologyMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<ResearchMethodology> UpdateResearchMethodology(ResearchMethodology updated, ResearchMethodology original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateResearchMethodology(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteResearchMethodology(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = ResearchMethodologyMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<ResearchMethodology>> QueryResearchMethodology(Query query)
        {
            return await _connection.ExecuteQuery<ResearchMethodology, ResearchMethodologyMetadata>(query);
        }
        public async Task<ResearchMethodology?> GetResearchMethodology(Guid id)
        {
            var sql = @"SELECT * FROM [ResearchMethodology] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<ResearchMethodology, ResearchMethodologyMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<ResearchMethodology> RequireResearchMethodology(Guid id)
        {
            return await GetResearchMethodology(id) ?? throw new InvalidOperationException("The specified ResearchMethodology does not exist");
        }
        public async Task<int> CountResearchMethodology()
        {
            var sql = @"SELECT COUNT(Id) FROM [ResearchMethodology]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<ResearchMethodology>> SelectResearchMethodology()
        {
            var sql = @"SELECT * FROM [ResearchMethodology] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<ResearchMethodology, ResearchMethodologyMetadata>(sql);
        }
        public async Task<List<ResearchMethodology>> SelectResearchMethodologyWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [ResearchMethodology] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<ResearchMethodology, ResearchMethodologyMetadata>(sql, parameters);
        }
        public async Task<ResearchMethodology?> GetResearchMethodologyByCode(string code)
        {
            var sql = @"SELECT * FROM [ResearchMethodology] WHERE Code = @Code ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Code", code),
            };
            return (await _connection.ExecuteReader<ResearchMethodology, ResearchMethodologyMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<ResearchMethodology> RequireResearchMethodologyByCode(string code)
        {
            return await GetResearchMethodologyByCode(code) ?? throw new InvalidOperationException("The specified ResearchMethodology does not exist");
        }
    }
}

