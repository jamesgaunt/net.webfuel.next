using MediatR;

namespace Webfuel.Domain
{
    internal class CreateReportHandler : IRequestHandler<CreateReport, Report>
    {
        private readonly IReportRepository _reportRepository;

        public CreateReportHandler(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<Report> Handle(CreateReport request, CancellationToken cancellationToken)
        {
            return await _reportRepository.InsertReport(new Report { Name = request.Name });
        }
    }
}
