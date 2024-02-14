using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IRSSHubRepository
    {
        Task<RSSHub> InsertRSSHub(RSSHub entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<RSSHub> UpdateRSSHub(RSSHub entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<RSSHub> UpdateRSSHub(RSSHub updated, RSSHub original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteRSSHub(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<RSSHub>> QueryRSSHub(Query query, bool selectItems = true, bool countTotal = true);
        Task<RSSHub?> GetRSSHub(Guid id);
        Task<RSSHub> RequireRSSHub(Guid id);
        Task<int> CountRSSHub();
        Task<List<RSSHub>> SelectRSSHub();
        Task<List<RSSHub>> SelectRSSHubWithPage(int skip, int take);
        Task<RSSHub?> GetRSSHubByName(string name);
        Task<RSSHub> RequireRSSHubByName(string name);
    }
    [Service(typeof(IRSSHubRepository))]
    internal partial class RSSHubRepository: IRSSHubRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public RSSHubRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<RSSHub> InsertRSSHub(RSSHub entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            RSSHubMetadata.Validate(entity);
            var sql = RSSHubMetadata.InsertSQL();
            var parameters = RSSHubMetadata.ExtractParameters(entity, RSSHubMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<RSSHub> UpdateRSSHub(RSSHub entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            RSSHubMetadata.Validate(entity);
            var sql = RSSHubMetadata.UpdateSQL();
            var parameters = RSSHubMetadata.ExtractParameters(entity, RSSHubMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<RSSHub> UpdateRSSHub(RSSHub updated, RSSHub original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateRSSHub(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteRSSHub(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = RSSHubMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<RSSHub>> QueryRSSHub(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<RSSHub, RSSHubMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<RSSHub?> GetRSSHub(Guid id)
        {
            var sql = @"SELECT * FROM [RSSHub] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<RSSHub, RSSHubMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<RSSHub> RequireRSSHub(Guid id)
        {
            return await GetRSSHub(id) ?? throw new InvalidOperationException("The specified RSSHub does not exist");
        }
        public async Task<int> CountRSSHub()
        {
            var sql = @"SELECT COUNT(Id) FROM [RSSHub]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<RSSHub>> SelectRSSHub()
        {
            var sql = @"SELECT * FROM [RSSHub] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<RSSHub, RSSHubMetadata>(sql);
        }
        public async Task<List<RSSHub>> SelectRSSHubWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [RSSHub] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<RSSHub, RSSHubMetadata>(sql, parameters);
        }
        public async Task<RSSHub?> GetRSSHubByName(string name)
        {
            var sql = @"SELECT * FROM [RSSHub] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<RSSHub, RSSHubMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<RSSHub> RequireRSSHubByName(string name)
        {
            return await GetRSSHubByName(name) ?? throw new InvalidOperationException("The specified RSSHub does not exist");
        }
    }
}

