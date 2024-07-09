using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain
{
    internal partial interface ISupportTeamUserRepository
    {
        Task<SupportTeamUser> InsertSupportTeamUser(SupportTeamUser entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<SupportTeamUser> UpdateSupportTeamUser(SupportTeamUser entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<SupportTeamUser> UpdateSupportTeamUser(SupportTeamUser updated, SupportTeamUser original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteSupportTeamUser(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<SupportTeamUser>> QuerySupportTeamUser(Query query, bool selectItems = true, bool countTotal = true);
        Task<SupportTeamUser?> GetSupportTeamUser(Guid id);
        Task<SupportTeamUser> RequireSupportTeamUser(Guid id);
        Task<int> CountSupportTeamUser();
        Task<List<SupportTeamUser>> SelectSupportTeamUser();
        Task<List<SupportTeamUser>> SelectSupportTeamUserWithPage(int skip, int take);
        Task<List<SupportTeamUser>> SelectSupportTeamUserBySupportTeamIdAndIsTeamLead(Guid supportTeamId, bool isTeamLead);
        Task<List<SupportTeamUser>> SelectSupportTeamUserBySupportTeamId(Guid supportTeamId);
        Task<List<SupportTeamUser>> SelectSupportTeamUserByUserId(Guid userId);
        Task<SupportTeamUser?> GetSupportTeamUserByUserIdAndSupportTeamId(Guid userId, Guid supportTeamId);
        Task<SupportTeamUser> RequireSupportTeamUserByUserIdAndSupportTeamId(Guid userId, Guid supportTeamId);
    }
    [Service(typeof(ISupportTeamUserRepository))]
    internal partial class SupportTeamUserRepository: ISupportTeamUserRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public SupportTeamUserRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<SupportTeamUser> InsertSupportTeamUser(SupportTeamUser entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            SupportTeamUserMetadata.Validate(entity);
            var sql = SupportTeamUserMetadata.InsertSQL();
            var parameters = SupportTeamUserMetadata.ExtractParameters(entity, SupportTeamUserMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<SupportTeamUser> UpdateSupportTeamUser(SupportTeamUser entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            SupportTeamUserMetadata.Validate(entity);
            var sql = SupportTeamUserMetadata.UpdateSQL();
            var parameters = SupportTeamUserMetadata.ExtractParameters(entity, SupportTeamUserMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<SupportTeamUser> UpdateSupportTeamUser(SupportTeamUser updated, SupportTeamUser original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateSupportTeamUser(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteSupportTeamUser(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = SupportTeamUserMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<SupportTeamUser>> QuerySupportTeamUser(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<SupportTeamUser, SupportTeamUserMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<SupportTeamUser?> GetSupportTeamUser(Guid id)
        {
            var sql = @"SELECT * FROM [SupportTeamUser] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<SupportTeamUser, SupportTeamUserMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<SupportTeamUser> RequireSupportTeamUser(Guid id)
        {
            return await GetSupportTeamUser(id) ?? throw new InvalidOperationException("The specified SupportTeamUser does not exist");
        }
        public async Task<int> CountSupportTeamUser()
        {
            var sql = @"SELECT COUNT(Id) FROM [SupportTeamUser]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<SupportTeamUser>> SelectSupportTeamUser()
        {
            var sql = @"SELECT * FROM [SupportTeamUser] ORDER BY Id ASC";
            return await _connection.ExecuteReader<SupportTeamUser, SupportTeamUserMetadata>(sql);
        }
        public async Task<List<SupportTeamUser>> SelectSupportTeamUserWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [SupportTeamUser] ORDER BY Id ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<SupportTeamUser, SupportTeamUserMetadata>(sql, parameters);
        }
        public async Task<List<SupportTeamUser>> SelectSupportTeamUserBySupportTeamIdAndIsTeamLead(Guid supportTeamId, bool isTeamLead)
        {
            var sql = @"SELECT * FROM [SupportTeamUser] WHERE SupportTeamId = @SupportTeamId AND IsTeamLead = @IsTeamLead ORDER BY Id ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@SupportTeamId", supportTeamId),
                new SqlParameter("@IsTeamLead", isTeamLead),
            };
            return await _connection.ExecuteReader<SupportTeamUser, SupportTeamUserMetadata>(sql, parameters);
        }
        public async Task<List<SupportTeamUser>> SelectSupportTeamUserBySupportTeamId(Guid supportTeamId)
        {
            var sql = @"SELECT * FROM [SupportTeamUser] WHERE SupportTeamId = @SupportTeamId ORDER BY Id ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@SupportTeamId", supportTeamId),
            };
            return await _connection.ExecuteReader<SupportTeamUser, SupportTeamUserMetadata>(sql, parameters);
        }
        public async Task<List<SupportTeamUser>> SelectSupportTeamUserByUserId(Guid userId)
        {
            var sql = @"SELECT * FROM [SupportTeamUser] WHERE UserId = @UserId ORDER BY Id ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@UserId", userId),
            };
            return await _connection.ExecuteReader<SupportTeamUser, SupportTeamUserMetadata>(sql, parameters);
        }
        public async Task<SupportTeamUser?> GetSupportTeamUserByUserIdAndSupportTeamId(Guid userId, Guid supportTeamId)
        {
            var sql = @"SELECT * FROM [SupportTeamUser] WHERE UserId = @UserId AND SupportTeamId = @SupportTeamId ORDER BY Id ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@SupportTeamId", supportTeamId),
            };
            return (await _connection.ExecuteReader<SupportTeamUser, SupportTeamUserMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<SupportTeamUser> RequireSupportTeamUserByUserIdAndSupportTeamId(Guid userId, Guid supportTeamId)
        {
            return await GetSupportTeamUserByUserIdAndSupportTeamId(userId, supportTeamId) ?? throw new InvalidOperationException("The specified SupportTeamUser does not exist");
        }
    }
}

