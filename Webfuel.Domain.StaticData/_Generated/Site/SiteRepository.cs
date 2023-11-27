using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface ISiteRepository
    {
        Task<Site> InsertSite(Site entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<Site> UpdateSite(Site entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<Site> UpdateSite(Site updated, Site original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteSite(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<Site>> QuerySite(Query query, bool countTotal = true);
        Task<Site?> GetSite(Guid id);
        Task<Site> RequireSite(Guid id);
        Task<int> CountSite();
        Task<List<Site>> SelectSite();
        Task<List<Site>> SelectSiteWithPage(int skip, int take);
        Task<Site?> GetSiteByName(string name);
        Task<Site> RequireSiteByName(string name);
    }
    [Service(typeof(ISiteRepository))]
    internal partial class SiteRepository: ISiteRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public SiteRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<Site> InsertSite(Site entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            SiteMetadata.Validate(entity);
            var sql = SiteMetadata.InsertSQL();
            var parameters = SiteMetadata.ExtractParameters(entity, SiteMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<Site> UpdateSite(Site entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            SiteMetadata.Validate(entity);
            var sql = SiteMetadata.UpdateSQL();
            var parameters = SiteMetadata.ExtractParameters(entity, SiteMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<Site> UpdateSite(Site updated, Site original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateSite(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteSite(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = SiteMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<Site>> QuerySite(Query query, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<Site, SiteMetadata>(query, countTotal);
        }
        public async Task<Site?> GetSite(Guid id)
        {
            var sql = @"SELECT * FROM [Site] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<Site, SiteMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<Site> RequireSite(Guid id)
        {
            return await GetSite(id) ?? throw new InvalidOperationException("The specified Site does not exist");
        }
        public async Task<int> CountSite()
        {
            var sql = @"SELECT COUNT(Id) FROM [Site]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<Site>> SelectSite()
        {
            var sql = @"SELECT * FROM [Site] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<Site, SiteMetadata>(sql);
        }
        public async Task<List<Site>> SelectSiteWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [Site] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<Site, SiteMetadata>(sql, parameters);
        }
        public async Task<Site?> GetSiteByName(string name)
        {
            var sql = @"SELECT * FROM [Site] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<Site, SiteMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<Site> RequireSiteByName(string name)
        {
            return await GetSiteByName(name) ?? throw new InvalidOperationException("The specified Site does not exist");
        }
    }
}

