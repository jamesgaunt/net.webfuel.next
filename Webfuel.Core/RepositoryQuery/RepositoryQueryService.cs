namespace Webfuel
{
    public interface IRepositoryQueryService
    {
        Task<QueryResult<TItem>> ExecuteQueryAsync<TItem>(string spanName, RepositoryQuery query, IRepositoryAccessor<TItem> accessor) where TItem : class;
    }

    [ServiceImplementation(typeof(IRepositoryQueryService))]
    internal class RepositoryQueryService : IRepositoryQueryService
    {
        private readonly IRepositoryService RepositoryService;

        public RepositoryQueryService(IRepositoryService repositoryService)
        {
            RepositoryService = repositoryService;
        }

        public async Task<QueryResult<TItem>> ExecuteQueryAsync<TItem>(string spanName, RepositoryQuery query, IRepositoryAccessor<TItem> accessor) where TItem : class
        {
            var fields = accessor.InsertProperties.ToList();

            RepositoryQueryUtility.PurgeFilters(query, fields);

            var parameters = new List<RepositoryQueryParameter>();
            var filterSql = RepositoryQueryUtility.FilterSql(query, parameters);

            var selectSql = RepositoryQueryUtility.SelectSql(query, fields);
            var fromSql = $"FROM [{accessor.DatabaseSchema}].[{accessor.DatabaseTable}]";
            var orderSql = RepositoryQueryUtility.OrderSql(query, fields, accessor.DefaultOrderBy);
            var pageSql = RepositoryQueryUtility.PageSql(query);

            var querySql = $"{selectSql} {fromSql} {filterSql} {orderSql} {pageSql}";

            var items = await RepositoryService.ExecuteReaderAsync<TItem>(spanName, querySql, RepositoryQueryUtility.SqlParameters(parameters));
            int? totalCount = null;

            if (!String.IsNullOrEmpty(pageSql))
            {
                var countSql = RepositoryQueryUtility.CountSql(query, fields);
                querySql = $"{countSql} {fromSql} {filterSql}";
                totalCount = (int)(await RepositoryService.ExecuteScalarAsync(spanName, querySql, RepositoryQueryUtility.SqlParameters(parameters)))!;
            }

            return new QueryResult<TItem>(items, totalCount);
        }
    }
}
