using MediatR;

namespace Webfuel.Domain
{
    internal class UpdateSupportRequestResearcherHandler : IRequestHandler<UpdateSupportRequestResearcher, SupportRequest>
    {
        private readonly IUpdateSupportRequestService _updateSupportRequestService;

        public UpdateSupportRequestResearcherHandler(IUpdateSupportRequestService updateSupportRequestService)
        {
            _updateSupportRequestService = updateSupportRequestService;
        }

        public async Task<SupportRequest> Handle(UpdateSupportRequestResearcher request, CancellationToken cancellationToken)
        {
            return await _updateSupportRequestService.UpdateSupportRequestResearcher(request);
        }
    }
}