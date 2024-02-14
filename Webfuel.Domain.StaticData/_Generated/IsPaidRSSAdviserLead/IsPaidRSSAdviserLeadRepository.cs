using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IIsPaidRSSAdviserLeadRepository
    {
        Task<QueryResult<IsPaidRSSAdviserLead>> QueryIsPaidRSSAdviserLead(Query query, bool selectItems = true, bool countTotal = true);
        Task<IsPaidRSSAdviserLead?> GetIsPaidRSSAdviserLead(Guid id);
        Task<IsPaidRSSAdviserLead> RequireIsPaidRSSAdviserLead(Guid id);
        Task<int> CountIsPaidRSSAdviserLead();
        Task<List<IsPaidRSSAdviserLead>> SelectIsPaidRSSAdviserLead();
        Task<List<IsPaidRSSAdviserLead>> SelectIsPaidRSSAdviserLeadWithPage(int skip, int take);
        Task<IsPaidRSSAdviserLead?> GetIsPaidRSSAdviserLeadByName(string name);
        Task<IsPaidRSSAdviserLead> RequireIsPaidRSSAdviserLeadByName(string name);
    }
    [Service(typeof(IIsPaidRSSAdviserLeadRepository))]
    internal partial class IsPaidRSSAdviserLeadRepository: IIsPaidRSSAdviserLeadRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public IsPaidRSSAdviserLeadRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<QueryResult<IsPaidRSSAdviserLead>> QueryIsPaidRSSAdviserLead(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<IsPaidRSSAdviserLead, IsPaidRSSAdviserLeadMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<IsPaidRSSAdviserLead?> GetIsPaidRSSAdviserLead(Guid id)
        {
            var sql = @"SELECT * FROM [IsPaidRSSAdviserLead] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<IsPaidRSSAdviserLead, IsPaidRSSAdviserLeadMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<IsPaidRSSAdviserLead> RequireIsPaidRSSAdviserLead(Guid id)
        {
            return await GetIsPaidRSSAdviserLead(id) ?? throw new InvalidOperationException("The specified IsPaidRSSAdviserLead does not exist");
        }
        public async Task<int> CountIsPaidRSSAdviserLead()
        {
            var sql = @"SELECT COUNT(Id) FROM [IsPaidRSSAdviserLead]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<IsPaidRSSAdviserLead>> SelectIsPaidRSSAdviserLead()
        {
            var sql = @"SELECT * FROM [IsPaidRSSAdviserLead] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<IsPaidRSSAdviserLead, IsPaidRSSAdviserLeadMetadata>(sql);
        }
        public async Task<List<IsPaidRSSAdviserLead>> SelectIsPaidRSSAdviserLeadWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [IsPaidRSSAdviserLead] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<IsPaidRSSAdviserLead, IsPaidRSSAdviserLeadMetadata>(sql, parameters);
        }
        public async Task<IsPaidRSSAdviserLead?> GetIsPaidRSSAdviserLeadByName(string name)
        {
            var sql = @"SELECT * FROM [IsPaidRSSAdviserLead] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<IsPaidRSSAdviserLead, IsPaidRSSAdviserLeadMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<IsPaidRSSAdviserLead> RequireIsPaidRSSAdviserLeadByName(string name)
        {
            return await GetIsPaidRSSAdviserLeadByName(name) ?? throw new InvalidOperationException("The specified IsPaidRSSAdviserLead does not exist");
        }
    }
}

