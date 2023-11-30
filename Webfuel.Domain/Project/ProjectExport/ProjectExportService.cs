using Microsoft.AspNetCore.Mvc.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Common;

namespace Webfuel.Domain
{

    public interface IProjectExportService
    {
        Task<ReportProgress> InitialiseReport(ProjectExportRequest request);
    }

    [Service(typeof(IProjectExportService))]
    internal class ProjectExportService : IProjectExportService
    {
        private readonly IProjectReportProvider _projectReportProvider;

        public ProjectExportService(IProjectReportProvider projectReportProvider)
        {
            _projectReportProvider = projectReportProvider;
        }

        public Task<ReportProgress> InitialiseReport(ProjectExportRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
