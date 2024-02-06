using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public interface IReportProvider
    {
        Guid Id { get; }

        ReportSchema Schema { get; }

        ReportBuilderBase GetReportBuilder(ReportRequest request);

        Task<IEnumerable<object>> QueryItems(Query query);

        Task<int> GetTotalCount(Query query);
    }
}
