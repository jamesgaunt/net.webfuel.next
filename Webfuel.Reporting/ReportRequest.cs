using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public class ReportRequest
    {
        public required string ReportName { get; init; }

        public required Guid ReportProviderId { get; init; }

        public required ReportDesign Design { get; init; }

        public required List<ReportFilter> Arguments { get; init; }
    }
}
