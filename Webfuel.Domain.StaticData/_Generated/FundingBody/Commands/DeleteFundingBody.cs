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
        private readonly IStaticDataCache _staticDataCache;
        
        
        public DeleteFundingBodyHandler(IFundingBodyRepository fundingBodyRepository, IStaticDataCache staticDataCache)
        {
            _fundingBodyRepository = fundingBodyRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(DeleteFundingBody request, CancellationToken cancellationToken)
        {
            await _fundingBodyRepository.DeleteFundingBody(request.Id);
            _staticDataCache.FlushStaticData();
        }
    }
}

