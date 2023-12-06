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

        ReportBuilder GetReportBuilder(ReportRequest request);

        Task<IEnumerable<object>> QueryItems(int skip, int take);

        Task<int> GetTotalCount();
    }
}
