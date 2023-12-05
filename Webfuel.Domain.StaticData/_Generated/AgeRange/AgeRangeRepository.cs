using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IAgeRangeRepository
    {
        Task<AgeRange> InsertAgeRange(AgeRange entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<AgeRange> UpdateAgeRange(AgeRange entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<AgeRange> UpdateAgeRange(AgeRange updated, AgeRange original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteAgeRange(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<AgeRange>> QueryAgeRange(Query query, bool selectItems = true, bool countTotal = true);
        Task<AgeRange?> GetAgeRange(Guid id);
        Task<AgeRange> RequireAgeRange(Guid id);
        Task<int> CountAgeRange();
        Task<List<AgeRange>> SelectAgeRange();
        Task<List<AgeRange>> SelectAgeRangeWithPage(int skip, int take);
        Task<AgeRange?> GetAgeRangeByName(string name);
        Task<AgeRange> RequireAgeRangeByName(string name);
    }
    [Service(typeof(IAgeRangeRepository))]
    internal partial class AgeRangeRepository: IAgeRangeRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public AgeRangeRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<AgeRange> InsertAgeRange(AgeRange entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            AgeRangeMetadata.Validate(entity);
            var sql = AgeRangeMetadata.InsertSQL();
            var parameters = AgeRangeMetadata.ExtractParameters(entity, AgeRangeMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<AgeRange> UpdateAgeRange(AgeRange entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            AgeRangeMetadata.Validate(entity);
            var sql = AgeRangeMetadata.UpdateSQL();
            var parameters = AgeRangeMetadata.ExtractParameters(entity, AgeRangeMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<AgeRange> UpdateAgeRange(AgeRange updated, AgeRange original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateAgeRange(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteAgeRange(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = AgeRangeMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<AgeRange>> QueryAgeRange(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<AgeRange, AgeRangeMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<AgeRange?> GetAgeRange(Guid id)
        {
            var sql = @"SELECT * FROM [AgeRange] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<AgeRange, AgeRangeMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<AgeRange> RequireAgeRange(Guid id)
        {
            return await GetAgeRange(id) ?? throw new InvalidOperationException("The specified AgeRange does not exist");
        }
        public async Task<int> CountAgeRange()
        {
            var sql = @"SELECT COUNT(Id) FROM [AgeRange]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<AgeRange>> SelectAgeRange()
        {
            var sql = @"SELECT * FROM [AgeRange] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<AgeRange, AgeRangeMetadata>(sql);
        }
        public async Task<List<AgeRange>> SelectAgeRangeWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [AgeRange] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<AgeRange, AgeRangeMetadata>(sql, parameters);
        }
        public async Task<AgeRange?> GetAgeRangeByName(string name)
        {
            var sql = @"SELECT * FROM [AgeRange] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<AgeRange, AgeRangeMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<AgeRange> RequireAgeRangeByName(string name)
        {
            return await GetAgeRangeByName(name) ?? throw new InvalidOperationException("The specified AgeRange does not exist");
        }
    }
}

