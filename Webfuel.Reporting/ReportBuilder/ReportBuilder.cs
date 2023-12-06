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
        // These are set by the report generator service before every iteration
        public IServiceProvider ServiceProvider { get; set; }
        public IReportDesignService ReportDesignService { get; set; }
#pragma warning restore CS8618

        public string Stage { get; set; } = String.Empty;
        public int StageCount { get; set; }
        public int StageTotal { get; set; }
        public bool Complete { get; set; }

        public abstract Task GenerateReport();
        public abstract Task<ReportResult> RenderReport();

    }
}
