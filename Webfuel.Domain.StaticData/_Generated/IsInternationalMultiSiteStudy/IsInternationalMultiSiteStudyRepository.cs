using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IIsInternationalMultiSiteStudyRepository
    {
        Task<QueryResult<IsInternationalMultiSiteStudy>> QueryIsInternationalMultiSiteStudy(Query query);
        Task<IsInternationalMultiSiteStudy?> GetIsInternationalMultiSiteStudy(Guid id);
        Task<IsInternationalMultiSiteStudy> RequireIsInternationalMultiSiteStudy(Guid id);
        Task<int> CountIsInternationalMultiSiteStudy();
        Task<List<IsInternationalMultiSiteStudy>> SelectIsInternationalMultiSiteStudy();
        Task<List<IsInternationalMultiSiteStudy>> SelectIsInternationalMultiSiteStudyWithPage(int skip, int take);
        Task<IsInternationalMultiSiteStudy?> GetIsInternationalMultiSiteStudyByName(string name);
        Task<IsInternationalMultiSiteStudy> RequireIsInternationalMultiSiteStudyByName(string name);
    }
    [Service(typeof(IIsInternationalMultiSiteStudyRepository))]
    internal partial class IsInternationalMultiSiteStudyRepository: IIsInternationalMultiSiteStudyRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public IsInternationalMultiSiteStudyRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<QueryResult<IsInternationalMultiSiteStudy>> QueryIsInternationalMultiSiteStudy(Query query)
        {
            return await _connection.ExecuteQuery<IsInternationalMultiSiteStudy, IsInternationalMultiSiteStudyMetadata>(query);
        }
        public async Task<IsInternationalMultiSiteStudy?> GetIsInternationalMultiSiteStudy(Guid id)
        {
            var sql = @"SELECT * FROM [IsInternationalMultiSiteStudy] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<IsInternationalMultiSiteStudy, IsInternationalMultiSiteStudyMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<IsInternationalMultiSiteStudy> RequireIsInternationalMultiSiteStudy(Guid id)
        {
            return await GetIsInternationalMultiSiteStudy(id) ?? throw new InvalidOperationException("The specified IsInternationalMultiSiteStudy does not exist");
        }
        public async Task<int> CountIsInternationalMultiSiteStudy()
        {
            var sql = @"SELECT COUNT(Id) FROM [IsInternationalMultiSiteStudy]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<IsInternationalMultiSiteStudy>> SelectIsInternationalMultiSiteStudy()
        {
            var sql = @"SELECT * FROM [IsInternationalMultiSiteStudy] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<IsInternationalMultiSiteStudy, IsInternationalMultiSiteStudyMetadata>(sql);
        }
        public async Task<List<IsInternationalMultiSiteStudy>> SelectIsInternationalMultiSiteStudyWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [IsInternationalMultiSiteStudy] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<IsInternationalMultiSiteStudy, IsInternationalMultiSiteStudyMetadata>(sql, parameters);
        }
        public async Task<IsInternationalMultiSiteStudy?> GetIsInternationalMultiSiteStudyByName(string name)
        {
            var sql = @"SELECT * FROM [IsInternationalMultiSiteStudy] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<IsInternationalMultiSiteStudy, IsInternationalMultiSiteStudyMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<IsInternationalMultiSiteStudy> RequireIsInternationalMultiSiteStudyByName(string name)
        {
            return await GetIsInternationalMultiSiteStudyByName(name) ?? throw new InvalidOperationException("The specified IsInternationalMultiSiteStudy does not exist");
        }
    }
}

