using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IFullOutcomeRepository
    {
        Task<QueryResult<FullOutcome>> QueryFullOutcome(Query query, bool selectItems = true, bool countTotal = true);
        Task<FullOutcome?> GetFullOutcome(Guid id);
        Task<FullOutcome> RequireFullOutcome(Guid id);
        Task<int> CountFullOutcome();
        Task<List<FullOutcome>> SelectFullOutcome();
        Task<List<FullOutcome>> SelectFullOutcomeWithPage(int skip, int take);
        Task<FullOutcome?> GetFullOutcomeByName(string name);
        Task<FullOutcome> RequireFullOutcomeByName(string name);
    }
    [Service(typeof(IFullOutcomeRepository))]
    internal partial class FullOutcomeRepository: IFullOutcomeRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public FullOutcomeRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<QueryResult<FullOutcome>> QueryFullOutcome(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<FullOutcome, FullOutcomeMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<FullOutcome?> GetFullOutcome(Guid id)
        {
            var sql = @"SELECT * FROM [FullOutcome] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<FullOutcome, FullOutcomeMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<FullOutcome> RequireFullOutcome(Guid id)
        {
            return await GetFullOutcome(id) ?? throw new InvalidOperationException("The specified FullOutcome does not exist");
        }
        public async Task<int> CountFullOutcome()
        {
            var sql = @"SELECT COUNT(Id) FROM [FullOutcome]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<FullOutcome>> SelectFullOutcome()
        {
            var sql = @"SELECT * FROM [FullOutcome] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<FullOutcome, FullOutcomeMetadata>(sql);
        }
        public async Task<List<FullOutcome>> SelectFullOutcomeWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [FullOutcome] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<FullOutcome, FullOutcomeMetadata>(sql, parameters);
        }
        public async Task<FullOutcome?> GetFullOutcomeByName(string name)
        {
            var sql = @"SELECT * FROM [FullOutcome] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<FullOutcome, FullOutcomeMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<FullOutcome> RequireFullOutcomeByName(string name)
        {
            return await GetFullOutcomeByName(name) ?? throw new InvalidOperationException("The specified FullOutcome does not exist");
        }
    }
}

