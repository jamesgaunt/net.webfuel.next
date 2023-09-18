using Microsoft.Data.SqlClient;


namespace Webfuel
{
    public interface IRepositoryMapper<TEntity> where TEntity : class
    {
        TEntity ActivateEntity(SqlDataReader dr);

        Task<TEntity> ExecuteInsertAsync(IRepositoryService repositoryService, TEntity entity, IEnumerable<string>? properties = null, CancellationToken? cancellationToken = null);

        Task<TEntity> ExecuteUpdateAsync(IRepositoryService repositoryService, TEntity entity, IEnumerable<string>? properties = null, CancellationToken? cancellationToken = null);

        Task ExecuteDeleteAsync(IRepositoryService repositoryService, object key, CancellationToken? cancellationToken = null);
    }
}
