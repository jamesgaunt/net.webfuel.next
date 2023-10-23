using MediatR;

namespace Webfuel.Domain
{
    internal class TriageSupportRequestHandler : IRequestHandler<TriageSupportRequest, Project?>
    {
        private readonly ITriageSupportRequestService _triageSupportRequestService;

        public TriageSupportRequestHandler(ITriageSupportRequestService triageSupportRequestService)
        {
            _triageSupportRequestService = triageSupportRequestService;
        }

        public async Task<Project?> Handle(TriageSupportRequest request, CancellationToken cancellationToken)
        {
            return await _triageSupportRequestService.TriageSupportRequest(request);
        }
    }
}