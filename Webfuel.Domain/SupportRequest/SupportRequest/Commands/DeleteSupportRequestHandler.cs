using MediatR;

namespace Webfuel.Domain
{
    internal class DeleteSupportRequestHandler : IRequestHandler<DeleteSupportRequest>
    {
        private readonly IDeleteSupportRequestService _deleteSupportRequestService;

        public DeleteSupportRequestHandler(IDeleteSupportRequestService deleteSupportRequestService)
        {
            _deleteSupportRequestService = deleteSupportRequestService;
        }

        public async Task Handle(DeleteSupportRequest request, CancellationToken cancellationToken)
        {
            await _deleteSupportRequestService.DeleteSupportRequest(request);
        }
    }
}
