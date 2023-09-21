using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.Common
{
    internal partial interface IJobRepository
    {
        Task<Job> InsertJob(Job entity);
        Task<Job> UpdateJob(Job entity);
        Task<Job> UpdateJob(Job entity, IEnumerable<string> properties);
        Task<Job> UpdateJob(Job updated, Job original);
        Task<Job> UpdateJob(Job updated, Job original, IEnumerable<string> properties);
        Task DeleteJob(Guid key);
        Task<QueryResult<Job>> QueryJob(Query query);
        Task<Job?> GetJob(Guid id);
        Task<Job> RequireJob(Guid id);
        Task<int> CountJob();
        Task<List<Job>> SelectJob();
        Task<List<Job>> SelectJobWithPage(int skip, int take);
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
        public async Task<Job> InsertJob(Job entity)
        {
            return await RepositoryService.ExecuteInsert(entity);
        }
        public async Task<Job> UpdateJob(Job entity)
        {
            return await RepositoryService.ExecuteUpdate(entity);
        }
        public async Task<Job> UpdateJob(Job entity, IEnumerable<string> properties)
        {
            return await RepositoryService.ExecuteUpdate(entity, properties);
        }
        public async Task<Job> UpdateJob(Job updated, Job original)
        {
            if(updated.Id != original.Id) throw new InvalidOperationException("UpdateJob: Entity keys do not match.");
            var _properties = new List<string>();
            if(updated.Name != original.Name) _properties.Add("Name");
            if(_properties.Count == 0) return updated;
            return await RepositoryService.ExecuteUpdate(updated, _properties);
        }
        public async Task<Job> UpdateJob(Job updated, Job original, IEnumerable<string> properties)
        {
            if(updated.Id != original.Id) throw new InvalidOperationException("UpdateJob: Entity keys do not match.");
            var _properties = new List<string>();
            if(properties.Contains("Name") && updated.Name != original.Name) _properties.Add("Name");
            if(_properties.Count == 0) return updated;
            return await RepositoryService.ExecuteUpdate(updated, _properties);
        }
        public async Task DeleteJob(Guid key)
        {
            await RepositoryService.ExecuteDelete<Job>(key);
        }
        public async Task<QueryResult<Job>> QueryJob(Query query)
        {
            return await RepositoryQueryService.ExecuteQuery(query, new JobRepositoryAccessor());
        }
        public async Task<Job?> GetJob(Guid id)
        {
            var sql = @"SELECT * FROM [Job] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await RepositoryService.ExecuteReader<Job>(sql, parameters)).SingleOrDefault();
        }
        public async Task<Job> RequireJob(Guid id)
        {
            return await GetJob(id) ?? throw new InvalidOperationException("The specified Job does not exist");
        }
        public async Task<int> CountJob()
        {
            var sql = @"SELECT COUNT(Id) FROM [Job]";
            return (int)((await RepositoryService.ExecuteScalar(sql))!);
        }
        public async Task<List<Job>> SelectJob()
        {
            var sql = @"SELECT * FROM [Job] ORDER BY Id ASC";
            return await RepositoryService.ExecuteReader<Job>(sql);
        }
        public async Task<List<Job>> SelectJobWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [Job] ORDER BY Id ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await RepositoryService.ExecuteReader<Job>(sql, parameters);
        }
    }
}

