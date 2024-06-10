using FluentValidation;
using MediatR;

namespace Webfuel.Domain
{
    public class GetSupportRequest : IRequest<SupportRequest?>
    {
        public Guid Id { get; set; }
    }

    internal class GetSupportRequestHandler : IRequestHandler<GetSupportRequest, SupportRequest?>
    {
        private readonly ISupportRequestRepository _supportRequestRepository;

        public GetSupportRequestHandler(ISupportRequestRepository supportRequestRepository)
        {
            _supportRequestRepository = supportRequestRepository;
        }

        public async Task<SupportRequest?> Handle(GetSupportRequest request, CancellationToken cancellationToken)
        {
            return await _supportRequestRepository.GetSupportRequest(request.Id);
        }
    }
}
