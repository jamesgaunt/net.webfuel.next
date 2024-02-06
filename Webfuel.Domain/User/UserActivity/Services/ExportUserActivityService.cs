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
    public interface IExportUserActivityService
    {
        Task<ReportStep> InitialiseReport(QueryUserActivity request);
    }

    [Service(typeof(IExportUserActivityService))]
    internal class ExportUserActivityService : IExportUserActivityService
    {
        private readonly IReportService _reportService;
        private readonly IReportDesignService _reportDesignService;
        private readonly IIdentityAccessor _identityAccessor;

        public ExportUserActivityService(IReportService reportService, IReportDesignService reportDesignService, IIdentityAccessor identityAccessor)
        {
            _reportService = reportService;
            _reportDesignService = reportDesignService;
            _identityAccessor = identityAccessor;
        }

        public async Task<ReportStep> InitialiseReport(QueryUserActivity request)
        {
            if (request.UserId == null)
                request.UserId = _identityAccessor.User?.Id ?? throw new InvalidOperationException("No current user");

            var report = await _reportService.GetReportByName("User Activity Export", ReportProviderEnum.UserActivity);

            return _reportDesignService.RegisterReport(new ReportRequest
            {
                ReportName = report.Name,
                Design = report.Design,
                Query = request.ApplyCustomFilters()
            });
        }
    }
}
