using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IProfessionalBackgroundRepository
    {
        Task<QueryResult<ProfessionalBackground>> QueryProfessionalBackground(Query query, bool selectItems = true, bool countTotal = true);
        Task<ProfessionalBackground?> GetProfessionalBackground(Guid id);
        Task<ProfessionalBackground> RequireProfessionalBackground(Guid id);
        Task<int> CountProfessionalBackground();
        Task<List<ProfessionalBackground>> SelectProfessionalBackground();
        Task<List<ProfessionalBackground>> SelectProfessionalBackgroundWithPage(int skip, int take);
        Task<ProfessionalBackground?> GetProfessionalBackgroundByName(string name);
        Task<ProfessionalBackground> RequireProfessionalBackgroundByName(string name);
    }
    [Service(typeof(IProfessionalBackgroundRepository))]
    internal partial class ProfessionalBackgroundRepository: IProfessionalBackgroundRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public ProfessionalBackgroundRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<QueryResult<ProfessionalBackground>> QueryProfessionalBackground(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<ProfessionalBackground, ProfessionalBackgroundMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<ProfessionalBackground?> GetProfessionalBackground(Guid id)
        {
            var sql = @"SELECT * FROM [ProfessionalBackground] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<ProfessionalBackground, ProfessionalBackgroundMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<ProfessionalBackground> RequireProfessionalBackground(Guid id)
        {
            return await GetProfessionalBackground(id) ?? throw new InvalidOperationException("The specified ProfessionalBackground does not exist");
        }
        public async Task<int> CountProfessionalBackground()
        {
            var sql = @"SELECT COUNT(Id) FROM [ProfessionalBackground]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<ProfessionalBackground>> SelectProfessionalBackground()
        {
            var sql = @"SELECT * FROM [ProfessionalBackground] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<ProfessionalBackground, ProfessionalBackgroundMetadata>(sql);
        }
        public async Task<List<ProfessionalBackground>> SelectProfessionalBackgroundWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [ProfessionalBackground] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<ProfessionalBackground, ProfessionalBackgroundMetadata>(sql, parameters);
        }
        public async Task<ProfessionalBackground?> GetProfessionalBackgroundByName(string name)
        {
            var sql = @"SELECT * FROM [ProfessionalBackground] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<ProfessionalBackground, ProfessionalBackgroundMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<ProfessionalBackground> RequireProfessionalBackgroundByName(string name)
        {
            return await GetProfessionalBackgroundByName(name) ?? throw new InvalidOperationException("The specified ProfessionalBackground does not exist");
        }
    }
}

