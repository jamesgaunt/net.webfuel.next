using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface ISubmissionStageRepository
    {
        Task<QueryResult<SubmissionStage>> QuerySubmissionStage(Query query, bool countTotal = true);
        Task<SubmissionStage?> GetSubmissionStage(Guid id);
        Task<SubmissionStage> RequireSubmissionStage(Guid id);
        Task<int> CountSubmissionStage();
        Task<List<SubmissionStage>> SelectSubmissionStage();
        Task<List<SubmissionStage>> SelectSubmissionStageWithPage(int skip, int take);
        Task<SubmissionStage?> GetSubmissionStageByName(string name);
        Task<SubmissionStage> RequireSubmissionStageByName(string name);
    }
    [Service(typeof(ISubmissionStageRepository))]
    internal partial class SubmissionStageRepository: ISubmissionStageRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public SubmissionStageRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<QueryResult<SubmissionStage>> QuerySubmissionStage(Query query, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<SubmissionStage, SubmissionStageMetadata>(query, countTotal);
        }
        public async Task<SubmissionStage?> GetSubmissionStage(Guid id)
        {
            var sql = @"SELECT * FROM [SubmissionStage] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<SubmissionStage, SubmissionStageMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<SubmissionStage> RequireSubmissionStage(Guid id)
        {
            return await GetSubmissionStage(id) ?? throw new InvalidOperationException("The specified SubmissionStage does not exist");
        }
        public async Task<int> CountSubmissionStage()
        {
            var sql = @"SELECT COUNT(Id) FROM [SubmissionStage]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<SubmissionStage>> SelectSubmissionStage()
        {
            var sql = @"SELECT * FROM [SubmissionStage] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<SubmissionStage, SubmissionStageMetadata>(sql);
        }
        public async Task<List<SubmissionStage>> SelectSubmissionStageWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [SubmissionStage] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<SubmissionStage, SubmissionStageMetadata>(sql, parameters);
        }
        public async Task<SubmissionStage?> GetSubmissionStageByName(string name)
        {
            var sql = @"SELECT * FROM [SubmissionStage] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<SubmissionStage, SubmissionStageMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<SubmissionStage> RequireSubmissionStageByName(string name)
        {
            return await GetSubmissionStageByName(name) ?? throw new InvalidOperationException("The specified SubmissionStage does not exist");
        }
    }
}

