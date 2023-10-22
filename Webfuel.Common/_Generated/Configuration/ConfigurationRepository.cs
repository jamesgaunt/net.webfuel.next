using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Common
{
    internal partial interface IConfigurationRepository
    {
        Task<Configuration> InsertConfiguration(Configuration entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<Configuration> UpdateConfiguration(Configuration entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<Configuration> UpdateConfiguration(Configuration updated, Configuration original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteConfiguration(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<Configuration>> QueryConfiguration(Query query);
        Task<Configuration?> GetConfiguration(Guid id);
        Task<Configuration> RequireConfiguration(Guid id);
        Task<int> CountConfiguration();
        Task<List<Configuration>> SelectConfiguration();
        Task<List<Configuration>> SelectConfigurationWithPage(int skip, int take);
    }
    [Service(typeof(IConfigurationRepository))]
    internal partial class ConfigurationRepository: IConfigurationRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public ConfigurationRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<Configuration> InsertConfiguration(Configuration entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            var sql = ConfigurationMetadata.InsertSQL();
            var parameters = ConfigurationMetadata.ExtractParameters(entity, ConfigurationMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<Configuration> UpdateConfiguration(Configuration entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = ConfigurationMetadata.UpdateSQL();
            var parameters = ConfigurationMetadata.ExtractParameters(entity, ConfigurationMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<Configuration> UpdateConfiguration(Configuration updated, Configuration original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateConfiguration(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteConfiguration(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = ConfigurationMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<Configuration>> QueryConfiguration(Query query)
        {
            return await _connection.ExecuteQuery<Configuration, ConfigurationMetadata>(query);
        }
        public async Task<Configuration?> GetConfiguration(Guid id)
        {
            var sql = @"SELECT * FROM [Configuration] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<Configuration, ConfigurationMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<Configuration> RequireConfiguration(Guid id)
        {
            return await GetConfiguration(id) ?? throw new InvalidOperationException("The specified Configuration does not exist");
        }
        public async Task<int> CountConfiguration()
        {
            var sql = @"SELECT COUNT(Id) FROM [Configuration]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<Configuration>> SelectConfiguration()
        {
            var sql = @"SELECT * FROM [Configuration] ORDER BY Id ASC";
            return await _connection.ExecuteReader<Configuration, ConfigurationMetadata>(sql);
        }
        public async Task<List<Configuration>> SelectConfigurationWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [Configuration] ORDER BY Id ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<Configuration, ConfigurationMetadata>(sql, parameters);
        }
    }
}

