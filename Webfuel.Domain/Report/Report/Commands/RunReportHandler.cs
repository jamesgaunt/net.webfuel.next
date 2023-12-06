using MediatR;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    internal class RunReportHandler : IRequestHandler<RunReport, ReportStep>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IIdentityAccessor _identityAccessor;
        private readonly IReportDesignService _reportDesignService;

        public RunReportHandler(IReportRepository reportRepository, IIdentityAccessor identityAccessor, IReportDesignService reportDesignService)
        {
            _reportRepository = reportRepository;
            _identityAccessor = identityAccessor;
            _reportDesignService = reportDesignService;
        }

        public async Task<ReportStep> Handle(RunReport request, CancellationToken cancellationToken)
        {
            var report = await _reportRepository.RequireReport(request.ReportId);

            return await _reportDesignService.RegisterReport(new ReportRequest
            {
                ProviderId = report.ReportProviderId,
                Design = report.Design,
                Arguments = request.Arguments ?? new Dictionary<string, object?>()
            });
        }
    }
}
