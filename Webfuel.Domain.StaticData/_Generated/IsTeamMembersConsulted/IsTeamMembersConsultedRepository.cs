using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IIsTeamMembersConsultedRepository
    {
        Task<QueryResult<IsTeamMembersConsulted>> QueryIsTeamMembersConsulted(Query query);
        Task<IsTeamMembersConsulted?> GetIsTeamMembersConsulted(Guid id);
        Task<IsTeamMembersConsulted> RequireIsTeamMembersConsulted(Guid id);
        Task<int> CountIsTeamMembersConsulted();
        Task<List<IsTeamMembersConsulted>> SelectIsTeamMembersConsulted();
        Task<List<IsTeamMembersConsulted>> SelectIsTeamMembersConsultedWithPage(int skip, int take);
        Task<IsTeamMembersConsulted?> GetIsTeamMembersConsultedByName(string name);
        Task<IsTeamMembersConsulted> RequireIsTeamMembersConsultedByName(string name);
    }
    [Service(typeof(IIsTeamMembersConsultedRepository))]
    internal partial class IsTeamMembersConsultedRepository: IIsTeamMembersConsultedRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public IsTeamMembersConsultedRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<QueryResult<IsTeamMembersConsulted>> QueryIsTeamMembersConsulted(Query query)
        {
            return await _connection.ExecuteQuery<IsTeamMembersConsulted, IsTeamMembersConsultedMetadata>(query);
        }
        public async Task<IsTeamMembersConsulted?> GetIsTeamMembersConsulted(Guid id)
        {
            var sql = @"SELECT * FROM [IsTeamMembersConsulted] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<IsTeamMembersConsulted, IsTeamMembersConsultedMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<IsTeamMembersConsulted> RequireIsTeamMembersConsulted(Guid id)
        {
            return await GetIsTeamMembersConsulted(id) ?? throw new InvalidOperationException("The specified IsTeamMembersConsulted does not exist");
        }
        public async Task<int> CountIsTeamMembersConsulted()
        {
            var sql = @"SELECT COUNT(Id) FROM [IsTeamMembersConsulted]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<IsTeamMembersConsulted>> SelectIsTeamMembersConsulted()
        {
            var sql = @"SELECT * FROM [IsTeamMembersConsulted] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<IsTeamMembersConsulted, IsTeamMembersConsultedMetadata>(sql);
        }
        public async Task<List<IsTeamMembersConsulted>> SelectIsTeamMembersConsultedWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [IsTeamMembersConsulted] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<IsTeamMembersConsulted, IsTeamMembersConsultedMetadata>(sql, parameters);
        }
        public async Task<IsTeamMembersConsulted?> GetIsTeamMembersConsultedByName(string name)
        {
            var sql = @"SELECT * FROM [IsTeamMembersConsulted] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<IsTeamMembersConsulted, IsTeamMembersConsultedMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<IsTeamMembersConsulted> RequireIsTeamMembersConsultedByName(string name)
        {
            return await GetIsTeamMembersConsultedByName(name) ?? throw new InvalidOperationException("The specified IsTeamMembersConsulted does not exist");
        }
    }
}

