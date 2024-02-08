using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public class ReferenceLookup
    {
        public required Guid Id { get; set; }

        public required string Name { get; set; }
    }

    public interface IReportMap<TEntity> : IReportMap where TEntity : class
    {
    }

    public interface IReportMap
    {
        Task<object?> Get(Guid id);

        Guid Id(object reference);

        string Name(object reference);

        Task<QueryResult<ReferenceLookup>> Query(Query query);
    }
}
