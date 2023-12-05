using MediatR;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    internal class RunReportHandler : IRequestHandler<RunReport, ReportStep>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IIdentityAccessor _identityAccessor;
        private readonly IReportProviderService _reportProviderService;

        public RunReportHandler(IReportRepository reportRepository, IIdentityAccessor identityAccessor, IReportProviderService reportProviderService)
        {
            _reportRepository = reportRepository;
            _identityAccessor = identityAccessor;
            _reportProviderService = reportProviderService;
        }

        public async Task<ReportStep> Handle(RunReport request, CancellationToken cancellationToken)
        {
            var report = await _reportRepository.RequireReport(request.ReportId);

            return await _reportProviderService.RegisterReport(new ReportRequest
            {
                Design = report.Design,
                Arguments = request.Arguments ?? new Dictionary<string, object?>()
            });
        }
    }
}
