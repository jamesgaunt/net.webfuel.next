using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public interface IReportReferenceProvider
    {
        Task<ReportReference?> GetReportReference(Guid id);

        Task<QueryResult<ReportReference>> QueryReportReference(Query query);
    }
}
