using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class SortWorkActivity: IRequest
    {
        public required IEnumerable<Guid> Ids { get; set; }
    }
    internal class SortWorkActivityHandler : IRequestHandler<SortWorkActivity>
    {
        private readonly IWorkActivityRepository _workActivityRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public SortWorkActivityHandler(IWorkActivityRepository workActivityRepository, IStaticDataCache staticDataCache)
        {
            _workActivityRepository = workActivityRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(SortWorkActivity request, CancellationToken cancellationToken)
        {
            var items = await _workActivityRepository.SelectWorkActivity();
            
            var index = 0;
            foreach (var id in request.Ids)
            {
                var original = items.FirstOrDefault(p => p.Id == id);
                if (original != null && original.SortOrder != index)
                {
                    var updated = original.Copy();
                    updated.SortOrder = index;
                    await _workActivityRepository.UpdateWorkActivity(updated: updated, original: original);
                }
                index++;
            }
            _staticDataCache.FlushStaticData();
        }
    }
}

