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
        private readonly IStaticDataCache _staticDataCache;
        
        
        public DeleteFundingStreamHandler(IFundingStreamRepository fundingStreamRepository, IStaticDataCache staticDataCache)
        {
            _fundingStreamRepository = fundingStreamRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(DeleteFundingStream request, CancellationToken cancellationToken)
        {
            await _fundingStreamRepository.DeleteFundingStream(request.Id);
            _staticDataCache.FlushStaticData();
        }
    }
}

