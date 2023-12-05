using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public abstract class ReportBuilder
    {
#pragma warning disable CS8618
        // This is set by the report generator service before every iteration
        public IServiceProvider ServiceProvider { get; set; }
#pragma warning restore CS8618

        public int ProgressCount { get; set; }
        public int TotalCount { get; set; }
        public bool Complete { get; set; }

        public abstract Task InitialiseReport();
        public abstract Task GenerateReport();
        public abstract Task<ReportResult> RenderReport();

    }
}
