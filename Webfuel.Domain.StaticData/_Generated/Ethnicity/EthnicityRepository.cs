using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IEthnicityRepository
    {
        Task<Ethnicity> InsertEthnicity(Ethnicity entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<Ethnicity> UpdateEthnicity(Ethnicity entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<Ethnicity> UpdateEthnicity(Ethnicity updated, Ethnicity original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteEthnicity(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<Ethnicity>> QueryEthnicity(Query query, bool countTotal = true);
        Task<Ethnicity?> GetEthnicity(Guid id);
        Task<Ethnicity> RequireEthnicity(Guid id);
        Task<int> CountEthnicity();
        Task<List<Ethnicity>> SelectEthnicity();
        Task<List<Ethnicity>> SelectEthnicityWithPage(int skip, int take);
        Task<Ethnicity?> GetEthnicityByName(string name);
        Task<Ethnicity> RequireEthnicityByName(string name);
    }
    [Service(typeof(IEthnicityRepository))]
    internal partial class EthnicityRepository: IEthnicityRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public EthnicityRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<Ethnicity> InsertEthnicity(Ethnicity entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            EthnicityMetadata.Validate(entity);
            var sql = EthnicityMetadata.InsertSQL();
            var parameters = EthnicityMetadata.ExtractParameters(entity, EthnicityMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<Ethnicity> UpdateEthnicity(Ethnicity entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            EthnicityMetadata.Validate(entity);
            var sql = EthnicityMetadata.UpdateSQL();
            var parameters = EthnicityMetadata.ExtractParameters(entity, EthnicityMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<Ethnicity> UpdateEthnicity(Ethnicity updated, Ethnicity original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateEthnicity(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteEthnicity(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = EthnicityMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<Ethnicity>> QueryEthnicity(Query query, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<Ethnicity, EthnicityMetadata>(query, countTotal);
        }
        public async Task<Ethnicity?> GetEthnicity(Guid id)
        {
            var sql = @"SELECT * FROM [Ethnicity] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<Ethnicity, EthnicityMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<Ethnicity> RequireEthnicity(Guid id)
        {
            return await GetEthnicity(id) ?? throw new InvalidOperationException("The specified Ethnicity does not exist");
        }
        public async Task<int> CountEthnicity()
        {
            var sql = @"SELECT COUNT(Id) FROM [Ethnicity]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<Ethnicity>> SelectEthnicity()
        {
            var sql = @"SELECT * FROM [Ethnicity] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<Ethnicity, EthnicityMetadata>(sql);
        }
        public async Task<List<Ethnicity>> SelectEthnicityWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [Ethnicity] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<Ethnicity, EthnicityMetadata>(sql, parameters);
        }
        public async Task<Ethnicity?> GetEthnicityByName(string name)
        {
            var sql = @"SELECT * FROM [Ethnicity] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<Ethnicity, EthnicityMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<Ethnicity> RequireEthnicityByName(string name)
        {
            return await GetEthnicityByName(name) ?? throw new InvalidOperationException("The specified Ethnicity does not exist");
        }
    }
}

