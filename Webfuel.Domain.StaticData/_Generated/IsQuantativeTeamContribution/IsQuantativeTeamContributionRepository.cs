using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IIsQuantativeTeamContributionRepository
    {
        Task<QueryResult<IsQuantativeTeamContribution>> QueryIsQuantativeTeamContribution(Query query, bool countTotal = true);
        Task<IsQuantativeTeamContribution?> GetIsQuantativeTeamContribution(Guid id);
        Task<IsQuantativeTeamContribution> RequireIsQuantativeTeamContribution(Guid id);
        Task<int> CountIsQuantativeTeamContribution();
        Task<List<IsQuantativeTeamContribution>> SelectIsQuantativeTeamContribution();
        Task<List<IsQuantativeTeamContribution>> SelectIsQuantativeTeamContributionWithPage(int skip, int take);
        Task<IsQuantativeTeamContribution?> GetIsQuantativeTeamContributionByName(string name);
        Task<IsQuantativeTeamContribution> RequireIsQuantativeTeamContributionByName(string name);
    }
    [Service(typeof(IIsQuantativeTeamContributionRepository))]
    internal partial class IsQuantativeTeamContributionRepository: IIsQuantativeTeamContributionRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public IsQuantativeTeamContributionRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<QueryResult<IsQuantativeTeamContribution>> QueryIsQuantativeTeamContribution(Query query, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<IsQuantativeTeamContribution, IsQuantativeTeamContributionMetadata>(query, countTotal);
        }
        public async Task<IsQuantativeTeamContribution?> GetIsQuantativeTeamContribution(Guid id)
        {
            var sql = @"SELECT * FROM [IsQuantativeTeamContribution] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<IsQuantativeTeamContribution, IsQuantativeTeamContributionMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<IsQuantativeTeamContribution> RequireIsQuantativeTeamContribution(Guid id)
        {
            return await GetIsQuantativeTeamContribution(id) ?? throw new InvalidOperationException("The specified IsQuantativeTeamContribution does not exist");
        }
        public async Task<int> CountIsQuantativeTeamContribution()
        {
            var sql = @"SELECT COUNT(Id) FROM [IsQuantativeTeamContribution]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<IsQuantativeTeamContribution>> SelectIsQuantativeTeamContribution()
        {
            var sql = @"SELECT * FROM [IsQuantativeTeamContribution] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<IsQuantativeTeamContribution, IsQuantativeTeamContributionMetadata>(sql);
        }
        public async Task<List<IsQuantativeTeamContribution>> SelectIsQuantativeTeamContributionWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [IsQuantativeTeamContribution] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<IsQuantativeTeamContribution, IsQuantativeTeamContributionMetadata>(sql, parameters);
        }
        public async Task<IsQuantativeTeamContribution?> GetIsQuantativeTeamContributionByName(string name)
        {
            var sql = @"SELECT * FROM [IsQuantativeTeamContribution] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<IsQuantativeTeamContribution, IsQuantativeTeamContributionMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<IsQuantativeTeamContribution> RequireIsQuantativeTeamContributionByName(string name)
        {
            return await GetIsQuantativeTeamContributionByName(name) ?? throw new InvalidOperationException("The specified IsQuantativeTeamContribution does not exist");
        }
    }
}

