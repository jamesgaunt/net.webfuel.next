using MediatR;

namespace Webfuel.Domain
{
    internal class DeleteReportHandler : IRequestHandler<DeleteReport>
    {
        private readonly IReportRepository _reportRepository;

        public DeleteReportHandler(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task Handle(DeleteReport request, CancellationToken cancellationToken)
        {
            await _reportRepository.DeleteReport(request.Id);
        }
    }
}
