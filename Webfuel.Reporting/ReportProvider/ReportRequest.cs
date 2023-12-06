using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public class ReportRequest
    {
        public required Guid ProviderId { get; init; }

        public required ReportDesign Design { get; init; }

        public required Dictionary<string, object?> Arguments { get; init; }
    }
}
