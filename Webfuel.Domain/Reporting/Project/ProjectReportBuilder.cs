using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    internal class ProjectReportBuilder : StandardReportBuilder
    {
        public ProjectReportBuilder(Webfuel.Reporting.ReportDesign design):
            base(design, new ProjectReportQuery())
        {
        }

        public override Task<Reporting.ReportResult> RenderReport(IServiceProvider services)
        {
            throw new NotImplementedException();
        }

        public Task ProcessItem(object item, IServiceProvider services)
        {
            throw new NotImplementedException();
        }
    }
}
