using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IResearcherLocationRepository
    {
        Task<ResearcherLocation> InsertResearcherLocation(ResearcherLocation entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<ResearcherLocation> UpdateResearcherLocation(ResearcherLocation entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<ResearcherLocation> UpdateResearcherLocation(ResearcherLocation updated, ResearcherLocation original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteResearcherLocation(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<ResearcherLocation>> QueryResearcherLocation(Query query, bool selectItems = true, bool countTotal = true);
        Task<ResearcherLocation?> GetResearcherLocation(Guid id);
        Task<ResearcherLocation> RequireResearcherLocation(Guid id);
        Task<int> CountResearcherLocation();
        Task<List<ResearcherLocation>> SelectResearcherLocation();
        Task<List<ResearcherLocation>> SelectResearcherLocationWithPage(int skip, int take);
        Task<ResearcherLocation?> GetResearcherLocationByName(string name);
        Task<ResearcherLocation> RequireResearcherLocationByName(string name);
    }
    [Service(typeof(IResearcherLocationRepository))]
    internal partial class ResearcherLocationRepository: IResearcherLocationRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public ResearcherLocationRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<ResearcherLocation> InsertResearcherLocation(ResearcherLocation entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            ResearcherLocationMetadata.Validate(entity);
            var sql = ResearcherLocationMetadata.InsertSQL();
            var parameters = ResearcherLocationMetadata.ExtractParameters(entity, ResearcherLocationMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<ResearcherLocation> UpdateResearcherLocation(ResearcherLocation entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            ResearcherLocationMetadata.Validate(entity);
            var sql = ResearcherLocationMetadata.UpdateSQL();
            var parameters = ResearcherLocationMetadata.ExtractParameters(entity, ResearcherLocationMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<ResearcherLocation> UpdateResearcherLocation(ResearcherLocation updated, ResearcherLocation original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateResearcherLocation(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteResearcherLocation(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = ResearcherLocationMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<ResearcherLocation>> QueryResearcherLocation(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<ResearcherLocation, ResearcherLocationMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<ResearcherLocation?> GetResearcherLocation(Guid id)
        {
            var sql = @"SELECT * FROM [ResearcherLocation] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<ResearcherLocation, ResearcherLocationMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<ResearcherLocation> RequireResearcherLocation(Guid id)
        {
            return await GetResearcherLocation(id) ?? throw new InvalidOperationException("The specified ResearcherLocation does not exist");
        }
        public async Task<int> CountResearcherLocation()
        {
            var sql = @"SELECT COUNT(Id) FROM [ResearcherLocation]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<ResearcherLocation>> SelectResearcherLocation()
        {
            var sql = @"SELECT * FROM [ResearcherLocation] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<ResearcherLocation, ResearcherLocationMetadata>(sql);
        }
        public async Task<List<ResearcherLocation>> SelectResearcherLocationWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [ResearcherLocation] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<ResearcherLocation, ResearcherLocationMetadata>(sql, parameters);
        }
        public async Task<ResearcherLocation?> GetResearcherLocationByName(string name)
        {
            var sql = @"SELECT * FROM [ResearcherLocation] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<ResearcherLocation, ResearcherLocationMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<ResearcherLocation> RequireResearcherLocationByName(string name)
        {
            return await GetResearcherLocationByName(name) ?? throw new InvalidOperationException("The specified ResearcherLocation does not exist");
        }
    }
}

