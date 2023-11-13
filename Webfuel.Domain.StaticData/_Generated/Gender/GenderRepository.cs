using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IGenderRepository
    {
        Task<Gender> InsertGender(Gender entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<Gender> UpdateGender(Gender entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<Gender> UpdateGender(Gender updated, Gender original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteGender(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<Gender>> QueryGender(Query query);
        Task<Gender?> GetGender(Guid id);
        Task<Gender> RequireGender(Guid id);
        Task<int> CountGender();
        Task<List<Gender>> SelectGender();
        Task<List<Gender>> SelectGenderWithPage(int skip, int take);
        Task<Gender?> GetGenderByName(string name);
        Task<Gender> RequireGenderByName(string name);
    }
    [Service(typeof(IGenderRepository))]
    internal partial class GenderRepository: IGenderRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public GenderRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<Gender> InsertGender(Gender entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            GenderMetadata.Validate(entity);
            var sql = GenderMetadata.InsertSQL();
            var parameters = GenderMetadata.ExtractParameters(entity, GenderMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<Gender> UpdateGender(Gender entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            GenderMetadata.Validate(entity);
            var sql = GenderMetadata.UpdateSQL();
            var parameters = GenderMetadata.ExtractParameters(entity, GenderMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<Gender> UpdateGender(Gender updated, Gender original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateGender(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteGender(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = GenderMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<Gender>> QueryGender(Query query)
        {
            return await _connection.ExecuteQuery<Gender, GenderMetadata>(query);
        }
        public async Task<Gender?> GetGender(Guid id)
        {
            var sql = @"SELECT * FROM [Gender] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<Gender, GenderMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<Gender> RequireGender(Guid id)
        {
            return await GetGender(id) ?? throw new InvalidOperationException("The specified Gender does not exist");
        }
        public async Task<int> CountGender()
        {
            var sql = @"SELECT COUNT(Id) FROM [Gender]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<Gender>> SelectGender()
        {
            var sql = @"SELECT * FROM [Gender] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<Gender, GenderMetadata>(sql);
        }
        public async Task<List<Gender>> SelectGenderWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [Gender] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<Gender, GenderMetadata>(sql, parameters);
        }
        public async Task<Gender?> GetGenderByName(string name)
        {
            var sql = @"SELECT * FROM [Gender] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<Gender, GenderMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<Gender> RequireGenderByName(string name)
        {
            return await GetGenderByName(name) ?? throw new InvalidOperationException("The specified Gender does not exist");
        }
    }
}

