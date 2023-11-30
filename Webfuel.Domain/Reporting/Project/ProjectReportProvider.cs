using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Common;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain
{
    public interface IProjectReportProvider: IReportProvider
    {
    }

    [Service(typeof(IProjectReportProvider), typeof(IReportProvider))]
    internal class ProjectReportProvider : IProjectReportProvider
    {
        public Guid Id => ReportProviderEnum.Project;

        public Task<IReportSchema> GetReportSchema()
        {
            return Task.FromResult<IReportSchema>(Schema);
        }

        public Task<ReportProgress> InitialiseReport(ReportRequest request)
        {
            throw new NotImplementedException();
        }

        static ProjectReportSchema Schema = new ProjectReportSchema();
    }
}
