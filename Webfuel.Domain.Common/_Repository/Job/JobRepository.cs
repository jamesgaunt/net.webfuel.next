using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.Common
{
    internal partial interface IJobRepository
    {
        Task<Job> InsertJobAsync(Job entity);
        Task<Job> UpdateJobAsync(Job entity);
        Task<Job> UpdateJobAsync(Job entity, IEnumerable<string> properties);
        Task<Job> UpdateJobAsync(Job updated, Job original);
        Task<Job> UpdateJobAsync(Job updated, Job original, IEnumerable<string> properties);
        Task DeleteJobAsync(Guid key);
        Task<QueryResult<Job>> QueryJobAsync(Query query);
        Task<Job?> GetJobAsync(Guid id);
        Task<Job> RequireJobAsync(Guid id);
        Task<int> CountJobAsync();
        Task<List<Job>> SelectJobAsync();
        Task<List<Job>> SelectJobWithPageAsync(int skip, int take);
    }
    internal partial class JobRepository: IJobRepository
    {
        private readonly IRepositoryService RepositoryService;
        private readonly IRepositoryQueryService RepositoryQueryService;
        public JobRepository(IRepositoryService repositoryService, IRepositoryQueryService repositoryQueryService)
        {
            RepositoryService = repositoryService;
            RepositoryQueryService = repositoryQueryService;
        }
        public async Task<Job> InsertJobAsync(Job entity)
        {
            return await RepositoryService.ExecuteInsertAsync(entity);
        }
        public async Task<Job> UpdateJobAsync(Job entity)
        {
            return await RepositoryService.ExecuteUpdateAsync(entity);
        }
        public async Task<Job> UpdateJobAsync(Job entity, IEnumerable<string> properties)
        {
            return await RepositoryService.ExecuteUpdateAsync(entity, properties);
        }
        public async Task<Job> UpdateJobAsync(Job updated, Job original)
        {
            if(updated.Id != original.Id) throw new InvalidOperationException("UpdateJobAsync: Entity keys do not match.");
            var _properties = new List<string>();
            if(updated.Name != original.Name) _properties.Add("Name");
            if(_properties.Count == 0) return updated;
            return await RepositoryService.ExecuteUpdateAsync(updated, _properties);
        }
        public async Task<Job> UpdateJobAsync(Job updated, Job original, IEnumerable<string> properties)
        {
            if(updated.Id != original.Id) throw new InvalidOperationException("UpdateJobAsync: Entity keys do not match.");
            var _properties = new List<string>();
            if(properties.Contains("Name") && updated.Name != original.Name) _properties.Add("Name");
            if(_properties.Count == 0) return updated;
            return await RepositoryService.ExecuteUpdateAsync(updated, _properties);
        }
        public async Task DeleteJobAsync(Guid key)
        {
            await RepositoryService.ExecuteDeleteAsync<Job>(key);
        }
        public async Task<QueryResult<Job>> QueryJobAsync(Query query)
        {
            return await RepositoryQueryService.ExecuteQueryAsync(query, new JobRepositoryAccessor());
        }
        public async Task<Job?> GetJobAsync(Guid id)
        {
            var sql = @"SELECT * FROM [Job] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await RepositoryService.ExecuteReaderAsync<Job>(sql, parameters)).SingleOrDefault();
        }
        public async Task<Job> RequireJobAsync(Guid id)
        {
            return await GetJobAsync(id) ?? throw new InvalidOperationException("The specified Job does not exist");
        }
        public async Task<int> CountJobAsync()
        {
            var sql = @"SELECT COUNT(Id) FROM [Job]";
            return (int)((await RepositoryService.ExecuteScalarAsync(sql))!);
        }
        public async Task<List<Job>> SelectJobAsync()
        {
            var sql = @"SELECT * FROM [Job] ORDER BY Id ASC";
            return await RepositoryService.ExecuteReaderAsync<Job>(sql);
        }
        public async Task<List<Job>> SelectJobWithPageAsync(int skip, int take)
        {
            var sql = @"SELECT * FROM [Job] ORDER BY Id ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await RepositoryService.ExecuteReaderAsync<Job>(sql, parameters);
        }
    }
}

