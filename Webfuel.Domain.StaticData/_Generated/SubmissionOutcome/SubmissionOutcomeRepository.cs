using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface ISubmissionOutcomeRepository
    {
        Task<QueryResult<SubmissionOutcome>> QuerySubmissionOutcome(Query query, bool countTotal = true);
        Task<SubmissionOutcome?> GetSubmissionOutcome(Guid id);
        Task<SubmissionOutcome> RequireSubmissionOutcome(Guid id);
        Task<int> CountSubmissionOutcome();
        Task<List<SubmissionOutcome>> SelectSubmissionOutcome();
        Task<List<SubmissionOutcome>> SelectSubmissionOutcomeWithPage(int skip, int take);
        Task<SubmissionOutcome?> GetSubmissionOutcomeByName(string name);
        Task<SubmissionOutcome> RequireSubmissionOutcomeByName(string name);
    }
    [Service(typeof(ISubmissionOutcomeRepository))]
    internal partial class SubmissionOutcomeRepository: ISubmissionOutcomeRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public SubmissionOutcomeRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<QueryResult<SubmissionOutcome>> QuerySubmissionOutcome(Query query, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<SubmissionOutcome, SubmissionOutcomeMetadata>(query, countTotal);
        }
        public async Task<SubmissionOutcome?> GetSubmissionOutcome(Guid id)
        {
            var sql = @"SELECT * FROM [SubmissionOutcome] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<SubmissionOutcome, SubmissionOutcomeMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<SubmissionOutcome> RequireSubmissionOutcome(Guid id)
        {
            return await GetSubmissionOutcome(id) ?? throw new InvalidOperationException("The specified SubmissionOutcome does not exist");
        }
        public async Task<int> CountSubmissionOutcome()
        {
            var sql = @"SELECT COUNT(Id) FROM [SubmissionOutcome]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<SubmissionOutcome>> SelectSubmissionOutcome()
        {
            var sql = @"SELECT * FROM [SubmissionOutcome] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<SubmissionOutcome, SubmissionOutcomeMetadata>(sql);
        }
        public async Task<List<SubmissionOutcome>> SelectSubmissionOutcomeWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [SubmissionOutcome] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<SubmissionOutcome, SubmissionOutcomeMetadata>(sql, parameters);
        }
        public async Task<SubmissionOutcome?> GetSubmissionOutcomeByName(string name)
        {
            var sql = @"SELECT * FROM [SubmissionOutcome] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<SubmissionOutcome, SubmissionOutcomeMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<SubmissionOutcome> RequireSubmissionOutcomeByName(string name)
        {
            return await GetSubmissionOutcomeByName(name) ?? throw new InvalidOperationException("The specified SubmissionOutcome does not exist");
        }
    }
}

