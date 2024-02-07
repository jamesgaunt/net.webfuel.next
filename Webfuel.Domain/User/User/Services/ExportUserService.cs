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
    public interface IExportUserService
    {
        Task<ReportStep> InitialiseReport(QueryUser request);
    }

    [Service(typeof(IExportUserService))]
    internal class ExportUserService : IExportUserService
    {
        private readonly IReportService _reportService;
        private readonly IReportDesignService _reportDesignService;

        public ExportUserService(IReportService reportService, IReportDesignService reportDesignService)
        {
            _reportService = reportService;
            _reportDesignService = reportDesignService;
        }

        public async Task<ReportStep> InitialiseReport(QueryUser request)
        {
            var report = await _reportService.GetDefaultNamedReport("User Export", ReportProviderEnum.User);

            return _reportDesignService.RegisterReport(new ReportRequest
            {
                ReportName = report.Name,
                Design = report.Design,
                Query = request.ApplyCustomFilters()
            });
        }
    }
}
