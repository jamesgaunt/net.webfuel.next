using FluentValidation;
using MediatR;

namespace Webfuel.Domain
{

    internal class GetReportHandler : IRequestHandler<GetReport, Report?>
    {
        private readonly IReportRepository _reportRepository;

        public GetReportHandler(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<Report?> Handle(GetReport request, CancellationToken cancellationToken)
        {
            return await _reportRepository.GetReport(request.Id);
        }
    }
}
