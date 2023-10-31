using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IDisabilityRepository
    {
        Task<Disability> InsertDisability(Disability entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<Disability> UpdateDisability(Disability entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<Disability> UpdateDisability(Disability updated, Disability original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteDisability(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<Disability>> QueryDisability(Query query);
        Task<Disability?> GetDisability(Guid id);
        Task<Disability> RequireDisability(Guid id);
        Task<int> CountDisability();
        Task<List<Disability>> SelectDisability();
        Task<List<Disability>> SelectDisabilityWithPage(int skip, int take);
        Task<Disability?> GetDisabilityByName(string name);
        Task<Disability> RequireDisabilityByName(string name);
    }
    [Service(typeof(IDisabilityRepository))]
    internal partial class DisabilityRepository: IDisabilityRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public DisabilityRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<Disability> InsertDisability(Disability entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            var sql = DisabilityMetadata.InsertSQL();
            var parameters = DisabilityMetadata.ExtractParameters(entity, DisabilityMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<Disability> UpdateDisability(Disability entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = DisabilityMetadata.UpdateSQL();
            var parameters = DisabilityMetadata.ExtractParameters(entity, DisabilityMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<Disability> UpdateDisability(Disability updated, Disability original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateDisability(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteDisability(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = DisabilityMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<Disability>> QueryDisability(Query query)
        {
            return await _connection.ExecuteQuery<Disability, DisabilityMetadata>(query);
        }
        public async Task<Disability?> GetDisability(Guid id)
        {
            var sql = @"SELECT * FROM [Disability] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<Disability, DisabilityMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<Disability> RequireDisability(Guid id)
        {
            return await GetDisability(id) ?? throw new InvalidOperationException("The specified Disability does not exist");
        }
        public async Task<int> CountDisability()
        {
            var sql = @"SELECT COUNT(Id) FROM [Disability]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<Disability>> SelectDisability()
        {
            var sql = @"SELECT * FROM [Disability] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<Disability, DisabilityMetadata>(sql);
        }
        public async Task<List<Disability>> SelectDisabilityWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [Disability] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<Disability, DisabilityMetadata>(sql, parameters);
        }
        public async Task<Disability?> GetDisabilityByName(string name)
        {
            var sql = @"SELECT * FROM [Disability] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<Disability, DisabilityMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<Disability> RequireDisabilityByName(string name)
        {
            return await GetDisabilityByName(name) ?? throw new InvalidOperationException("The specified Disability does not exist");
        }
    }
}

