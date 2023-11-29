using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public interface IReportService
    {
        Task<IReportSchema> GetReportSchema(Guid reportProviderId);

        Task<ReportProgress> InitialiseReport(ReportRequest request);
    }

    [Service(typeof(IReportService))]
    internal class ReportService: IReportService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IReportTaskService _reportTaskService;
        private readonly IReportRepository _reportRepository;

        public ReportService(IServiceProvider serviceProvider, IReportTaskService reportTaskService, IReportRepository reportRepository) 
        { 
            _serviceProvider = serviceProvider;
            _reportTaskService = reportTaskService;
            _reportRepository = reportRepository;
        }

        public Task<IReportSchema> GetReportSchema(Guid reportProviderId)
        {
            var provider = GetReportProvider(reportProviderId);
            return provider.GetReportSchema();
        }

        public async Task<ReportProgress> InitialiseReport(ReportRequest request)
        {
            var report = await _reportRepository.RequireReport(request.ReportId);
            var provider = GetReportProvider(report.ReportProviderId);
            return await provider.InitialiseReport(request);
        }

        IReportProvider GetReportProvider(Guid id)
        {
            var providers = _serviceProvider.GetServices<IReportProvider>();

            return providers.FirstOrDefault(p => p.Id == id)
                ?? throw new InvalidOperationException("The specified report provider does not exist.");
        }
    }
}
