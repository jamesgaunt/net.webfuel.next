using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IRSSHubRepository
    {
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

