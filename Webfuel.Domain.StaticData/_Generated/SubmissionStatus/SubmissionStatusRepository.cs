using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface ISubmissionStatusRepository
    {
        Task<QueryResult<SubmissionStatus>> QuerySubmissionStatus(Query query, bool selectItems = true, bool countTotal = true);
        Task<SubmissionStatus?> GetSubmissionStatus(Guid id);
        Task<SubmissionStatus> RequireSubmissionStatus(Guid id);
        Task<int> CountSubmissionStatus();
        Task<List<SubmissionStatus>> SelectSubmissionStatus();
        Task<List<SubmissionStatus>> SelectSubmissionStatusWithPage(int skip, int take);
        Task<SubmissionStatus?> GetSubmissionStatusByName(string name);
        Task<SubmissionStatus> RequireSubmissionStatusByName(string name);
    }
    [Service(typeof(ISubmissionStatusRepository))]
    internal partial class SubmissionStatusRepository: ISubmissionStatusRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public SubmissionStatusRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<QueryResult<SubmissionStatus>> QuerySubmissionStatus(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<SubmissionStatus, SubmissionStatusMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<SubmissionStatus?> GetSubmissionStatus(Guid id)
        {
            var sql = @"SELECT * FROM [SubmissionStatus] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<SubmissionStatus, SubmissionStatusMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<SubmissionStatus> RequireSubmissionStatus(Guid id)
        {
            return await GetSubmissionStatus(id) ?? throw new InvalidOperationException("The specified SubmissionStatus does not exist");
        }
        public async Task<int> CountSubmissionStatus()
        {
            var sql = @"SELECT COUNT(Id) FROM [SubmissionStatus]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<SubmissionStatus>> SelectSubmissionStatus()
        {
            var sql = @"SELECT * FROM [SubmissionStatus] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<SubmissionStatus, SubmissionStatusMetadata>(sql);
        }
        public async Task<List<SubmissionStatus>> SelectSubmissionStatusWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [SubmissionStatus] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<SubmissionStatus, SubmissionStatusMetadata>(sql, parameters);
        }
        public async Task<SubmissionStatus?> GetSubmissionStatusByName(string name)
        {
            var sql = @"SELECT * FROM [SubmissionStatus] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<SubmissionStatus, SubmissionStatusMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<SubmissionStatus> RequireSubmissionStatusByName(string name)
        {
            return await GetSubmissionStatusByName(name) ?? throw new InvalidOperationException("The specified SubmissionStatus does not exist");
        }
    }
}

