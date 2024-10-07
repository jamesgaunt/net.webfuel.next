using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain
{
    internal partial interface IWidgetTypeRepository
    {
        Task<QueryResult<WidgetType>> QueryWidgetType(Query query, bool selectItems = true, bool countTotal = true);
        Task<WidgetType?> GetWidgetType(Guid id);
        Task<WidgetType> RequireWidgetType(Guid id);
        Task<int> CountWidgetType();
        Task<List<WidgetType>> SelectWidgetType();
        Task<List<WidgetType>> SelectWidgetTypeWithPage(int skip, int take);
    }
    [Service(typeof(IWidgetTypeRepository))]
    internal partial class WidgetTypeRepository: IWidgetTypeRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public WidgetTypeRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<QueryResult<WidgetType>> QueryWidgetType(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<WidgetType, WidgetTypeMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<WidgetType?> GetWidgetType(Guid id)
        {
            var sql = @"SELECT * FROM [WidgetType] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<WidgetType, WidgetTypeMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<WidgetType> RequireWidgetType(Guid id)
        {
            return await GetWidgetType(id) ?? throw new InvalidOperationException("The specified WidgetType does not exist");
        }
        public async Task<int> CountWidgetType()
        {
            var sql = @"SELECT COUNT(Id) FROM [WidgetType]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<WidgetType>> SelectWidgetType()
        {
            var sql = @"SELECT * FROM [WidgetType] ORDER BY Id ASC";
            return await _connection.ExecuteReader<WidgetType, WidgetTypeMetadata>(sql);
        }
        public async Task<List<WidgetType>> SelectWidgetTypeWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [WidgetType] ORDER BY Id ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<WidgetType, WidgetTypeMetadata>(sql, parameters);
        }
    }
}

