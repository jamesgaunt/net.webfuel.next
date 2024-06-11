using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class SortResearcherCareerStage: IRequest
    {
        public required IEnumerable<Guid> Ids { get; set; }
    }
    internal class SortResearcherCareerStageHandler : IRequestHandler<SortResearcherCareerStage>
    {
        private readonly IResearcherCareerStageRepository _researcherCareerStageRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public SortResearcherCareerStageHandler(IResearcherCareerStageRepository researcherCareerStageRepository, IStaticDataCache staticDataCache)
        {
            _researcherCareerStageRepository = researcherCareerStageRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(SortResearcherCareerStage request, CancellationToken cancellationToken)
        {
            var items = await _researcherCareerStageRepository.SelectResearcherCareerStage();
            
            var index = 0;
            foreach (var id in request.Ids)
            {
                var original = items.FirstOrDefault(p => p.Id == id);
                if (original != null && original.SortOrder != index)
                {
                    var updated = original.Copy();
                    updated.SortOrder = index;
                    await _researcherCareerStageRepository.UpdateResearcherCareerStage(updated: updated, original: original);
                }
                index++;
            }
            _staticDataCache.FlushStaticData();
        }
    }
}

