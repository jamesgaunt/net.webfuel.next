using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IOutlineSubmissionStatusRepository
    {
        Task<QueryResult<OutlineSubmissionStatus>> QueryOutlineSubmissionStatus(Query query, bool selectItems = true, bool countTotal = true);
        Task<OutlineSubmissionStatus?> GetOutlineSubmissionStatus(Guid id);
        Task<OutlineSubmissionStatus> RequireOutlineSubmissionStatus(Guid id);
        Task<int> CountOutlineSubmissionStatus();
        Task<List<OutlineSubmissionStatus>> SelectOutlineSubmissionStatus();
        Task<List<OutlineSubmissionStatus>> SelectOutlineSubmissionStatusWithPage(int skip, int take);
        Task<OutlineSubmissionStatus?> GetOutlineSubmissionStatusByName(string name);
        Task<OutlineSubmissionStatus> RequireOutlineSubmissionStatusByName(string name);
    }
    [Service(typeof(IOutlineSubmissionStatusRepository))]
    internal partial class OutlineSubmissionStatusRepository: IOutlineSubmissionStatusRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public OutlineSubmissionStatusRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<QueryResult<OutlineSubmissionStatus>> QueryOutlineSubmissionStatus(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<OutlineSubmissionStatus, OutlineSubmissionStatusMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<OutlineSubmissionStatus?> GetOutlineSubmissionStatus(Guid id)
        {
            var sql = @"SELECT * FROM [OutlineSubmissionStatus] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<OutlineSubmissionStatus, OutlineSubmissionStatusMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<OutlineSubmissionStatus> RequireOutlineSubmissionStatus(Guid id)
        {
            return await GetOutlineSubmissionStatus(id) ?? throw new InvalidOperationException("The specified OutlineSubmissionStatus does not exist");
        }
        public async Task<int> CountOutlineSubmissionStatus()
        {
            var sql = @"SELECT COUNT(Id) FROM [OutlineSubmissionStatus]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<OutlineSubmissionStatus>> SelectOutlineSubmissionStatus()
        {
            var sql = @"SELECT * FROM [OutlineSubmissionStatus] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<OutlineSubmissionStatus, OutlineSubmissionStatusMetadata>(sql);
        }
        public async Task<List<OutlineSubmissionStatus>> SelectOutlineSubmissionStatusWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [OutlineSubmissionStatus] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<OutlineSubmissionStatus, OutlineSubmissionStatusMetadata>(sql, parameters);
        }
        public async Task<OutlineSubmissionStatus?> GetOutlineSubmissionStatusByName(string name)
        {
            var sql = @"SELECT * FROM [OutlineSubmissionStatus] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<OutlineSubmissionStatus, OutlineSubmissionStatusMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<OutlineSubmissionStatus> RequireOutlineSubmissionStatusByName(string name)
        {
            return await GetOutlineSubmissionStatusByName(name) ?? throw new InvalidOperationException("The specified OutlineSubmissionStatus does not exist");
        }
    }
}

