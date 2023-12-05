using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    internal class ProjectReportBuilder : SchemaReportBuilder
    {
        public ProjectReportBuilder(ReportRequest request):
            base(ProjectReportSchema.Schema, request, new ProjectReportQuery())
        {
        }
    }
}
