using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public interface IReportProviderService
    {
        Task<ReportStep> RegisterReport(ReportRequest request);
    }

    [Service(typeof(IReportProviderService))]
    internal class ReportProviderService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IReportGeneratorService _reportGeneratorService;

        public ReportProviderService(IServiceProvider serviceProvider, IReportGeneratorService reportGeneratorService)
        {
            _serviceProvider = serviceProvider;
            _reportGeneratorService = reportGeneratorService;   
        }

        public async Task<ReportStep> RegisterReport(ReportRequest request)
        {
            var provider = GetReportProvider(request.Design.ProviderId);
            var builder = await provider.InitialiseReport(request);
            return await _reportGeneratorService.RegisterReport(builder);
        }

        IReportProvider GetReportProvider(Guid id)
        {
            var providers = _serviceProvider.GetServices<IReportProvider>();

            return providers.FirstOrDefault(p => p.Id == id)
                ?? throw new InvalidOperationException("The specified report provider does not exist.");
        }
    }
}
