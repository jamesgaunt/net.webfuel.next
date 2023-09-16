using Microsoft.Data.SqlClient;


namespace Webfuel
{
    public interface IRepositoryMapper<TEntity> where TEntity : class
    {
        TEntity ActivateEntity(SqlDataReader dr);

        Task<TEntity> ExecuteInsertAsync(string spanName, IRepositoryService repositoryService, TEntity entity, IEnumerable<string>? properties = null);

        Task<TEntity> ExecuteUpdateAsync(string spanName, IRepositoryService repositoryService, TEntity entity, IEnumerable<string>? properties = null);

        Task ExecuteDeleteAsync(string spanName, IRepositoryService repositoryService, object key);
    }
}
