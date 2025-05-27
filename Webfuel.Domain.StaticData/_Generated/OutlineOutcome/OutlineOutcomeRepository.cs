using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IOutlineOutcomeRepository
    {
        Task<QueryResult<OutlineOutcome>> QueryOutlineOutcome(Query query, bool selectItems = true, bool countTotal = true);
        Task<OutlineOutcome?> GetOutlineOutcome(Guid id);
        Task<OutlineOutcome> RequireOutlineOutcome(Guid id);
        Task<int> CountOutlineOutcome();
        Task<List<OutlineOutcome>> SelectOutlineOutcome();
        Task<List<OutlineOutcome>> SelectOutlineOutcomeWithPage(int skip, int take);
        Task<OutlineOutcome?> GetOutlineOutcomeByName(string name);
        Task<OutlineOutcome> RequireOutlineOutcomeByName(string name);
    }
    [Service(typeof(IOutlineOutcomeRepository))]
    internal partial class OutlineOutcomeRepository: IOutlineOutcomeRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public OutlineOutcomeRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<QueryResult<OutlineOutcome>> QueryOutlineOutcome(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<OutlineOutcome, OutlineOutcomeMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<OutlineOutcome?> GetOutlineOutcome(Guid id)
        {
            var sql = @"SELECT * FROM [OutlineOutcome] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<OutlineOutcome, OutlineOutcomeMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<OutlineOutcome> RequireOutlineOutcome(Guid id)
        {
            return await GetOutlineOutcome(id) ?? throw new InvalidOperationException("The specified OutlineOutcome does not exist");
        }
        public async Task<int> CountOutlineOutcome()
        {
            var sql = @"SELECT COUNT(Id) FROM [OutlineOutcome]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<OutlineOutcome>> SelectOutlineOutcome()
        {
            var sql = @"SELECT * FROM [OutlineOutcome] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<OutlineOutcome, OutlineOutcomeMetadata>(sql);
        }
        public async Task<List<OutlineOutcome>> SelectOutlineOutcomeWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [OutlineOutcome] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<OutlineOutcome, OutlineOutcomeMetadata>(sql, parameters);
        }
        public async Task<OutlineOutcome?> GetOutlineOutcomeByName(string name)
        {
            var sql = @"SELECT * FROM [OutlineOutcome] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<OutlineOutcome, OutlineOutcomeMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<OutlineOutcome> RequireOutlineOutcomeByName(string name)
        {
            return await GetOutlineOutcomeByName(name) ?? throw new InvalidOperationException("The specified OutlineOutcome does not exist");
        }
    }
}

