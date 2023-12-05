using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Common;
using Webfuel.Domain.StaticData;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    public interface IProjectReportProvider: IReportProvider
    {
    }

    [Service(typeof(IProjectReportProvider), typeof(IReportProvider))]
    internal class ProjectReportProvider : IProjectReportProvider
    {
        public Guid Id => ReportProviderEnum.Project;

        public Task<ReportSchema> GetReportSchema()
        {
            return Task.FromResult(ProjectReportSchema.Schema);
        }

        public Task<ReportBuilder> InitialiseReport(ReportRequest request)
        {
            return Task.FromResult<ReportBuilder>(new ProjectReportBuilder(request));
        }
    }
}
