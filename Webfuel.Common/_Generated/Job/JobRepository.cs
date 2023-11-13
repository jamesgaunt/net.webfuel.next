using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Common
{
    internal partial interface IJobRepository
    {
        Task<Job> InsertJob(Job entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<Job> UpdateJob(Job entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<Job> UpdateJob(Job updated, Job original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteJob(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<Job>> QueryJob(Query query);
        Task<Job?> GetJob(Guid id);
        Task<Job> RequireJob(Guid id);
        Task<int> CountJob();
        Task<List<Job>> SelectJob();
        Task<List<Job>> SelectJobWithPage(int skip, int take);
    }
    [Service(typeof(IJobRepository))]
    internal partial class JobRepository: IJobRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public JobRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<Job> InsertJob(Job entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            JobMetadata.Validate(entity);
            var sql = JobMetadata.InsertSQL();
            var parameters = JobMetadata.ExtractParameters(entity, JobMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<Job> UpdateJob(Job entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            JobMetadata.Validate(entity);
            var sql = JobMetadata.UpdateSQL();
            var parameters = JobMetadata.ExtractParameters(entity, JobMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<Job> UpdateJob(Job updated, Job original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateJob(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteJob(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = JobMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<Job>> QueryJob(Query query)
        {
            return await _connection.ExecuteQuery<Job, JobMetadata>(query);
        }
        public async Task<Job?> GetJob(Guid id)
        {
            var sql = @"SELECT * FROM [Job] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<Job, JobMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<Job> RequireJob(Guid id)
        {
            return await GetJob(id) ?? throw new InvalidOperationException("The specified Job does not exist");
        }
        public async Task<int> CountJob()
        {
            var sql = @"SELECT COUNT(Id) FROM [Job]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<Job>> SelectJob()
        {
            var sql = @"SELECT * FROM [Job] ORDER BY Id ASC";
            return await _connection.ExecuteReader<Job, JobMetadata>(sql);
        }
        public async Task<List<Job>> SelectJobWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [Job] ORDER BY Id ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<Job, JobMetadata>(sql, parameters);
        }
    }
}

