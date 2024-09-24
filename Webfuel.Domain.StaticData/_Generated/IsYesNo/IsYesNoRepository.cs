using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IIsYesNoRepository
    {
        Task<QueryResult<IsYesNo>> QueryIsYesNo(Query query, bool selectItems = true, bool countTotal = true);
        Task<IsYesNo?> GetIsYesNo(Guid id);
        Task<IsYesNo> RequireIsYesNo(Guid id);
        Task<int> CountIsYesNo();
        Task<List<IsYesNo>> SelectIsYesNo();
        Task<List<IsYesNo>> SelectIsYesNoWithPage(int skip, int take);
        Task<IsYesNo?> GetIsYesNoByName(string name);
        Task<IsYesNo> RequireIsYesNoByName(string name);
    }
    [Service(typeof(IIsYesNoRepository))]
    internal partial class IsYesNoRepository: IIsYesNoRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public IsYesNoRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<QueryResult<IsYesNo>> QueryIsYesNo(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<IsYesNo, IsYesNoMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<IsYesNo?> GetIsYesNo(Guid id)
        {
            var sql = @"SELECT * FROM [IsYesNo] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<IsYesNo, IsYesNoMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<IsYesNo> RequireIsYesNo(Guid id)
        {
            return await GetIsYesNo(id) ?? throw new InvalidOperationException("The specified IsYesNo does not exist");
        }
        public async Task<int> CountIsYesNo()
        {
            var sql = @"SELECT COUNT(Id) FROM [IsYesNo]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<IsYesNo>> SelectIsYesNo()
        {
            var sql = @"SELECT * FROM [IsYesNo] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<IsYesNo, IsYesNoMetadata>(sql);
        }
        public async Task<List<IsYesNo>> SelectIsYesNoWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [IsYesNo] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<IsYesNo, IsYesNoMetadata>(sql, parameters);
        }
        public async Task<IsYesNo?> GetIsYesNoByName(string name)
        {
            var sql = @"SELECT * FROM [IsYesNo] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<IsYesNo, IsYesNoMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<IsYesNo> RequireIsYesNoByName(string name)
        {
            return await GetIsYesNoByName(name) ?? throw new InvalidOperationException("The specified IsYesNo does not exist");
        }
    }
}

