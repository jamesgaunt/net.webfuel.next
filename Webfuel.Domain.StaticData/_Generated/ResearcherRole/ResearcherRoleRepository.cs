using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IResearcherRoleRepository
    {
        Task<QueryResult<ResearcherRole>> QueryResearcherRole(Query query, bool selectItems = true, bool countTotal = true);
        Task<ResearcherRole?> GetResearcherRole(Guid id);
        Task<ResearcherRole> RequireResearcherRole(Guid id);
        Task<int> CountResearcherRole();
        Task<List<ResearcherRole>> SelectResearcherRole();
        Task<List<ResearcherRole>> SelectResearcherRoleWithPage(int skip, int take);
        Task<ResearcherRole?> GetResearcherRoleByName(string name);
        Task<ResearcherRole> RequireResearcherRoleByName(string name);
    }
    [Service(typeof(IResearcherRoleRepository))]
    internal partial class ResearcherRoleRepository: IResearcherRoleRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public ResearcherRoleRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<QueryResult<ResearcherRole>> QueryResearcherRole(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<ResearcherRole, ResearcherRoleMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<ResearcherRole?> GetResearcherRole(Guid id)
        {
            var sql = @"SELECT * FROM [ResearcherRole] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<ResearcherRole, ResearcherRoleMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<ResearcherRole> RequireResearcherRole(Guid id)
        {
            return await GetResearcherRole(id) ?? throw new InvalidOperationException("The specified ResearcherRole does not exist");
        }
        public async Task<int> CountResearcherRole()
        {
            var sql = @"SELECT COUNT(Id) FROM [ResearcherRole]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<ResearcherRole>> SelectResearcherRole()
        {
            var sql = @"SELECT * FROM [ResearcherRole] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<ResearcherRole, ResearcherRoleMetadata>(sql);
        }
        public async Task<List<ResearcherRole>> SelectResearcherRoleWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [ResearcherRole] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<ResearcherRole, ResearcherRoleMetadata>(sql, parameters);
        }
        public async Task<ResearcherRole?> GetResearcherRoleByName(string name)
        {
            var sql = @"SELECT * FROM [ResearcherRole] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<ResearcherRole, ResearcherRoleMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<ResearcherRole> RequireResearcherRoleByName(string name)
        {
            return await GetResearcherRoleByName(name) ?? throw new InvalidOperationException("The specified ResearcherRole does not exist");
        }
    }
}

