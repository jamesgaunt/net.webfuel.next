using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteFundingBody: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteFundingBodyHandler : IRequestHandler<DeleteFundingBody>
    {
        private readonly IFundingBodyRepository _fundingBodyRepository;
        
        
        public DeleteFundingBodyHandler(IFundingBodyRepository fundingBodyRepository)
        {
            _fundingBodyRepository = fundingBodyRepository;
        }
        
        public async Task Handle(DeleteFundingBody request, CancellationToken cancellationToken)
        {
            await _fundingBodyRepository.DeleteFundingBody(request.Id);
        }
    }
}

