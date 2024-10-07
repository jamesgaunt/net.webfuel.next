using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Domain
{
    internal partial interface IWidgetRepository
    {
        Task<Widget> InsertWidget(Widget entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<Widget> UpdateWidget(Widget entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<Widget> UpdateWidget(Widget updated, Widget original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteWidget(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<Widget>> QueryWidget(Query query, bool selectItems = true, bool countTotal = true);
        Task<Widget?> GetWidget(Guid id);
        Task<Widget> RequireWidget(Guid id);
        Task<int> CountWidget();
        Task<List<Widget>> SelectWidget();
        Task<List<Widget>> SelectWidgetWithPage(int skip, int take);
        Task<List<Widget>> SelectWidgetByUserId(Guid userId);
    }
    [Service(typeof(IWidgetRepository))]
    internal partial class WidgetRepository: IWidgetRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public WidgetRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<Widget> InsertWidget(Widget entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            WidgetMetadata.Validate(entity);
            var sql = WidgetMetadata.InsertSQL();
            var parameters = WidgetMetadata.ExtractParameters(entity, WidgetMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<Widget> UpdateWidget(Widget entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            WidgetMetadata.Validate(entity);
            var sql = WidgetMetadata.UpdateSQL();
            var parameters = WidgetMetadata.ExtractParameters(entity, WidgetMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<Widget> UpdateWidget(Widget updated, Widget original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateWidget(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteWidget(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = WidgetMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<Widget>> QueryWidget(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<Widget, WidgetMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<Widget?> GetWidget(Guid id)
        {
            var sql = @"SELECT * FROM [Widget] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<Widget, WidgetMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<Widget> RequireWidget(Guid id)
        {
            return await GetWidget(id) ?? throw new InvalidOperationException("The specified Widget does not exist");
        }
        public async Task<int> CountWidget()
        {
            var sql = @"SELECT COUNT(Id) FROM [Widget]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<Widget>> SelectWidget()
        {
            var sql = @"SELECT * FROM [Widget] ORDER BY SortOrder ASC";
            return await _connection.ExecuteReader<Widget, WidgetMetadata>(sql);
        }
        public async Task<List<Widget>> SelectWidgetWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [Widget] ORDER BY SortOrder ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<Widget, WidgetMetadata>(sql, parameters);
        }
        public async Task<List<Widget>> SelectWidgetByUserId(Guid userId)
        {
            var sql = @"SELECT * FROM [Widget] WHERE UserId = @UserId ORDER BY SortOrder ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@UserId", userId),
            };
            return await _connection.ExecuteReader<Widget, WidgetMetadata>(sql, parameters);
        }
    }
}

