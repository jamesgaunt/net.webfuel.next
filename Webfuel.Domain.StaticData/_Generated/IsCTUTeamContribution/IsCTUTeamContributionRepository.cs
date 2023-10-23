using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IIsCTUTeamContributionRepository
    {
        Task<QueryResult<IsCTUTeamContribution>> QueryIsCTUTeamContribution(Query query);
        Task<IsCTUTeamContribution?> GetIsCTUTeamContribution(Guid id);
        Task<IsCTUTeamContribution> RequireIsCTUTeamContribution(Guid id);
        Task<int> CountIsCTUTeamContribution();
        Task<List<IsCTUTeamContribution>> SelectIsCTUTeamContribution();
        Task<List<IsCTUTeamContribution>> SelectIsCTUTeamContributionWithPage(int skip, int take);
        Task<IsCTUTeamContribution?> GetIsCTUTeamContributionByName(string name);
        Task<IsCTUTeamContribution> RequireIsCTUTeamContributionByName(string name);
    }
    [Service(typeof(IIsCTUTeamContributionRepository))]
    internal partial class IsCTUTeamContributionRepository: IIsCTUTeamContributionRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public IsCTUTeamContributionRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<QueryResult<IsCTUTeamContribution>> QueryIsCTUTeamContribution(Query query)
        {
            return await _connection.ExecuteQuery<IsCTUTeamContribution, IsCTUTeamContributionMetadata>(query);
        }
        public async Task<IsCTUTeamContribution?> GetIsCTUTeamContribution(Guid id)
        {
            var sql = @"SELECT * FROM [IsCTUTeamContribution] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<IsCTUTeamContribution, IsCTUTeamContributionMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<IsCTUTeamContribution> RequireIsCTUTeamContribution(Guid id)
        {
            return await GetIsCTUTeamContribution(id) ?? throw new InvalidOperationException("The specified IsCTUTeamContribution does not exist");
        }
        public async Task<int> CountIsCTUTeamContribution()
        {
            var sql = @"SELECT COUNT(Id) FROM [IsCTUTeamContribution]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<IsCTUTeamContribution>> SelectIsCTUTeamContribution()
        {
            var sql = @"SELECT * FROM [IsCTUTeamContribution] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<IsCTUTeamContribution, IsCTUTeamContributionMetadata>(sql);
        }
        public async Task<List<IsCTUTeamContribution>> SelectIsCTUTeamContributionWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [IsCTUTeamContribution] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<IsCTUTeamContribution, IsCTUTeamContributionMetadata>(sql, parameters);
        }
        public async Task<IsCTUTeamContribution?> GetIsCTUTeamContributionByName(string name)
        {
            var sql = @"SELECT * FROM [IsCTUTeamContribution] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<IsCTUTeamContribution, IsCTUTeamContributionMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<IsCTUTeamContribution> RequireIsCTUTeamContributionByName(string name)
        {
            return await GetIsCTUTeamContributionByName(name) ?? throw new InvalidOperationException("The specified IsCTUTeamContribution does not exist");
        }
    }
}

