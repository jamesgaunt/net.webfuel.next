using Microsoft.Data.SqlClient;


namespace Webfuel
{
    public interface IRepositoryMapper<TEntity> where TEntity : class
    {
        TEntity ActivateEntity(SqlDataReader dr);

        Task<TEntity> ExecuteInsert(IRepositoryService repositoryService, TEntity entity, IEnumerable<string>? properties = null, CancellationToken? cancellationToken = null);

        Task<TEntity> ExecuteUpdate(IRepositoryService repositoryService, TEntity entity, IEnumerable<string>? properties = null, CancellationToken? cancellationToken = null);

        Task ExecuteDelete(IRepositoryService repositoryService, object key, CancellationToken? cancellationToken = null);
    }
}
