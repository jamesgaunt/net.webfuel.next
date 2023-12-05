using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IProjectStatusRepository
    {
        Task<QueryResult<ProjectStatus>> QueryProjectStatus(Query query, bool selectItems = true, bool countTotal = true);
        Task<ProjectStatus?> GetProjectStatus(Guid id);
        Task<ProjectStatus> RequireProjectStatus(Guid id);
        Task<int> CountProjectStatus();
        Task<List<ProjectStatus>> SelectProjectStatus();
        Task<List<ProjectStatus>> SelectProjectStatusWithPage(int skip, int take);
        Task<ProjectStatus?> GetProjectStatusByName(string name);
        Task<ProjectStatus> RequireProjectStatusByName(string name);
    }
    [Service(typeof(IProjectStatusRepository))]
    internal partial class ProjectStatusRepository: IProjectStatusRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public ProjectStatusRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<QueryResult<ProjectStatus>> QueryProjectStatus(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<ProjectStatus, ProjectStatusMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<ProjectStatus?> GetProjectStatus(Guid id)
        {
            var sql = @"SELECT * FROM [ProjectStatus] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<ProjectStatus, ProjectStatusMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<ProjectStatus> RequireProjectStatus(Guid id)
        {
            return await GetProjectStatus(id) ?? throw new InvalidOperationException("The specified ProjectStatus does not exist");
        }
        public async Task<int> CountProjectStatus()
        {
            var sql = @"SELECT COUNT(Id) FROM [ProjectStatus]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<ProjectStatus>> SelectProjectStatus()
        {
            var sql = @"SELECT * FROM [ProjectStatus] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<ProjectStatus, ProjectStatusMetadata>(sql);
        }
        public async Task<List<ProjectStatus>> SelectProjectStatusWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [ProjectStatus] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<ProjectStatus, ProjectStatusMetadata>(sql, parameters);
        }
        public async Task<ProjectStatus?> GetProjectStatusByName(string name)
        {
            var sql = @"SELECT * FROM [ProjectStatus] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<ProjectStatus, ProjectStatusMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<ProjectStatus> RequireProjectStatusByName(string name)
        {
            return await GetProjectStatusByName(name) ?? throw new InvalidOperationException("The specified ProjectStatus does not exist");
        }
    }
}

