using MediatR;

namespace Webfuel.Domain
{
    internal class CreateReportHandler : IRequestHandler<CreateReport, Report>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IIdentityAccessor _identityAccessor;

        public CreateReportHandler(IReportRepository reportRepository, IIdentityAccessor identityAccessor)
        {
            _reportRepository = reportRepository;
            _identityAccessor = identityAccessor;
        }

        public async Task<Report> Handle(CreateReport request, CancellationToken cancellationToken)
        {
            var report = new Report
            {
                Name = request.Name,
                ReportGroupId = request.ReportGroupId,
                ReportProviderId = request.ReportProviderId,
            };

            report.OwnerUserId = _identityAccessor.User?.Id ?? throw new DomainException("Unable to create report without identity context");
            return await _reportRepository.InsertReport(report);
        }
    }
}
