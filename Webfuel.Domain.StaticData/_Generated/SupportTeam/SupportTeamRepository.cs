using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface ISupportTeamRepository
    {
        Task<QueryResult<SupportTeam>> QuerySupportTeam(Query query, bool selectItems = true, bool countTotal = true);
        Task<SupportTeam?> GetSupportTeam(Guid id);
        Task<SupportTeam> RequireSupportTeam(Guid id);
        Task<int> CountSupportTeam();
        Task<List<SupportTeam>> SelectSupportTeam();
        Task<List<SupportTeam>> SelectSupportTeamWithPage(int skip, int take);
        Task<SupportTeam?> GetSupportTeamByName(string name);
        Task<SupportTeam> RequireSupportTeamByName(string name);
    }
    [Service(typeof(ISupportTeamRepository))]
    internal partial class SupportTeamRepository: ISupportTeamRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public SupportTeamRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<QueryResult<SupportTeam>> QuerySupportTeam(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<SupportTeam, SupportTeamMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<SupportTeam?> GetSupportTeam(Guid id)
        {
            var sql = @"SELECT * FROM [SupportTeam] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<SupportTeam, SupportTeamMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<SupportTeam> RequireSupportTeam(Guid id)
        {
            return await GetSupportTeam(id) ?? throw new InvalidOperationException("The specified SupportTeam does not exist");
        }
        public async Task<int> CountSupportTeam()
        {
            var sql = @"SELECT COUNT(Id) FROM [SupportTeam]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<SupportTeam>> SelectSupportTeam()
        {
            var sql = @"SELECT * FROM [SupportTeam] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<SupportTeam, SupportTeamMetadata>(sql);
        }
        public async Task<List<SupportTeam>> SelectSupportTeamWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [SupportTeam] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<SupportTeam, SupportTeamMetadata>(sql, parameters);
        }
        public async Task<SupportTeam?> GetSupportTeamByName(string name)
        {
            var sql = @"SELECT * FROM [SupportTeam] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<SupportTeam, SupportTeamMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<SupportTeam> RequireSupportTeamByName(string name)
        {
            return await GetSupportTeamByName(name) ?? throw new InvalidOperationException("The specified SupportTeam does not exist");
        }
    }
}

