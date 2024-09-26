using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IProfessionalBackgroundDetailRepository
    {
        Task<ProfessionalBackgroundDetail> InsertProfessionalBackgroundDetail(ProfessionalBackgroundDetail entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<ProfessionalBackgroundDetail> UpdateProfessionalBackgroundDetail(ProfessionalBackgroundDetail entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<ProfessionalBackgroundDetail> UpdateProfessionalBackgroundDetail(ProfessionalBackgroundDetail updated, ProfessionalBackgroundDetail original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteProfessionalBackgroundDetail(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<ProfessionalBackgroundDetail>> QueryProfessionalBackgroundDetail(Query query, bool selectItems = true, bool countTotal = true);
        Task<ProfessionalBackgroundDetail?> GetProfessionalBackgroundDetail(Guid id);
        Task<ProfessionalBackgroundDetail> RequireProfessionalBackgroundDetail(Guid id);
        Task<int> CountProfessionalBackgroundDetail();
        Task<List<ProfessionalBackgroundDetail>> SelectProfessionalBackgroundDetail();
        Task<List<ProfessionalBackgroundDetail>> SelectProfessionalBackgroundDetailWithPage(int skip, int take);
    }
    [Service(typeof(IProfessionalBackgroundDetailRepository))]
    internal partial class ProfessionalBackgroundDetailRepository: IProfessionalBackgroundDetailRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public ProfessionalBackgroundDetailRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<ProfessionalBackgroundDetail> InsertProfessionalBackgroundDetail(ProfessionalBackgroundDetail entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            ProfessionalBackgroundDetailMetadata.Validate(entity);
            var sql = ProfessionalBackgroundDetailMetadata.InsertSQL();
            var parameters = ProfessionalBackgroundDetailMetadata.ExtractParameters(entity, ProfessionalBackgroundDetailMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<ProfessionalBackgroundDetail> UpdateProfessionalBackgroundDetail(ProfessionalBackgroundDetail entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            ProfessionalBackgroundDetailMetadata.Validate(entity);
            var sql = ProfessionalBackgroundDetailMetadata.UpdateSQL();
            var parameters = ProfessionalBackgroundDetailMetadata.ExtractParameters(entity, ProfessionalBackgroundDetailMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<ProfessionalBackgroundDetail> UpdateProfessionalBackgroundDetail(ProfessionalBackgroundDetail updated, ProfessionalBackgroundDetail original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateProfessionalBackgroundDetail(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteProfessionalBackgroundDetail(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = ProfessionalBackgroundDetailMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<ProfessionalBackgroundDetail>> QueryProfessionalBackgroundDetail(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<ProfessionalBackgroundDetail, ProfessionalBackgroundDetailMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<ProfessionalBackgroundDetail?> GetProfessionalBackgroundDetail(Guid id)
        {
            var sql = @"SELECT * FROM [ProfessionalBackgroundDetail] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<ProfessionalBackgroundDetail, ProfessionalBackgroundDetailMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<ProfessionalBackgroundDetail> RequireProfessionalBackgroundDetail(Guid id)
        {
            return await GetProfessionalBackgroundDetail(id) ?? throw new InvalidOperationException("The specified ProfessionalBackgroundDetail does not exist");
        }
        public async Task<int> CountProfessionalBackgroundDetail()
        {
            var sql = @"SELECT COUNT(Id) FROM [ProfessionalBackgroundDetail]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<ProfessionalBackgroundDetail>> SelectProfessionalBackgroundDetail()
        {
            var sql = @"SELECT * FROM [ProfessionalBackgroundDetail] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<ProfessionalBackgroundDetail, ProfessionalBackgroundDetailMetadata>(sql);
        }
        public async Task<List<ProfessionalBackgroundDetail>> SelectProfessionalBackgroundDetailWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [ProfessionalBackgroundDetail] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<ProfessionalBackgroundDetail, ProfessionalBackgroundDetailMetadata>(sql, parameters);
        }
    }
}

