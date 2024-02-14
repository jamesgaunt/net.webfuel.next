using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IWillStudyUseCTURepository
    {
        Task<QueryResult<WillStudyUseCTU>> QueryWillStudyUseCTU(Query query, bool selectItems = true, bool countTotal = true);
        Task<WillStudyUseCTU?> GetWillStudyUseCTU(Guid id);
        Task<WillStudyUseCTU> RequireWillStudyUseCTU(Guid id);
        Task<int> CountWillStudyUseCTU();
        Task<List<WillStudyUseCTU>> SelectWillStudyUseCTU();
        Task<List<WillStudyUseCTU>> SelectWillStudyUseCTUWithPage(int skip, int take);
        Task<WillStudyUseCTU?> GetWillStudyUseCTUByName(string name);
        Task<WillStudyUseCTU> RequireWillStudyUseCTUByName(string name);
    }
    [Service(typeof(IWillStudyUseCTURepository))]
    internal partial class WillStudyUseCTURepository: IWillStudyUseCTURepository
    {
        private readonly IRepositoryConnection _connection;
        
        public WillStudyUseCTURepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<QueryResult<WillStudyUseCTU>> QueryWillStudyUseCTU(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<WillStudyUseCTU, WillStudyUseCTUMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<WillStudyUseCTU?> GetWillStudyUseCTU(Guid id)
        {
            var sql = @"SELECT * FROM [WillStudyUseCTU] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<WillStudyUseCTU, WillStudyUseCTUMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<WillStudyUseCTU> RequireWillStudyUseCTU(Guid id)
        {
            return await GetWillStudyUseCTU(id) ?? throw new InvalidOperationException("The specified WillStudyUseCTU does not exist");
        }
        public async Task<int> CountWillStudyUseCTU()
        {
            var sql = @"SELECT COUNT(Id) FROM [WillStudyUseCTU]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<WillStudyUseCTU>> SelectWillStudyUseCTU()
        {
            var sql = @"SELECT * FROM [WillStudyUseCTU] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<WillStudyUseCTU, WillStudyUseCTUMetadata>(sql);
        }
        public async Task<List<WillStudyUseCTU>> SelectWillStudyUseCTUWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [WillStudyUseCTU] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<WillStudyUseCTU, WillStudyUseCTUMetadata>(sql, parameters);
        }
        public async Task<WillStudyUseCTU?> GetWillStudyUseCTUByName(string name)
        {
            var sql = @"SELECT * FROM [WillStudyUseCTU] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<WillStudyUseCTU, WillStudyUseCTUMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<WillStudyUseCTU> RequireWillStudyUseCTUByName(string name)
        {
            return await GetWillStudyUseCTUByName(name) ?? throw new InvalidOperationException("The specified WillStudyUseCTU does not exist");
        }
    }
}

