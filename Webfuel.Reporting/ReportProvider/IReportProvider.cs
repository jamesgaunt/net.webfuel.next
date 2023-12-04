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

        Task<ReportSchema> GetReportSchema();

        Task<ReportBuilder> InitialiseReport(ReportRequest request);
    }
}
