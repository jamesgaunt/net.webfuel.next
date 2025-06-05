using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IResearcherProfessionalBackgroundRepository
    {
        Task<QueryResult<ResearcherProfessionalBackground>> QueryResearcherProfessionalBackground(Query query, bool selectItems = true, bool countTotal = true);
        Task<ResearcherProfessionalBackground?> GetResearcherProfessionalBackground(Guid id);
        Task<ResearcherProfessionalBackground> RequireResearcherProfessionalBackground(Guid id);
        Task<int> CountResearcherProfessionalBackground();
        Task<List<ResearcherProfessionalBackground>> SelectResearcherProfessionalBackground();
        Task<List<ResearcherProfessionalBackground>> SelectResearcherProfessionalBackgroundWithPage(int skip, int take);
        Task<ResearcherProfessionalBackground?> GetResearcherProfessionalBackgroundByName(string name);
        Task<ResearcherProfessionalBackground> RequireResearcherProfessionalBackgroundByName(string name);
    }
    [Service(typeof(IResearcherProfessionalBackgroundRepository))]
    internal partial class ResearcherProfessionalBackgroundRepository: IResearcherProfessionalBackgroundRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public ResearcherProfessionalBackgroundRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<QueryResult<ResearcherProfessionalBackground>> QueryResearcherProfessionalBackground(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<ResearcherProfessionalBackground, ResearcherProfessionalBackgroundMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<ResearcherProfessionalBackground?> GetResearcherProfessionalBackground(Guid id)
        {
            var sql = @"SELECT * FROM [ResearcherProfessionalBackground] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<ResearcherProfessionalBackground, ResearcherProfessionalBackgroundMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<ResearcherProfessionalBackground> RequireResearcherProfessionalBackground(Guid id)
        {
            return await GetResearcherProfessionalBackground(id) ?? throw new InvalidOperationException("The specified ResearcherProfessionalBackground does not exist");
        }
        public async Task<int> CountResearcherProfessionalBackground()
        {
            var sql = @"SELECT COUNT(Id) FROM [ResearcherProfessionalBackground]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<ResearcherProfessionalBackground>> SelectResearcherProfessionalBackground()
        {
            var sql = @"SELECT * FROM [ResearcherProfessionalBackground] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<ResearcherProfessionalBackground, ResearcherProfessionalBackgroundMetadata>(sql);
        }
        public async Task<List<ResearcherProfessionalBackground>> SelectResearcherProfessionalBackgroundWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [ResearcherProfessionalBackground] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<ResearcherProfessionalBackground, ResearcherProfessionalBackgroundMetadata>(sql, parameters);
        }
        public async Task<ResearcherProfessionalBackground?> GetResearcherProfessionalBackgroundByName(string name)
        {
            var sql = @"SELECT * FROM [ResearcherProfessionalBackground] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<ResearcherProfessionalBackground, ResearcherProfessionalBackgroundMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<ResearcherProfessionalBackground> RequireResearcherProfessionalBackgroundByName(string name)
        {
            return await GetResearcherProfessionalBackgroundByName(name) ?? throw new InvalidOperationException("The specified ResearcherProfessionalBackground does not exist");
        }
    }
}

