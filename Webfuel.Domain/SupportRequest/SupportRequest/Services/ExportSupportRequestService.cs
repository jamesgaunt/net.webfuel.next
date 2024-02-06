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
    public interface IExportSupportRequestService
    {
        Task<ReportStep> InitialiseReport(QuerySupportRequest request);
    }

    [Service(typeof(IExportSupportRequestService))]
    internal class ExportSupportRequestService : IExportSupportRequestService
    {
        private readonly IReportService _reportService;
        private readonly IReportDesignService _reportDesignService;

        public ExportSupportRequestService(IReportService reportService, IReportDesignService reportDesignService)
        {
            _reportService = reportService;
            _reportDesignService = reportDesignService;
        }

        public async Task<ReportStep> InitialiseReport(QuerySupportRequest request)
        {
            var report = await _reportService.GetReportByName("Support Request Export", ReportProviderEnum.SupportRequest);

            return _reportDesignService.RegisterReport(new ReportRequest
            {
                ReportName = report.Name,
                Design = report.Design,
                Query = request.ApplyCustomFilters()
            });
        }
    }
}
