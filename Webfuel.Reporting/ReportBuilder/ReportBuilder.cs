using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public abstract class ReportBuilder
    {
        public int ProgressCount { get; set; }
        public int TotalCount { get; set; }
        public bool Complete { get; set; }

        public abstract Task InitialiseReport(IServiceProvider services);
        public abstract Task GenerateReport(IServiceProvider services);
        public abstract Task<ReportResult> RenderReport(IServiceProvider services);

    }

    public abstract class StandardReportBuilder: ReportBuilder
    {
        public StandardReportBuilder(ReportDesign design, ReportQuery query)
        {
            Design = design;
            Query = query;
        }

        public override async Task InitialiseReport(IServiceProvider services)
        {
            TotalCount = await Query.GetTotalCount(services);
        }

        public override async Task GenerateReport(IServiceProvider services)
        {
            var items = await Query.GetItems(ProgressCount, ItemsPerStep, services);

            if (!items.Any())
            {
                ProgressCount = TotalCount;
                Complete = true;
                return;
            }

            ProgressCount += ItemsPerStep;
        }

        public int ItemsPerStep { get; set; } = 10;

        public ReportData Data { get; } = new ReportData();
        
        public ReportDesign Design { get; }
        
        public ReportQuery Query { get; }
    }
}
