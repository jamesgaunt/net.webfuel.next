using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IIsFellowshipRepository
    {
        Task<QueryResult<IsFellowship>> QueryIsFellowship(Query query, bool countTotal = true);
        Task<IsFellowship?> GetIsFellowship(Guid id);
        Task<IsFellowship> RequireIsFellowship(Guid id);
        Task<int> CountIsFellowship();
        Task<List<IsFellowship>> SelectIsFellowship();
        Task<List<IsFellowship>> SelectIsFellowshipWithPage(int skip, int take);
        Task<IsFellowship?> GetIsFellowshipByName(string name);
        Task<IsFellowship> RequireIsFellowshipByName(string name);
    }
    [Service(typeof(IIsFellowshipRepository))]
    internal partial class IsFellowshipRepository: IIsFellowshipRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public IsFellowshipRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<QueryResult<IsFellowship>> QueryIsFellowship(Query query, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<IsFellowship, IsFellowshipMetadata>(query, countTotal);
        }
        public async Task<IsFellowship?> GetIsFellowship(Guid id)
        {
            var sql = @"SELECT * FROM [IsFellowship] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<IsFellowship, IsFellowshipMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<IsFellowship> RequireIsFellowship(Guid id)
        {
            return await GetIsFellowship(id) ?? throw new InvalidOperationException("The specified IsFellowship does not exist");
        }
        public async Task<int> CountIsFellowship()
        {
            var sql = @"SELECT COUNT(Id) FROM [IsFellowship]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<IsFellowship>> SelectIsFellowship()
        {
            var sql = @"SELECT * FROM [IsFellowship] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<IsFellowship, IsFellowshipMetadata>(sql);
        }
        public async Task<List<IsFellowship>> SelectIsFellowshipWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [IsFellowship] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<IsFellowship, IsFellowshipMetadata>(sql, parameters);
        }
        public async Task<IsFellowship?> GetIsFellowshipByName(string name)
        {
            var sql = @"SELECT * FROM [IsFellowship] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<IsFellowship, IsFellowshipMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<IsFellowship> RequireIsFellowshipByName(string name)
        {
            return await GetIsFellowshipByName(name) ?? throw new InvalidOperationException("The specified IsFellowship does not exist");
        }
    }
}

