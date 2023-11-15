using MediatR;

namespace Webfuel.Domain
{
    internal class UpdateSupportRequestStatusHandler : IRequestHandler<UpdateSupportRequestStatus, SupportRequest>
    {
        private readonly IUpdateSupportRequestService _updateSupportRequestService;

        public UpdateSupportRequestStatusHandler(IUpdateSupportRequestService updateSupportRequestService)
        {
            _updateSupportRequestService = updateSupportRequestService;
        }

        public async Task<SupportRequest> Handle(UpdateSupportRequestStatus request, CancellationToken cancellationToken)
        {
            return await _updateSupportRequestService.UpdateSupportRequestStatus(request);
        }
    }
}