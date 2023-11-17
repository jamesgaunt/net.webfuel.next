using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class SortFundingBody: IRequest
    {
        public required IEnumerable<Guid> Ids { get; set; }
    }
    internal class SortFundingBodyHandler : IRequestHandler<SortFundingBody>
    {
        private readonly IFundingBodyRepository _fundingBodyRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public SortFundingBodyHandler(IFundingBodyRepository fundingBodyRepository, IStaticDataCache staticDataCache)
        {
            _fundingBodyRepository = fundingBodyRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(SortFundingBody request, CancellationToken cancellationToken)
        {
            var items = await _fundingBodyRepository.SelectFundingBody();
            
            var index = 0;
            foreach (var id in request.Ids)
            {
                var original = items.FirstOrDefault(p => p.Id == id);
                if (original != null && original.SortOrder != index)
                {
                    var updated = original.Copy();
                    updated.SortOrder = index;
                    await _fundingBodyRepository.UpdateFundingBody(updated: updated, original: original);
                }
                index++;
            }
            _staticDataCache.FlushStaticData();
        }
    }
}

