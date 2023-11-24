using MediatR;

namespace Webfuel.Domain
{
    internal class DeleteReportGroupHandler : IRequestHandler<DeleteReportGroup>
    {
        private readonly IReportGroupRepository _reportGroupRepository;

        public DeleteReportGroupHandler(IReportGroupRepository reportGroupRepository)
        {
            _reportGroupRepository = reportGroupRepository;
        }

        public async Task Handle(DeleteReportGroup request, CancellationToken cancellationToken)
        {
            await _reportGroupRepository.DeleteReportGroup(request.Id);
        }
    }
}
