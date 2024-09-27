using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain.StaticData
{
    internal partial interface IStaffRoleRepository
    {
        Task<StaffRole> InsertStaffRole(StaffRole entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<StaffRole> UpdateStaffRole(StaffRole entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<StaffRole> UpdateStaffRole(StaffRole updated, StaffRole original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteStaffRole(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<StaffRole>> QueryStaffRole(Query query, bool selectItems = true, bool countTotal = true);
        Task<StaffRole?> GetStaffRole(Guid id);
        Task<StaffRole> RequireStaffRole(Guid id);
        Task<int> CountStaffRole();
        Task<List<StaffRole>> SelectStaffRole();
        Task<List<StaffRole>> SelectStaffRoleWithPage(int skip, int take);
        Task<StaffRole?> GetStaffRoleByName(string name);
        Task<StaffRole> RequireStaffRoleByName(string name);
    }
    [Service(typeof(IStaffRoleRepository))]
    internal partial class StaffRoleRepository: IStaffRoleRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public StaffRoleRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<StaffRole> InsertStaffRole(StaffRole entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            StaffRoleMetadata.Validate(entity);
            var sql = StaffRoleMetadata.InsertSQL();
            var parameters = StaffRoleMetadata.ExtractParameters(entity, StaffRoleMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<StaffRole> UpdateStaffRole(StaffRole entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            StaffRoleMetadata.Validate(entity);
            var sql = StaffRoleMetadata.UpdateSQL();
            var parameters = StaffRoleMetadata.ExtractParameters(entity, StaffRoleMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<StaffRole> UpdateStaffRole(StaffRole updated, StaffRole original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateStaffRole(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteStaffRole(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = StaffRoleMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<StaffRole>> QueryStaffRole(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<StaffRole, StaffRoleMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<StaffRole?> GetStaffRole(Guid id)
        {
            var sql = @"SELECT * FROM [StaffRole] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<StaffRole, StaffRoleMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<StaffRole> RequireStaffRole(Guid id)
        {
            return await GetStaffRole(id) ?? throw new InvalidOperationException("The specified StaffRole does not exist");
        }
        public async Task<int> CountStaffRole()
        {
            var sql = @"SELECT COUNT(Id) FROM [StaffRole]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<StaffRole>> SelectStaffRole()
        {
            var sql = @"SELECT * FROM [StaffRole] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<StaffRole, StaffRoleMetadata>(sql);
        }
        public async Task<List<StaffRole>> SelectStaffRoleWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [StaffRole] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<StaffRole, StaffRoleMetadata>(sql, parameters);
        }
        public async Task<StaffRole?> GetStaffRoleByName(string name)
        {
            var sql = @"SELECT * FROM [StaffRole] WHERE Name = @Name ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<StaffRole, StaffRoleMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<StaffRole> RequireStaffRoleByName(string name)
        {
            return await GetStaffRoleByName(name) ?? throw new InvalidOperationException("The specified StaffRole does not exist");
        }
    }
}

