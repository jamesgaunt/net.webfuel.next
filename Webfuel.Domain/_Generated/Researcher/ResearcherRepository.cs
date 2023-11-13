using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain
{
    internal partial interface IResearcherRepository
    {
        Task<Researcher> InsertResearcher(Researcher entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<Researcher> UpdateResearcher(Researcher entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<Researcher> UpdateResearcher(Researcher updated, Researcher original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteResearcher(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<Researcher>> QueryResearcher(Query query);
        Task<Researcher?> GetResearcher(Guid id);
        Task<Researcher> RequireResearcher(Guid id);
        Task<int> CountResearcher();
        Task<List<Researcher>> SelectResearcher();
        Task<List<Researcher>> SelectResearcherWithPage(int skip, int take);
        Task<Researcher?> GetResearcherByEmail(string email);
        Task<Researcher> RequireResearcherByEmail(string email);
    }
    [Service(typeof(IResearcherRepository))]
    internal partial class ResearcherRepository: IResearcherRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public ResearcherRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<Researcher> InsertResearcher(Researcher entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            ResearcherMetadata.Validate(entity);
            var sql = ResearcherMetadata.InsertSQL();
            var parameters = ResearcherMetadata.ExtractParameters(entity, ResearcherMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<Researcher> UpdateResearcher(Researcher entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            ResearcherMetadata.Validate(entity);
            var sql = ResearcherMetadata.UpdateSQL();
            var parameters = ResearcherMetadata.ExtractParameters(entity, ResearcherMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<Researcher> UpdateResearcher(Researcher updated, Researcher original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateResearcher(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteResearcher(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = ResearcherMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<Researcher>> QueryResearcher(Query query)
        {
            return await _connection.ExecuteQuery<Researcher, ResearcherMetadata>(query);
        }
        public async Task<Researcher?> GetResearcher(Guid id)
        {
            var sql = @"SELECT * FROM [Researcher] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<Researcher, ResearcherMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<Researcher> RequireResearcher(Guid id)
        {
            return await GetResearcher(id) ?? throw new InvalidOperationException("The specified Researcher does not exist");
        }
        public async Task<int> CountResearcher()
        {
            var sql = @"SELECT COUNT(Id) FROM [Researcher]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<Researcher>> SelectResearcher()
        {
            var sql = @"SELECT * FROM [Researcher] ORDER BY Id ASC";
            return await _connection.ExecuteReader<Researcher, ResearcherMetadata>(sql);
        }
        public async Task<List<Researcher>> SelectResearcherWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [Researcher] ORDER BY Id ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<Researcher, ResearcherMetadata>(sql, parameters);
        }
        public async Task<Researcher?> GetResearcherByEmail(string email)
        {
            var sql = @"SELECT * FROM [Researcher] WHERE Email = @Email ORDER BY Id ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Email", email),
            };
            return (await _connection.ExecuteReader<Researcher, ResearcherMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<Researcher> RequireResearcherByEmail(string email)
        {
            return await GetResearcherByEmail(email) ?? throw new InvalidOperationException("The specified Researcher does not exist");
        }
    }
}

