using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IIsPrePostAwardRepository
    {
        Task<QueryResult<IsPrePostAward>> QueryIsPrePostAward(Query query, bool selectItems = true, bool countTotal = true);
        Task<IsPrePostAward?> GetIsPrePostAward(Guid id);
        Task<IsPrePostAward> RequireIsPrePostAward(Guid id);
        Task<int> CountIsPrePostAward();
        Task<List<IsPrePostAward>> SelectIsPrePostAward();
        Task<List<IsPrePostAward>> SelectIsPrePostAwardWithPage(int skip, int take);
        Task<IsPrePostAward?> GetIsPrePostAwardByName(string name);
        Task<IsPrePostAward> RequireIsPrePostAwardByName(string name);
    }
    [Service(typeof(IIsPrePostAwardRepository))]
    internal partial class IsPrePostAwardRepository: IIsPrePostAwardRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public IsPrePostAwardRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<QueryResult<IsPrePostAward>> QueryIsPrePostAward(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<IsPrePostAward, IsPrePostAwardMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<IsPrePostAward?> GetIsPrePostAward(Guid id)
        {
            var sql = @"SELECT * FROM [IsPrePostAward] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<IsPrePostAward, IsPrePostAwardMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<IsPrePostAward> RequireIsPrePostAward(Guid id)
        {
            return await GetIsPrePostAward(id) ?? throw new InvalidOperationException("The specified IsPrePostAward does not exist");
        }
        public async Task<int> CountIsPrePostAward()
        {
            var sql = @"SELECT COUNT(Id) FROM [IsPrePostAward]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<IsPrePostAward>> SelectIsPrePostAward()
        {
            var sql = @"SELECT * FROM [IsPrePostAward] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<IsPrePostAward, IsPrePostAwardMetadata>(sql);
        }
        public async Task<List<IsPrePostAward>> SelectIsPrePostAwardWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [IsPrePostAward] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<IsPrePostAward, IsPrePostAwardMetadata>(sql, parameters);
        }
        public async Task<IsPrePostAward?> GetIsPrePostAwardByName(string name)
        {
            var sql = @"SELECT * FROM [IsPrePostAward] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<IsPrePostAward, IsPrePostAwardMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<IsPrePostAward> RequireIsPrePostAwardByName(string name)
        {
            return await GetIsPrePostAwardByName(name) ?? throw new InvalidOperationException("The specified IsPrePostAward does not exist");
        }
    }
}

