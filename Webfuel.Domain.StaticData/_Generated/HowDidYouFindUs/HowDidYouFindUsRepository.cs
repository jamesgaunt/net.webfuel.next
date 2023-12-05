using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IHowDidYouFindUsRepository
    {
        Task<HowDidYouFindUs> InsertHowDidYouFindUs(HowDidYouFindUs entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<HowDidYouFindUs> UpdateHowDidYouFindUs(HowDidYouFindUs entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<HowDidYouFindUs> UpdateHowDidYouFindUs(HowDidYouFindUs updated, HowDidYouFindUs original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteHowDidYouFindUs(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<HowDidYouFindUs>> QueryHowDidYouFindUs(Query query, bool selectItems = true, bool countTotal = true);
        Task<HowDidYouFindUs?> GetHowDidYouFindUs(Guid id);
        Task<HowDidYouFindUs> RequireHowDidYouFindUs(Guid id);
        Task<int> CountHowDidYouFindUs();
        Task<List<HowDidYouFindUs>> SelectHowDidYouFindUs();
        Task<List<HowDidYouFindUs>> SelectHowDidYouFindUsWithPage(int skip, int take);
        Task<HowDidYouFindUs?> GetHowDidYouFindUsByName(string name);
        Task<HowDidYouFindUs> RequireHowDidYouFindUsByName(string name);
    }
    [Service(typeof(IHowDidYouFindUsRepository))]
    internal partial class HowDidYouFindUsRepository: IHowDidYouFindUsRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public HowDidYouFindUsRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<HowDidYouFindUs> InsertHowDidYouFindUs(HowDidYouFindUs entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            HowDidYouFindUsMetadata.Validate(entity);
            var sql = HowDidYouFindUsMetadata.InsertSQL();
            var parameters = HowDidYouFindUsMetadata.ExtractParameters(entity, HowDidYouFindUsMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<HowDidYouFindUs> UpdateHowDidYouFindUs(HowDidYouFindUs entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            HowDidYouFindUsMetadata.Validate(entity);
            var sql = HowDidYouFindUsMetadata.UpdateSQL();
            var parameters = HowDidYouFindUsMetadata.ExtractParameters(entity, HowDidYouFindUsMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<HowDidYouFindUs> UpdateHowDidYouFindUs(HowDidYouFindUs updated, HowDidYouFindUs original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateHowDidYouFindUs(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteHowDidYouFindUs(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = HowDidYouFindUsMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<HowDidYouFindUs>> QueryHowDidYouFindUs(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<HowDidYouFindUs, HowDidYouFindUsMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<HowDidYouFindUs?> GetHowDidYouFindUs(Guid id)
        {
            var sql = @"SELECT * FROM [HowDidYouFindUs] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<HowDidYouFindUs, HowDidYouFindUsMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<HowDidYouFindUs> RequireHowDidYouFindUs(Guid id)
        {
            return await GetHowDidYouFindUs(id) ?? throw new InvalidOperationException("The specified HowDidYouFindUs does not exist");
        }
        public async Task<int> CountHowDidYouFindUs()
        {
            var sql = @"SELECT COUNT(Id) FROM [HowDidYouFindUs]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<HowDidYouFindUs>> SelectHowDidYouFindUs()
        {
            var sql = @"SELECT * FROM [HowDidYouFindUs] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<HowDidYouFindUs, HowDidYouFindUsMetadata>(sql);
        }
        public async Task<List<HowDidYouFindUs>> SelectHowDidYouFindUsWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [HowDidYouFindUs] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<HowDidYouFindUs, HowDidYouFindUsMetadata>(sql, parameters);
        }
        public async Task<HowDidYouFindUs?> GetHowDidYouFindUsByName(string name)
        {
            var sql = @"SELECT * FROM [HowDidYouFindUs] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<HowDidYouFindUs, HowDidYouFindUsMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<HowDidYouFindUs> RequireHowDidYouFindUsByName(string name)
        {
            return await GetHowDidYouFindUsByName(name) ?? throw new InvalidOperationException("The specified HowDidYouFindUs does not exist");
        }
    }
}

