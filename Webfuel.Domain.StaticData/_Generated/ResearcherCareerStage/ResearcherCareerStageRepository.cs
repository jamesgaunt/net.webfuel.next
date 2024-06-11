using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IResearcherCareerStageRepository
    {
        Task<ResearcherCareerStage> InsertResearcherCareerStage(ResearcherCareerStage entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<ResearcherCareerStage> UpdateResearcherCareerStage(ResearcherCareerStage entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<ResearcherCareerStage> UpdateResearcherCareerStage(ResearcherCareerStage updated, ResearcherCareerStage original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteResearcherCareerStage(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<ResearcherCareerStage>> QueryResearcherCareerStage(Query query, bool selectItems = true, bool countTotal = true);
        Task<ResearcherCareerStage?> GetResearcherCareerStage(Guid id);
        Task<ResearcherCareerStage> RequireResearcherCareerStage(Guid id);
        Task<int> CountResearcherCareerStage();
        Task<List<ResearcherCareerStage>> SelectResearcherCareerStage();
        Task<List<ResearcherCareerStage>> SelectResearcherCareerStageWithPage(int skip, int take);
        Task<ResearcherCareerStage?> GetResearcherCareerStageByName(string name);
        Task<ResearcherCareerStage> RequireResearcherCareerStageByName(string name);
    }
    [Service(typeof(IResearcherCareerStageRepository))]
    internal partial class ResearcherCareerStageRepository: IResearcherCareerStageRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public ResearcherCareerStageRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<ResearcherCareerStage> InsertResearcherCareerStage(ResearcherCareerStage entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            ResearcherCareerStageMetadata.Validate(entity);
            var sql = ResearcherCareerStageMetadata.InsertSQL();
            var parameters = ResearcherCareerStageMetadata.ExtractParameters(entity, ResearcherCareerStageMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<ResearcherCareerStage> UpdateResearcherCareerStage(ResearcherCareerStage entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            ResearcherCareerStageMetadata.Validate(entity);
            var sql = ResearcherCareerStageMetadata.UpdateSQL();
            var parameters = ResearcherCareerStageMetadata.ExtractParameters(entity, ResearcherCareerStageMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<ResearcherCareerStage> UpdateResearcherCareerStage(ResearcherCareerStage updated, ResearcherCareerStage original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateResearcherCareerStage(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteResearcherCareerStage(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = ResearcherCareerStageMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<ResearcherCareerStage>> QueryResearcherCareerStage(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<ResearcherCareerStage, ResearcherCareerStageMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<ResearcherCareerStage?> GetResearcherCareerStage(Guid id)
        {
            var sql = @"SELECT * FROM [ResearcherCareerStage] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<ResearcherCareerStage, ResearcherCareerStageMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<ResearcherCareerStage> RequireResearcherCareerStage(Guid id)
        {
            return await GetResearcherCareerStage(id) ?? throw new InvalidOperationException("The specified ResearcherCareerStage does not exist");
        }
        public async Task<int> CountResearcherCareerStage()
        {
            var sql = @"SELECT COUNT(Id) FROM [ResearcherCareerStage]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<ResearcherCareerStage>> SelectResearcherCareerStage()
        {
            var sql = @"SELECT * FROM [ResearcherCareerStage] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<ResearcherCareerStage, ResearcherCareerStageMetadata>(sql);
        }
        public async Task<List<ResearcherCareerStage>> SelectResearcherCareerStageWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [ResearcherCareerStage] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<ResearcherCareerStage, ResearcherCareerStageMetadata>(sql, parameters);
        }
        public async Task<ResearcherCareerStage?> GetResearcherCareerStageByName(string name)
        {
            var sql = @"SELECT * FROM [ResearcherCareerStage] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<ResearcherCareerStage, ResearcherCareerStageMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<ResearcherCareerStage> RequireResearcherCareerStageByName(string name)
        {
            return await GetResearcherCareerStageByName(name) ?? throw new InvalidOperationException("The specified ResearcherCareerStage does not exist");
        }
    }
}

