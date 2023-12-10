using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    public interface IDataSource<TEntity> where TEntity : class
    {
        Task<TEntity?> Get(Guid id);

        Task<QueryResult<TEntity>> Query(Query query);
    }
}
