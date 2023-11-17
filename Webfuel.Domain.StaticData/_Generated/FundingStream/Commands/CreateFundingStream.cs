using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateFundingStream: IRequest<FundingStream>
    {
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class CreateFundingStreamHandler : IRequestHandler<CreateFundingStream, FundingStream>
    {
        private readonly IFundingStreamRepository _fundingStreamRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public CreateFundingStreamHandler(IFundingStreamRepository fundingStreamRepository, IStaticDataCache staticDataCache)
        {
            _fundingStreamRepository = fundingStreamRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<FundingStream> Handle(CreateFundingStream request, CancellationToken cancellationToken)
        {
            var updated = await _fundingStreamRepository.InsertFundingStream(new FundingStream {
                    Name = request.Name,
                    Default = request.Default,
                    Hidden = request.Hidden,
                    FreeText = request.FreeText,
                    SortOrder = await _fundingStreamRepository.CountFundingStream(),
                });
            
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

