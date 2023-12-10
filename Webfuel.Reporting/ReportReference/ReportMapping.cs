using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public interface IReportMapper<TEntity> where TEntity : class
    {
        Task<TEntity?> Get(Guid id);

        Task<QueryResult<TEntity>> Query(Query query);
    }
}
