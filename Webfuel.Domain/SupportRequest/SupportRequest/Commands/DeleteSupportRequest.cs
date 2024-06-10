using MediatR;

namespace Webfuel.Domain
{
    public class DeleteSupportRequest : IRequest
    {
        public required Guid Id { get; set; }
    }

    internal class DeleteSupportRequestHandler : IRequestHandler<DeleteSupportRequest>
    {
        private readonly ISupportRequestRepository _supportRequestRepository;

        public DeleteSupportRequestHandler(ISupportRequestRepository supportRequestRepository)
        {
            _supportRequestRepository = supportRequestRepository;
        }

        public async Task Handle(DeleteSupportRequest request, CancellationToken cancellationToken)
        {
            await _supportRequestRepository.DeleteSupportRequest(request.Id);
        }
    }
}
