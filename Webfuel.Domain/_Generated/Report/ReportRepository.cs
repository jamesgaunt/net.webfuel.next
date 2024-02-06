using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain
{
    internal partial interface IReportRepository
    {
        Task<Report> InsertReport(Report entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<Report> UpdateReport(Report entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<Report> UpdateReport(Report updated, Report original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteReport(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<Report>> QueryReport(Query query, bool selectItems = true, bool countTotal = true);
        Task<Report?> GetReport(Guid id);
        Task<Report> RequireReport(Guid id);
        Task<int> CountReport();
        Task<List<Report>> SelectReport();
        Task<List<Report>> SelectReportWithPage(int skip, int take);
        Task<List<Report>> SelectReportByOwnerUserId(Guid ownerUserId);
        Task<List<Report>> SelectReportByNameAndReportProviderId(string name, Guid reportProviderId);
    }
    [Service(typeof(IReportRepository))]
    internal partial class ReportRepository: IReportRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public ReportRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<Report> InsertReport(Report entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            ReportMetadata.Validate(entity);
            var sql = ReportMetadata.InsertSQL();
            var parameters = ReportMetadata.ExtractParameters(entity, ReportMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<Report> UpdateReport(Report entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            ReportMetadata.Validate(entity);
            var sql = ReportMetadata.UpdateSQL();
            var parameters = ReportMetadata.ExtractParameters(entity, ReportMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<Report> UpdateReport(Report updated, Report original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateReport(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteReport(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = ReportMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<Report>> QueryReport(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<Report, ReportMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<Report?> GetReport(Guid id)
        {
            var sql = @"SELECT * FROM [Report] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<Report, ReportMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<Report> RequireReport(Guid id)
        {
            return await GetReport(id) ?? throw new InvalidOperationException("The specified Report does not exist");
        }
        public async Task<int> CountReport()
        {
            var sql = @"SELECT COUNT(Id) FROM [Report]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<Report>> SelectReport()
        {
            var sql = @"SELECT * FROM [Report] ORDER BY Id ASC";
            return await _connection.ExecuteReader<Report, ReportMetadata>(sql);
        }
        public async Task<List<Report>> SelectReportWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [Report] ORDER BY Id ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<Report, ReportMetadata>(sql, parameters);
        }
        public async Task<List<Report>> SelectReportByOwnerUserId(Guid ownerUserId)
        {
            var sql = @"SELECT * FROM [Report] WHERE OwnerUserId = @OwnerUserId ORDER BY Id ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@OwnerUserId", ownerUserId),
            };
            return await _connection.ExecuteReader<Report, ReportMetadata>(sql, parameters);
        }
        public async Task<List<Report>> SelectReportByNameAndReportProviderId(string name, Guid reportProviderId)
        {
            var sql = @"SELECT * FROM [Report] WHERE Name = @Name AND ReportProviderId = @ReportProviderId ORDER BY Id ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
                new SqlParameter("@ReportProviderId", reportProviderId),
            };
            return await _connection.ExecuteReader<Report, ReportMetadata>(sql, parameters);
        }
    }
}

