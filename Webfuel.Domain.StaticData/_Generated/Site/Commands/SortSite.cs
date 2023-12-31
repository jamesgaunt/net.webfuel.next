using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class SortSite: IRequest
    {
        public required IEnumerable<Guid> Ids { get; set; }
    }
    internal class SortSiteHandler : IRequestHandler<SortSite>
    {
        private readonly ISiteRepository _siteRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public SortSiteHandler(ISiteRepository siteRepository, IStaticDataCache staticDataCache)
        {
            _siteRepository = siteRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(SortSite request, CancellationToken cancellationToken)
        {
            var items = await _siteRepository.SelectSite();
            
            var index = 0;
            foreach (var id in request.Ids)
            {
                var original = items.FirstOrDefault(p => p.Id == id);
                if (original != null && original.SortOrder != index)
                {
                    var updated = original.Copy();
                    updated.SortOrder = index;
                    await _siteRepository.UpdateSite(updated: updated, original: original);
                }
                index++;
            }
            _staticDataCache.FlushStaticData();
        }
    }
}

