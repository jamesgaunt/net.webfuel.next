using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface ISupportRequestStatusRepository
    {
        Task<QueryResult<SupportRequestStatus>> QuerySupportRequestStatus(Query query, bool selectItems = true, bool countTotal = true);
        Task<SupportRequestStatus?> GetSupportRequestStatus(Guid id);
        Task<SupportRequestStatus> RequireSupportRequestStatus(Guid id);
        Task<int> CountSupportRequestStatus();
        Task<List<SupportRequestStatus>> SelectSupportRequestStatus();
        Task<List<SupportRequestStatus>> SelectSupportRequestStatusWithPage(int skip, int take);
        Task<SupportRequestStatus?> GetSupportRequestStatusByName(string name);
        Task<SupportRequestStatus> RequireSupportRequestStatusByName(string name);
    }
    [Service(typeof(ISupportRequestStatusRepository))]
    internal partial class SupportRequestStatusRepository: ISupportRequestStatusRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public SupportRequestStatusRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<QueryResult<SupportRequestStatus>> QuerySupportRequestStatus(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<SupportRequestStatus, SupportRequestStatusMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<SupportRequestStatus?> GetSupportRequestStatus(Guid id)
        {
            var sql = @"SELECT * FROM [SupportRequestStatus] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<SupportRequestStatus, SupportRequestStatusMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<SupportRequestStatus> RequireSupportRequestStatus(Guid id)
        {
            return await GetSupportRequestStatus(id) ?? throw new InvalidOperationException("The specified SupportRequestStatus does not exist");
        }
        public async Task<int> CountSupportRequestStatus()
        {
            var sql = @"SELECT COUNT(Id) FROM [SupportRequestStatus]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<SupportRequestStatus>> SelectSupportRequestStatus()
        {
            var sql = @"SELECT * FROM [SupportRequestStatus] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<SupportRequestStatus, SupportRequestStatusMetadata>(sql);
        }
        public async Task<List<SupportRequestStatus>> SelectSupportRequestStatusWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [SupportRequestStatus] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<SupportRequestStatus, SupportRequestStatusMetadata>(sql, parameters);
        }
        public async Task<SupportRequestStatus?> GetSupportRequestStatusByName(string name)
        {
            var sql = @"SELECT * FROM [SupportRequestStatus] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<SupportRequestStatus, SupportRequestStatusMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<SupportRequestStatus> RequireSupportRequestStatusByName(string name)
        {
            return await GetSupportRequestStatusByName(name) ?? throw new InvalidOperationException("The specified SupportRequestStatus does not exist");
        }
    }
}

