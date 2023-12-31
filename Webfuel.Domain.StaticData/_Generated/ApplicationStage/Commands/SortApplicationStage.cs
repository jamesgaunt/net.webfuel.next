using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class SortApplicationStage: IRequest
    {
        public required IEnumerable<Guid> Ids { get; set; }
    }
    internal class SortApplicationStageHandler : IRequestHandler<SortApplicationStage>
    {
        private readonly IApplicationStageRepository _applicationStageRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public SortApplicationStageHandler(IApplicationStageRepository applicationStageRepository, IStaticDataCache staticDataCache)
        {
            _applicationStageRepository = applicationStageRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(SortApplicationStage request, CancellationToken cancellationToken)
        {
            var items = await _applicationStageRepository.SelectApplicationStage();
            
            var index = 0;
            foreach (var id in request.Ids)
            {
                var original = items.FirstOrDefault(p => p.Id == id);
                if (original != null && original.SortOrder != index)
                {
                    var updated = original.Copy();
                    updated.SortOrder = index;
                    await _applicationStageRepository.UpdateApplicationStage(updated: updated, original: original);
                }
                index++;
            }
            _staticDataCache.FlushStaticData();
        }
    }
}

