using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class SortSupportProvided: IRequest
    {
        public required IEnumerable<Guid> Ids { get; set; }
    }
    internal class SortSupportProvidedHandler : IRequestHandler<SortSupportProvided>
    {
        private readonly ISupportProvidedRepository _supportProvidedRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public SortSupportProvidedHandler(ISupportProvidedRepository supportProvidedRepository, IStaticDataCache staticDataCache)
        {
            _supportProvidedRepository = supportProvidedRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(SortSupportProvided request, CancellationToken cancellationToken)
        {
            var items = await _supportProvidedRepository.SelectSupportProvided();
            
            var index = 0;
            foreach (var id in request.Ids)
            {
                var original = items.FirstOrDefault(p => p.Id == id);
                if (original != null && original.SortOrder != index)
                {
                    var updated = original.Copy();
                    updated.SortOrder = index;
                    await _supportProvidedRepository.UpdateSupportProvided(updated: updated, original: original);
                }
                index++;
            }
            _staticDataCache.FlushStaticData();
        }
    }
}

