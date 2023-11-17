using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class SortFundingStream: IRequest
    {
        public required IEnumerable<Guid> Ids { get; set; }
    }
    internal class SortFundingStreamHandler : IRequestHandler<SortFundingStream>
    {
        private readonly IFundingStreamRepository _fundingStreamRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public SortFundingStreamHandler(IFundingStreamRepository fundingStreamRepository, IStaticDataCache staticDataCache)
        {
            _fundingStreamRepository = fundingStreamRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(SortFundingStream request, CancellationToken cancellationToken)
        {
            var items = await _fundingStreamRepository.SelectFundingStream();
            
            var index = 0;
            foreach (var id in request.Ids)
            {
                var original = items.FirstOrDefault(p => p.Id == id);
                if (original != null && original.SortOrder != index)
                {
                    var updated = original.Copy();
                    updated.SortOrder = index;
                    await _fundingStreamRepository.UpdateFundingStream(updated: updated, original: original);
                }
                index++;
            }
            _staticDataCache.FlushStaticData();
        }
    }
}

