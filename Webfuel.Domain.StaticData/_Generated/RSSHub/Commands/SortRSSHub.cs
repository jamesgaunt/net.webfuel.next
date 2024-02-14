using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class SortRSSHub: IRequest
    {
        public required IEnumerable<Guid> Ids { get; set; }
    }
    internal class SortRSSHubHandler : IRequestHandler<SortRSSHub>
    {
        private readonly IRSSHubRepository _rsshubRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public SortRSSHubHandler(IRSSHubRepository rsshubRepository, IStaticDataCache staticDataCache)
        {
            _rsshubRepository = rsshubRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(SortRSSHub request, CancellationToken cancellationToken)
        {
            var items = await _rsshubRepository.SelectRSSHub();
            
            var index = 0;
            foreach (var id in request.Ids)
            {
                var original = items.FirstOrDefault(p => p.Id == id);
                if (original != null && original.SortOrder != index)
                {
                    var updated = original.Copy();
                    updated.SortOrder = index;
                    await _rsshubRepository.UpdateRSSHub(updated: updated, original: original);
                }
                index++;
            }
            _staticDataCache.FlushStaticData();
        }
    }
}

