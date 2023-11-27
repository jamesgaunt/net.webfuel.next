using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IApplicationStageRepository
    {
        Task<ApplicationStage> InsertApplicationStage(ApplicationStage entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<ApplicationStage> UpdateApplicationStage(ApplicationStage entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<ApplicationStage> UpdateApplicationStage(ApplicationStage updated, ApplicationStage original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteApplicationStage(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<ApplicationStage>> QueryApplicationStage(Query query, bool countTotal = true);
        Task<ApplicationStage?> GetApplicationStage(Guid id);
        Task<ApplicationStage> RequireApplicationStage(Guid id);
        Task<int> CountApplicationStage();
        Task<List<ApplicationStage>> SelectApplicationStage();
        Task<List<ApplicationStage>> SelectApplicationStageWithPage(int skip, int take);
        Task<ApplicationStage?> GetApplicationStageByName(string name);
        Task<ApplicationStage> RequireApplicationStageByName(string name);
    }
    [Service(typeof(IApplicationStageRepository))]
    internal partial class ApplicationStageRepository: IApplicationStageRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public ApplicationStageRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<ApplicationStage> InsertApplicationStage(ApplicationStage entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            ApplicationStageMetadata.Validate(entity);
            var sql = ApplicationStageMetadata.InsertSQL();
            var parameters = ApplicationStageMetadata.ExtractParameters(entity, ApplicationStageMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<ApplicationStage> UpdateApplicationStage(ApplicationStage entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            ApplicationStageMetadata.Validate(entity);
            var sql = ApplicationStageMetadata.UpdateSQL();
            var parameters = ApplicationStageMetadata.ExtractParameters(entity, ApplicationStageMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<ApplicationStage> UpdateApplicationStage(ApplicationStage updated, ApplicationStage original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateApplicationStage(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteApplicationStage(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = ApplicationStageMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<ApplicationStage>> QueryApplicationStage(Query query, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<ApplicationStage, ApplicationStageMetadata>(query, countTotal);
        }
        public async Task<ApplicationStage?> GetApplicationStage(Guid id)
        {
            var sql = @"SELECT * FROM [ApplicationStage] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<ApplicationStage, ApplicationStageMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<ApplicationStage> RequireApplicationStage(Guid id)
        {
            return await GetApplicationStage(id) ?? throw new InvalidOperationException("The specified ApplicationStage does not exist");
        }
        public async Task<int> CountApplicationStage()
        {
            var sql = @"SELECT COUNT(Id) FROM [ApplicationStage]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<ApplicationStage>> SelectApplicationStage()
        {
            var sql = @"SELECT * FROM [ApplicationStage] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<ApplicationStage, ApplicationStageMetadata>(sql);
        }
        public async Task<List<ApplicationStage>> SelectApplicationStageWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [ApplicationStage] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<ApplicationStage, ApplicationStageMetadata>(sql, parameters);
        }
        public async Task<ApplicationStage?> GetApplicationStageByName(string name)
        {
            var sql = @"SELECT * FROM [ApplicationStage] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<ApplicationStage, ApplicationStageMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<ApplicationStage> RequireApplicationStageByName(string name)
        {
            return await GetApplicationStageByName(name) ?? throw new InvalidOperationException("The specified ApplicationStage does not exist");
        }
    }
}

