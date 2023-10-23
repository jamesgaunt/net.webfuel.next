using MediatR;

namespace Webfuel.Domain
{
    internal class CreateSupportRequestHandler : IRequestHandler<CreateSupportRequest, SupportRequest>
    {
        private readonly ICreateSupportRequestService _createSupportRequestService;

        public CreateSupportRequestHandler(ICreateSupportRequestService createSupportRequestService)
        {
            _createSupportRequestService = createSupportRequestService;
        }

        public async Task<SupportRequest> Handle(CreateSupportRequest request, CancellationToken cancellationToken)
        {
            return await _createSupportRequestService.CreateSupportRequest(request);
        }
    }
}