using MediatR;

namespace Webfuel.Domain
{
    internal class UpdateSupportRequestHandler : IRequestHandler<UpdateSupportRequest, SupportRequest>
    {
        private readonly IUpdateSupportRequestService _updateSupportRequestService;

        public UpdateSupportRequestHandler(IUpdateSupportRequestService updateSupportRequestService)
        {
            _updateSupportRequestService = updateSupportRequestService;
        }

        public async Task<SupportRequest> Handle(UpdateSupportRequest request, CancellationToken cancellationToken)
        {
            return await _updateSupportRequestService.UpdateSupportRequest(request);
        }
    }
}