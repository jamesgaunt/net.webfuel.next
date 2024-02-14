using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IIsPaidRSSAdviserCoapplicantRepository
    {
        Task<QueryResult<IsPaidRSSAdviserCoapplicant>> QueryIsPaidRSSAdviserCoapplicant(Query query, bool selectItems = true, bool countTotal = true);
        Task<IsPaidRSSAdviserCoapplicant?> GetIsPaidRSSAdviserCoapplicant(Guid id);
        Task<IsPaidRSSAdviserCoapplicant> RequireIsPaidRSSAdviserCoapplicant(Guid id);
        Task<int> CountIsPaidRSSAdviserCoapplicant();
        Task<List<IsPaidRSSAdviserCoapplicant>> SelectIsPaidRSSAdviserCoapplicant();
        Task<List<IsPaidRSSAdviserCoapplicant>> SelectIsPaidRSSAdviserCoapplicantWithPage(int skip, int take);
        Task<IsPaidRSSAdviserCoapplicant?> GetIsPaidRSSAdviserCoapplicantByName(string name);
        Task<IsPaidRSSAdviserCoapplicant> RequireIsPaidRSSAdviserCoapplicantByName(string name);
    }
    [Service(typeof(IIsPaidRSSAdviserCoapplicantRepository))]
    internal partial class IsPaidRSSAdviserCoapplicantRepository: IIsPaidRSSAdviserCoapplicantRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public IsPaidRSSAdviserCoapplicantRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<QueryResult<IsPaidRSSAdviserCoapplicant>> QueryIsPaidRSSAdviserCoapplicant(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<IsPaidRSSAdviserCoapplicant, IsPaidRSSAdviserCoapplicantMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<IsPaidRSSAdviserCoapplicant?> GetIsPaidRSSAdviserCoapplicant(Guid id)
        {
            var sql = @"SELECT * FROM [IsPaidRSSAdviserCoapplicant] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<IsPaidRSSAdviserCoapplicant, IsPaidRSSAdviserCoapplicantMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<IsPaidRSSAdviserCoapplicant> RequireIsPaidRSSAdviserCoapplicant(Guid id)
        {
            return await GetIsPaidRSSAdviserCoapplicant(id) ?? throw new InvalidOperationException("The specified IsPaidRSSAdviserCoapplicant does not exist");
        }
        public async Task<int> CountIsPaidRSSAdviserCoapplicant()
        {
            var sql = @"SELECT COUNT(Id) FROM [IsPaidRSSAdviserCoapplicant]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<IsPaidRSSAdviserCoapplicant>> SelectIsPaidRSSAdviserCoapplicant()
        {
            var sql = @"SELECT * FROM [IsPaidRSSAdviserCoapplicant] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<IsPaidRSSAdviserCoapplicant, IsPaidRSSAdviserCoapplicantMetadata>(sql);
        }
        public async Task<List<IsPaidRSSAdviserCoapplicant>> SelectIsPaidRSSAdviserCoapplicantWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [IsPaidRSSAdviserCoapplicant] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<IsPaidRSSAdviserCoapplicant, IsPaidRSSAdviserCoapplicantMetadata>(sql, parameters);
        }
        public async Task<IsPaidRSSAdviserCoapplicant?> GetIsPaidRSSAdviserCoapplicantByName(string name)
        {
            var sql = @"SELECT * FROM [IsPaidRSSAdviserCoapplicant] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<IsPaidRSSAdviserCoapplicant, IsPaidRSSAdviserCoapplicantMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<IsPaidRSSAdviserCoapplicant> RequireIsPaidRSSAdviserCoapplicantByName(string name)
        {
            return await GetIsPaidRSSAdviserCoapplicantByName(name) ?? throw new InvalidOperationException("The specified IsPaidRSSAdviserCoapplicant does not exist");
        }
    }
}

