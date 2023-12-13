using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public interface IReportMapper<TEntity> : IReportMapper where TEntity : class
    {
    }

    public interface IReportMapper
    {
        Task<object?> Get(Guid id);

        Task<QueryResult<object>> Query(Query query);

        Guid Id(object reference);

        string DisplayName(object reference);
    }
}
