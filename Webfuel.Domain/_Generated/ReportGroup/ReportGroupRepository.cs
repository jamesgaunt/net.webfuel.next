using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain
{
    internal partial interface IReportGroupRepository
    {
        Task<ReportGroup> InsertReportGroup(ReportGroup entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<ReportGroup> UpdateReportGroup(ReportGroup entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<ReportGroup> UpdateReportGroup(ReportGroup updated, ReportGroup original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteReportGroup(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<ReportGroup>> QueryReportGroup(Query query, bool countTotal = true);
        Task<ReportGroup?> GetReportGroup(Guid id);
        Task<ReportGroup> RequireReportGroup(Guid id);
        Task<int> CountReportGroup();
        Task<List<ReportGroup>> SelectReportGroup();
        Task<List<ReportGroup>> SelectReportGroupWithPage(int skip, int take);
    }
    [Service(typeof(IReportGroupRepository))]
    internal partial class ReportGroupRepository: IReportGroupRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public ReportGroupRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<ReportGroup> InsertReportGroup(ReportGroup entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            ReportGroupMetadata.Validate(entity);
            var sql = ReportGroupMetadata.InsertSQL();
            var parameters = ReportGroupMetadata.ExtractParameters(entity, ReportGroupMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<ReportGroup> UpdateReportGroup(ReportGroup entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            ReportGroupMetadata.Validate(entity);
            var sql = ReportGroupMetadata.UpdateSQL();
            var parameters = ReportGroupMetadata.ExtractParameters(entity, ReportGroupMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<ReportGroup> UpdateReportGroup(ReportGroup updated, ReportGroup original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateReportGroup(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteReportGroup(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = ReportGroupMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<ReportGroup>> QueryReportGroup(Query query, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<ReportGroup, ReportGroupMetadata>(query, countTotal);
        }
        public async Task<ReportGroup?> GetReportGroup(Guid id)
        {
            var sql = @"SELECT * FROM [ReportGroup] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<ReportGroup, ReportGroupMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<ReportGroup> RequireReportGroup(Guid id)
        {
            return await GetReportGroup(id) ?? throw new InvalidOperationException("The specified ReportGroup does not exist");
        }
        public async Task<int> CountReportGroup()
        {
            var sql = @"SELECT COUNT(Id) FROM [ReportGroup]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<ReportGroup>> SelectReportGroup()
        {
            var sql = @"SELECT * FROM [ReportGroup] ORDER BY Id ASC";
            return await _connection.ExecuteReader<ReportGroup, ReportGroupMetadata>(sql);
        }
        public async Task<List<ReportGroup>> SelectReportGroupWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [ReportGroup] ORDER BY Id ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<ReportGroup, ReportGroupMetadata>(sql, parameters);
        }
    }
}

