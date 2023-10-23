using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IIsPPIEAndEDIContributionRepository
    {
        Task<QueryResult<IsPPIEAndEDIContribution>> QueryIsPPIEAndEDIContribution(Query query);
        Task<IsPPIEAndEDIContribution?> GetIsPPIEAndEDIContribution(Guid id);
        Task<IsPPIEAndEDIContribution> RequireIsPPIEAndEDIContribution(Guid id);
        Task<int> CountIsPPIEAndEDIContribution();
        Task<List<IsPPIEAndEDIContribution>> SelectIsPPIEAndEDIContribution();
        Task<List<IsPPIEAndEDIContribution>> SelectIsPPIEAndEDIContributionWithPage(int skip, int take);
        Task<IsPPIEAndEDIContribution?> GetIsPPIEAndEDIContributionByName(string name);
        Task<IsPPIEAndEDIContribution> RequireIsPPIEAndEDIContributionByName(string name);
    }
    [Service(typeof(IIsPPIEAndEDIContributionRepository))]
    internal partial class IsPPIEAndEDIContributionRepository: IIsPPIEAndEDIContributionRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public IsPPIEAndEDIContributionRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<QueryResult<IsPPIEAndEDIContribution>> QueryIsPPIEAndEDIContribution(Query query)
        {
            return await _connection.ExecuteQuery<IsPPIEAndEDIContribution, IsPPIEAndEDIContributionMetadata>(query);
        }
        public async Task<IsPPIEAndEDIContribution?> GetIsPPIEAndEDIContribution(Guid id)
        {
            var sql = @"SELECT * FROM [IsPPIEAndEDIContribution] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<IsPPIEAndEDIContribution, IsPPIEAndEDIContributionMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<IsPPIEAndEDIContribution> RequireIsPPIEAndEDIContribution(Guid id)
        {
            return await GetIsPPIEAndEDIContribution(id) ?? throw new InvalidOperationException("The specified IsPPIEAndEDIContribution does not exist");
        }
        public async Task<int> CountIsPPIEAndEDIContribution()
        {
            var sql = @"SELECT COUNT(Id) FROM [IsPPIEAndEDIContribution]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<IsPPIEAndEDIContribution>> SelectIsPPIEAndEDIContribution()
        {
            var sql = @"SELECT * FROM [IsPPIEAndEDIContribution] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<IsPPIEAndEDIContribution, IsPPIEAndEDIContributionMetadata>(sql);
        }
        public async Task<List<IsPPIEAndEDIContribution>> SelectIsPPIEAndEDIContributionWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [IsPPIEAndEDIContribution] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<IsPPIEAndEDIContribution, IsPPIEAndEDIContributionMetadata>(sql, parameters);
        }
        public async Task<IsPPIEAndEDIContribution?> GetIsPPIEAndEDIContributionByName(string name)
        {
            var sql = @"SELECT * FROM [IsPPIEAndEDIContribution] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<IsPPIEAndEDIContribution, IsPPIEAndEDIContributionMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<IsPPIEAndEDIContribution> RequireIsPPIEAndEDIContributionByName(string name)
        {
            return await GetIsPPIEAndEDIContributionByName(name) ?? throw new InvalidOperationException("The specified IsPPIEAndEDIContribution does not exist");
        }
    }
}

