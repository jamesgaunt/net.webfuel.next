using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public interface IReportProvider
    {
        Guid Id { get; }

        Task<IReportSchema> GetReportSchema();

        Task<ReportProgress> InitialiseReport(Report report, ReportRequest request);
    }
}
