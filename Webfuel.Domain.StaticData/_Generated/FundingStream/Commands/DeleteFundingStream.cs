using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteFundingStream: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteFundingStreamHandler : IRequestHandler<DeleteFundingStream>
    {
        private readonly IFundingStreamRepository _fundingStreamRepository;
        
        
        public DeleteFundingStreamHandler(IFundingStreamRepository fundingStreamRepository)
        {
            _fundingStreamRepository = fundingStreamRepository;
        }
        
        public async Task Handle(DeleteFundingStream request, CancellationToken cancellationToken)
        {
            await _fundingStreamRepository.DeleteFundingStream(request.Id);
        }
    }
}

