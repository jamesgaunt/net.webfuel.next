using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface ISuportRequestStatusRepository
    {
        Task<QueryResult<SuportRequestStatus>> QuerySuportRequestStatus(Query query);
        Task<SuportRequestStatus?> GetSuportRequestStatus(Guid id);
        Task<SuportRequestStatus> RequireSuportRequestStatus(Guid id);
        Task<int> CountSuportRequestStatus();
        Task<List<SuportRequestStatus>> SelectSuportRequestStatus();
        Task<List<SuportRequestStatus>> SelectSuportRequestStatusWithPage(int skip, int take);
        Task<SuportRequestStatus?> GetSuportRequestStatusByName(string name);
        Task<SuportRequestStatus> RequireSuportRequestStatusByName(string name);
    }
    [Service(typeof(ISuportRequestStatusRepository))]
    internal partial class SuportRequestStatusRepository: ISuportRequestStatusRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public SuportRequestStatusRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<QueryResult<SuportRequestStatus>> QuerySuportRequestStatus(Query query)
        {
            return await _connection.ExecuteQuery<SuportRequestStatus, SuportRequestStatusMetadata>(query);
        }
        public async Task<SuportRequestStatus?> GetSuportRequestStatus(Guid id)
        {
            var sql = @"SELECT * FROM [SuportRequestStatus] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<SuportRequestStatus, SuportRequestStatusMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<SuportRequestStatus> RequireSuportRequestStatus(Guid id)
        {
            return await GetSuportRequestStatus(id) ?? throw new InvalidOperationException("The specified SuportRequestStatus does not exist");
        }
        public async Task<int> CountSuportRequestStatus()
        {
            var sql = @"SELECT COUNT(Id) FROM [SuportRequestStatus]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<SuportRequestStatus>> SelectSuportRequestStatus()
        {
            var sql = @"SELECT * FROM [SuportRequestStatus] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<SuportRequestStatus, SuportRequestStatusMetadata>(sql);
        }
        public async Task<List<SuportRequestStatus>> SelectSuportRequestStatusWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [SuportRequestStatus] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<SuportRequestStatus, SuportRequestStatusMetadata>(sql, parameters);
        }
        public async Task<SuportRequestStatus?> GetSuportRequestStatusByName(string name)
        {
            var sql = @"SELECT * FROM [SuportRequestStatus] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<SuportRequestStatus, SuportRequestStatusMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<SuportRequestStatus> RequireSuportRequestStatusByName(string name)
        {
            return await GetSuportRequestStatusByName(name) ?? throw new InvalidOperationException("The specified SuportRequestStatus does not exist");
        }
    }
}

