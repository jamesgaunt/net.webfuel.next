using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public class ReportReference 
    {
        public required Guid Id { get; init; }

        public required string Name { get; init; }

        public required object Entity { get; init; }

        public override string ToString()
        {
            return Name;
        }
    }

    public interface IReportReferenceProvider<TEntity>
    {
        Task<ReportReference?> Get(Guid id);

        Task<QueryResult<ReportReference>> Query(Query query);
    }
}
