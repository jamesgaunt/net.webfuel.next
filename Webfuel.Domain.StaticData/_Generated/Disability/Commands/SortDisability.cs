using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class SortDisability: IRequest
    {
        public required IEnumerable<Guid> Ids { get; set; }
    }
    internal class SortDisabilityHandler : IRequestHandler<SortDisability>
    {
        private readonly IDisabilityRepository _disabilityRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public SortDisabilityHandler(IDisabilityRepository disabilityRepository, IStaticDataCache staticDataCache)
        {
            _disabilityRepository = disabilityRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(SortDisability request, CancellationToken cancellationToken)
        {
            var items = await _disabilityRepository.SelectDisability();
            
            var index = 0;
            foreach (var id in request.Ids)
            {
                var original = items.FirstOrDefault(p => p.Id == id);
                if (original != null && original.SortOrder != index)
                {
                    var updated = original.Copy();
                    updated.SortOrder = index;
                    await _disabilityRepository.UpdateDisability(updated: updated, original: original);
                }
                index++;
            }
            _staticDataCache.FlushStaticData();
        }
    }
}

