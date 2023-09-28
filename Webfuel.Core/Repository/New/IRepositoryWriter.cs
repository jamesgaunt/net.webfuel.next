using Microsoft.Data.SqlClient;

namespace Webfuel.Repository.New
{
    public interface IRepositoryWriter<TEntity>
    {
        List<SqlParameter> Write(TEntity entity, IEnumerable<string> properties);
    }

}
