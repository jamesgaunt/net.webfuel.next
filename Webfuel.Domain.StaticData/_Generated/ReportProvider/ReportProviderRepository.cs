using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IReportProviderRepository
    {
        Task<QueryResult<ReportProvider>> QueryReportProvider(Query query, bool countTotal = true);
        Task<ReportProvider?> GetReportProvider(Guid id);
        Task<ReportProvider> RequireReportProvider(Guid id);
        Task<int> CountReportProvider();
        Task<List<ReportProvider>> SelectReportProvider();
        Task<List<ReportProvider>> SelectReportProviderWithPage(int skip, int take);
    }
    [Service(typeof(IReportProviderRepository))]
    internal partial class ReportProviderRepository: IReportProviderRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public ReportProviderRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<QueryResult<ReportProvider>> QueryReportProvider(Query query, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<ReportProvider, ReportProviderMetadata>(query, countTotal);
        }
        public async Task<ReportProvider?> GetReportProvider(Guid id)
        {
            var sql = @"SELECT * FROM [ReportProvider] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<ReportProvider, ReportProviderMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<ReportProvider> RequireReportProvider(Guid id)
        {
            return await GetReportProvider(id) ?? throw new InvalidOperationException("The specified ReportProvider does not exist");
        }
        public async Task<int> CountReportProvider()
        {
            var sql = @"SELECT COUNT(Id) FROM [ReportProvider]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<ReportProvider>> SelectReportProvider()
        {
            var sql = @"SELECT * FROM [ReportProvider] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<ReportProvider, ReportProviderMetadata>(sql);
        }
        public async Task<List<ReportProvider>> SelectReportProviderWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [ReportProvider] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<ReportProvider, ReportProviderMetadata>(sql, parameters);
        }
    }
}

