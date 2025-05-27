using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IFullSubmissionStatusRepository
    {
        Task<QueryResult<FullSubmissionStatus>> QueryFullSubmissionStatus(Query query, bool selectItems = true, bool countTotal = true);
        Task<FullSubmissionStatus?> GetFullSubmissionStatus(Guid id);
        Task<FullSubmissionStatus> RequireFullSubmissionStatus(Guid id);
        Task<int> CountFullSubmissionStatus();
        Task<List<FullSubmissionStatus>> SelectFullSubmissionStatus();
        Task<List<FullSubmissionStatus>> SelectFullSubmissionStatusWithPage(int skip, int take);
        Task<FullSubmissionStatus?> GetFullSubmissionStatusByName(string name);
        Task<FullSubmissionStatus> RequireFullSubmissionStatusByName(string name);
    }
    [Service(typeof(IFullSubmissionStatusRepository))]
    internal partial class FullSubmissionStatusRepository: IFullSubmissionStatusRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public FullSubmissionStatusRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<QueryResult<FullSubmissionStatus>> QueryFullSubmissionStatus(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<FullSubmissionStatus, FullSubmissionStatusMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<FullSubmissionStatus?> GetFullSubmissionStatus(Guid id)
        {
            var sql = @"SELECT * FROM [FullSubmissionStatus] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<FullSubmissionStatus, FullSubmissionStatusMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<FullSubmissionStatus> RequireFullSubmissionStatus(Guid id)
        {
            return await GetFullSubmissionStatus(id) ?? throw new InvalidOperationException("The specified FullSubmissionStatus does not exist");
        }
        public async Task<int> CountFullSubmissionStatus()
        {
            var sql = @"SELECT COUNT(Id) FROM [FullSubmissionStatus]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<FullSubmissionStatus>> SelectFullSubmissionStatus()
        {
            var sql = @"SELECT * FROM [FullSubmissionStatus] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<FullSubmissionStatus, FullSubmissionStatusMetadata>(sql);
        }
        public async Task<List<FullSubmissionStatus>> SelectFullSubmissionStatusWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [FullSubmissionStatus] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<FullSubmissionStatus, FullSubmissionStatusMetadata>(sql, parameters);
        }
        public async Task<FullSubmissionStatus?> GetFullSubmissionStatusByName(string name)
        {
            var sql = @"SELECT * FROM [FullSubmissionStatus] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<FullSubmissionStatus, FullSubmissionStatusMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<FullSubmissionStatus> RequireFullSubmissionStatusByName(string name)
        {
            return await GetFullSubmissionStatusByName(name) ?? throw new InvalidOperationException("The specified FullSubmissionStatus does not exist");
        }
    }
}

