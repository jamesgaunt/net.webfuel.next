using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class SortResearcherLocation: IRequest
    {
        public required IEnumerable<Guid> Ids { get; set; }
    }
    internal class SortResearcherLocationHandler : IRequestHandler<SortResearcherLocation>
    {
        private readonly IResearcherLocationRepository _researcherLocationRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public SortResearcherLocationHandler(IResearcherLocationRepository researcherLocationRepository, IStaticDataCache staticDataCache)
        {
            _researcherLocationRepository = researcherLocationRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(SortResearcherLocation request, CancellationToken cancellationToken)
        {
            var items = await _researcherLocationRepository.SelectResearcherLocation();
            
            var index = 0;
            foreach (var id in request.Ids)
            {
                var original = items.FirstOrDefault(p => p.Id == id);
                if (original != null && original.SortOrder != index)
                {
                    var updated = original.Copy();
                    updated.SortOrder = index;
                    await _researcherLocationRepository.UpdateResearcherLocation(updated: updated, original: original);
                }
                index++;
            }
            _staticDataCache.FlushStaticData();
        }
    }
}

