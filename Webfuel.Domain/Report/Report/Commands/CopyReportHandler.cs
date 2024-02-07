using MediatR;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    internal class CopyReportHandler : IRequestHandler<CopyReport, Report>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IIdentityAccessor _identityAccessor;

        public CopyReportHandler(IReportRepository reportRepository, IIdentityAccessor identityAccessor)
        {
            _reportRepository = reportRepository;
            _identityAccessor = identityAccessor;
        }

        public async Task<Report> Handle(CopyReport request, CancellationToken cancellationToken)
        {
            var original = await _reportRepository.GetReport(request.Id);
            if (original == null)
                throw new DomainException("The specified report does not exist.");

            var report = new Report
            {
                Name = request.Name,
                ReportGroupId = original.ReportGroupId,
                ReportProviderId = original.ReportProviderId,
                Design = original.Design,
            };

            report.OwnerUserId = _identityAccessor.User?.Id ?? throw new DomainException("Unable to copy report without identity context");
            return await _reportRepository.InsertReport(report);
        }
    }
}
