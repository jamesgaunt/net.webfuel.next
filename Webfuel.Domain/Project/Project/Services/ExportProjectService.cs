using Microsoft.AspNetCore.Mvc.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Common;
using Webfuel.Domain.StaticData;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    public interface IExportProjectService
    {
        Task<ReportStep> InitialiseReport(QueryProject request);
    }

    [Service(typeof(IExportProjectService))]
    internal class ExportProjectService : IExportProjectService
    {
        private readonly IReportService _reportService;
        private readonly IReportDesignService _reportDesignService;

        public ExportProjectService(IReportService reportService, IReportDesignService reportDesignService)
        {
            _reportService = reportService;
            _reportDesignService = reportDesignService;
        }

        public async Task<ReportStep> InitialiseReport(QueryProject request)
        {
            var report = await _reportService.GetReportByName("Project Export", ReportProviderEnum.Project);

            return _reportDesignService.RegisterReport(new ReportRequest
            {
                ReportName = report.Name,
                Design = report.Design,
                Query = request.ApplyCustomFilters()
            });
        }
    }
}
