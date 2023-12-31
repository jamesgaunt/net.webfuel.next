using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class SortAgeRange: IRequest
    {
        public required IEnumerable<Guid> Ids { get; set; }
    }
    internal class SortAgeRangeHandler : IRequestHandler<SortAgeRange>
    {
        private readonly IAgeRangeRepository _ageRangeRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public SortAgeRangeHandler(IAgeRangeRepository ageRangeRepository, IStaticDataCache staticDataCache)
        {
            _ageRangeRepository = ageRangeRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(SortAgeRange request, CancellationToken cancellationToken)
        {
            var items = await _ageRangeRepository.SelectAgeRange();
            
            var index = 0;
            foreach (var id in request.Ids)
            {
                var original = items.FirstOrDefault(p => p.Id == id);
                if (original != null && original.SortOrder != index)
                {
                    var updated = original.Copy();
                    updated.SortOrder = index;
                    await _ageRangeRepository.UpdateAgeRange(updated: updated, original: original);
                }
                index++;
            }
            _staticDataCache.FlushStaticData();
        }
    }
}

