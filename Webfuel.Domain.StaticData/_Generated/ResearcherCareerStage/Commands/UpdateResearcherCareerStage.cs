using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class UpdateResearcherCareerStage: IRequest<ResearcherCareerStage>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
    }
    internal class UpdateResearcherCareerStageHandler : IRequestHandler<UpdateResearcherCareerStage, ResearcherCareerStage>
    {
        private readonly IResearcherCareerStageRepository _researcherCareerStageRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public UpdateResearcherCareerStageHandler(IResearcherCareerStageRepository researcherCareerStageRepository, IStaticDataCache staticDataCache)
        {
            _researcherCareerStageRepository = researcherCareerStageRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<ResearcherCareerStage> Handle(UpdateResearcherCareerStage request, CancellationToken cancellationToken)
        {
            var original = await _researcherCareerStageRepository.RequireResearcherCareerStage(request.Id);
            
            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Default = request.Default;
            
            updated = await _researcherCareerStageRepository.UpdateResearcherCareerStage(original: original, updated: updated);
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

