using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IFundingCallTypeRepository
    {
        Task<FundingCallType> InsertFundingCallType(FundingCallType entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<FundingCallType> UpdateFundingCallType(FundingCallType entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<FundingCallType> UpdateFundingCallType(FundingCallType updated, FundingCallType original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteFundingCallType(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<FundingCallType>> QueryFundingCallType(Query query);
        Task<FundingCallType?> GetFundingCallType(Guid id);
        Task<FundingCallType> RequireFundingCallType(Guid id);
        Task<int> CountFundingCallType();
        Task<List<FundingCallType>> SelectFundingCallType();
        Task<List<FundingCallType>> SelectFundingCallTypeWithPage(int skip, int take);
        Task<FundingCallType?> GetFundingCallTypeByName(string name);
        Task<FundingCallType> RequireFundingCallTypeByName(string name);
    }
    [Service(typeof(IFundingCallTypeRepository))]
    internal partial class FundingCallTypeRepository: IFundingCallTypeRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public FundingCallTypeRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<FundingCallType> InsertFundingCallType(FundingCallType entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            FundingCallTypeMetadata.Validate(entity);
            var sql = FundingCallTypeMetadata.InsertSQL();
            var parameters = FundingCallTypeMetadata.ExtractParameters(entity, FundingCallTypeMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<FundingCallType> UpdateFundingCallType(FundingCallType entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            FundingCallTypeMetadata.Validate(entity);
            var sql = FundingCallTypeMetadata.UpdateSQL();
            var parameters = FundingCallTypeMetadata.ExtractParameters(entity, FundingCallTypeMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<FundingCallType> UpdateFundingCallType(FundingCallType updated, FundingCallType original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateFundingCallType(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteFundingCallType(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = FundingCallTypeMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<FundingCallType>> QueryFundingCallType(Query query)
        {
            return await _connection.ExecuteQuery<FundingCallType, FundingCallTypeMetadata>(query);
        }
        public async Task<FundingCallType?> GetFundingCallType(Guid id)
        {
            var sql = @"SELECT * FROM [FundingCallType] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<FundingCallType, FundingCallTypeMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<FundingCallType> RequireFundingCallType(Guid id)
        {
            return await GetFundingCallType(id) ?? throw new InvalidOperationException("The specified FundingCallType does not exist");
        }
        public async Task<int> CountFundingCallType()
        {
            var sql = @"SELECT COUNT(Id) FROM [FundingCallType]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<FundingCallType>> SelectFundingCallType()
        {
            var sql = @"SELECT * FROM [FundingCallType] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<FundingCallType, FundingCallTypeMetadata>(sql);
        }
        public async Task<List<FundingCallType>> SelectFundingCallTypeWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [FundingCallType] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<FundingCallType, FundingCallTypeMetadata>(sql, parameters);
        }
        public async Task<FundingCallType?> GetFundingCallTypeByName(string name)
        {
            var sql = @"SELECT * FROM [FundingCallType] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<FundingCallType, FundingCallTypeMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<FundingCallType> RequireFundingCallTypeByName(string name)
        {
            return await GetFundingCallTypeByName(name) ?? throw new InvalidOperationException("The specified FundingCallType does not exist");
        }
    }
}

