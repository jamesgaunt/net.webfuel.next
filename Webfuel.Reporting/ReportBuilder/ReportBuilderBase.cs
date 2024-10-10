using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public abstract class ReportBuilderBase
    {
#pragma warning disable CS8618
        
        // Set by the report generator service before every iteration
        public IServiceProvider ServiceProvider { get; set; }

#pragma warning restore CS8618

        public string Stage { get; set; } = String.Empty;
        public int StageCount { get; set; }
        public int StageTotal { get; set; }
        public bool Complete { get; set; }
        public ReportBuilderMetrics Metrics { get; } = new ReportBuilderMetrics();

        public abstract Task GenerateReport();
        public abstract ReportResult RenderReport();
        public virtual object ExtractReportData() { throw new NotImplementedException(); }
    }
}
