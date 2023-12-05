using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IIsLeadApplicantNHSRepository
    {
        Task<QueryResult<IsLeadApplicantNHS>> QueryIsLeadApplicantNHS(Query query, bool selectItems = true, bool countTotal = true);
        Task<IsLeadApplicantNHS?> GetIsLeadApplicantNHS(Guid id);
        Task<IsLeadApplicantNHS> RequireIsLeadApplicantNHS(Guid id);
        Task<int> CountIsLeadApplicantNHS();
        Task<List<IsLeadApplicantNHS>> SelectIsLeadApplicantNHS();
        Task<List<IsLeadApplicantNHS>> SelectIsLeadApplicantNHSWithPage(int skip, int take);
        Task<IsLeadApplicantNHS?> GetIsLeadApplicantNHSByName(string name);
        Task<IsLeadApplicantNHS> RequireIsLeadApplicantNHSByName(string name);
    }
    [Service(typeof(IIsLeadApplicantNHSRepository))]
    internal partial class IsLeadApplicantNHSRepository: IIsLeadApplicantNHSRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public IsLeadApplicantNHSRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<QueryResult<IsLeadApplicantNHS>> QueryIsLeadApplicantNHS(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<IsLeadApplicantNHS, IsLeadApplicantNHSMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<IsLeadApplicantNHS?> GetIsLeadApplicantNHS(Guid id)
        {
            var sql = @"SELECT * FROM [IsLeadApplicantNHS] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<IsLeadApplicantNHS, IsLeadApplicantNHSMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<IsLeadApplicantNHS> RequireIsLeadApplicantNHS(Guid id)
        {
            return await GetIsLeadApplicantNHS(id) ?? throw new InvalidOperationException("The specified IsLeadApplicantNHS does not exist");
        }
        public async Task<int> CountIsLeadApplicantNHS()
        {
            var sql = @"SELECT COUNT(Id) FROM [IsLeadApplicantNHS]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<IsLeadApplicantNHS>> SelectIsLeadApplicantNHS()
        {
            var sql = @"SELECT * FROM [IsLeadApplicantNHS] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<IsLeadApplicantNHS, IsLeadApplicantNHSMetadata>(sql);
        }
        public async Task<List<IsLeadApplicantNHS>> SelectIsLeadApplicantNHSWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [IsLeadApplicantNHS] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<IsLeadApplicantNHS, IsLeadApplicantNHSMetadata>(sql, parameters);
        }
        public async Task<IsLeadApplicantNHS?> GetIsLeadApplicantNHSByName(string name)
        {
            var sql = @"SELECT * FROM [IsLeadApplicantNHS] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<IsLeadApplicantNHS, IsLeadApplicantNHSMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<IsLeadApplicantNHS> RequireIsLeadApplicantNHSByName(string name)
        {
            return await GetIsLeadApplicantNHSByName(name) ?? throw new InvalidOperationException("The specified IsLeadApplicantNHS does not exist");
        }
    }
}

