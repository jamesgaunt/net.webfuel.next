using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class UpdateFundingStream: IRequest<FundingStream>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class UpdateFundingStreamHandler : IRequestHandler<UpdateFundingStream, FundingStream>
    {
        private readonly IFundingStreamRepository _fundingStreamRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public UpdateFundingStreamHandler(IFundingStreamRepository fundingStreamRepository, IStaticDataCache staticDataCache)
        {
            _fundingStreamRepository = fundingStreamRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<FundingStream> Handle(UpdateFundingStream request, CancellationToken cancellationToken)
        {
            var original = await _fundingStreamRepository.RequireFundingStream(request.Id);
            
            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Default = request.Default;
            updated.Hidden = request.Hidden;
            updated.FreeText = request.FreeText;
            
            updated = await _fundingStreamRepository.UpdateFundingStream(original: original, updated: updated);
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

