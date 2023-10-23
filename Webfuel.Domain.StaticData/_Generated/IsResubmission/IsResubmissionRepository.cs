using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IIsResubmissionRepository
    {
        Task<QueryResult<IsResubmission>> QueryIsResubmission(Query query);
        Task<IsResubmission?> GetIsResubmission(Guid id);
        Task<IsResubmission> RequireIsResubmission(Guid id);
        Task<int> CountIsResubmission();
        Task<List<IsResubmission>> SelectIsResubmission();
        Task<List<IsResubmission>> SelectIsResubmissionWithPage(int skip, int take);
        Task<IsResubmission?> GetIsResubmissionByName(string name);
        Task<IsResubmission> RequireIsResubmissionByName(string name);
    }
    [Service(typeof(IIsResubmissionRepository))]
    internal partial class IsResubmissionRepository: IIsResubmissionRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public IsResubmissionRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<QueryResult<IsResubmission>> QueryIsResubmission(Query query)
        {
            return await _connection.ExecuteQuery<IsResubmission, IsResubmissionMetadata>(query);
        }
        public async Task<IsResubmission?> GetIsResubmission(Guid id)
        {
            var sql = @"SELECT * FROM [IsResubmission] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<IsResubmission, IsResubmissionMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<IsResubmission> RequireIsResubmission(Guid id)
        {
            return await GetIsResubmission(id) ?? throw new InvalidOperationException("The specified IsResubmission does not exist");
        }
        public async Task<int> CountIsResubmission()
        {
            var sql = @"SELECT COUNT(Id) FROM [IsResubmission]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<IsResubmission>> SelectIsResubmission()
        {
            var sql = @"SELECT * FROM [IsResubmission] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<IsResubmission, IsResubmissionMetadata>(sql);
        }
        public async Task<List<IsResubmission>> SelectIsResubmissionWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [IsResubmission] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<IsResubmission, IsResubmissionMetadata>(sql, parameters);
        }
        public async Task<IsResubmission?> GetIsResubmissionByName(string name)
        {
            var sql = @"SELECT * FROM [IsResubmission] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<IsResubmission, IsResubmissionMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<IsResubmission> RequireIsResubmissionByName(string name)
        {
            return await GetIsResubmissionByName(name) ?? throw new InvalidOperationException("The specified IsResubmission does not exist");
        }
    }
}

