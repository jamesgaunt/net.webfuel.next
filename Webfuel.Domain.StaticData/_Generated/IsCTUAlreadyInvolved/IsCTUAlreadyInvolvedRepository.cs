using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IIsCTUAlreadyInvolvedRepository
    {
        Task<QueryResult<IsCTUAlreadyInvolved>> QueryIsCTUAlreadyInvolved(Query query, bool selectItems = true, bool countTotal = true);
        Task<IsCTUAlreadyInvolved?> GetIsCTUAlreadyInvolved(Guid id);
        Task<IsCTUAlreadyInvolved> RequireIsCTUAlreadyInvolved(Guid id);
        Task<int> CountIsCTUAlreadyInvolved();
        Task<List<IsCTUAlreadyInvolved>> SelectIsCTUAlreadyInvolved();
        Task<List<IsCTUAlreadyInvolved>> SelectIsCTUAlreadyInvolvedWithPage(int skip, int take);
        Task<IsCTUAlreadyInvolved?> GetIsCTUAlreadyInvolvedByName(string name);
        Task<IsCTUAlreadyInvolved> RequireIsCTUAlreadyInvolvedByName(string name);
    }
    [Service(typeof(IIsCTUAlreadyInvolvedRepository))]
    internal partial class IsCTUAlreadyInvolvedRepository: IIsCTUAlreadyInvolvedRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public IsCTUAlreadyInvolvedRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<QueryResult<IsCTUAlreadyInvolved>> QueryIsCTUAlreadyInvolved(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<IsCTUAlreadyInvolved, IsCTUAlreadyInvolvedMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<IsCTUAlreadyInvolved?> GetIsCTUAlreadyInvolved(Guid id)
        {
            var sql = @"SELECT * FROM [IsCTUAlreadyInvolved] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<IsCTUAlreadyInvolved, IsCTUAlreadyInvolvedMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<IsCTUAlreadyInvolved> RequireIsCTUAlreadyInvolved(Guid id)
        {
            return await GetIsCTUAlreadyInvolved(id) ?? throw new InvalidOperationException("The specified IsCTUAlreadyInvolved does not exist");
        }
        public async Task<int> CountIsCTUAlreadyInvolved()
        {
            var sql = @"SELECT COUNT(Id) FROM [IsCTUAlreadyInvolved]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<IsCTUAlreadyInvolved>> SelectIsCTUAlreadyInvolved()
        {
            var sql = @"SELECT * FROM [IsCTUAlreadyInvolved] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<IsCTUAlreadyInvolved, IsCTUAlreadyInvolvedMetadata>(sql);
        }
        public async Task<List<IsCTUAlreadyInvolved>> SelectIsCTUAlreadyInvolvedWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [IsCTUAlreadyInvolved] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<IsCTUAlreadyInvolved, IsCTUAlreadyInvolvedMetadata>(sql, parameters);
        }
        public async Task<IsCTUAlreadyInvolved?> GetIsCTUAlreadyInvolvedByName(string name)
        {
            var sql = @"SELECT * FROM [IsCTUAlreadyInvolved] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<IsCTUAlreadyInvolved, IsCTUAlreadyInvolvedMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<IsCTUAlreadyInvolved> RequireIsCTUAlreadyInvolvedByName(string name)
        {
            return await GetIsCTUAlreadyInvolvedByName(name) ?? throw new InvalidOperationException("The specified IsCTUAlreadyInvolved does not exist");
        }
    }
}

