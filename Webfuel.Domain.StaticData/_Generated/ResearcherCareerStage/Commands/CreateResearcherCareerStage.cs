using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateResearcherCareerStage: IRequest<ResearcherCareerStage>
    {
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
    }
    internal class CreateResearcherCareerStageHandler : IRequestHandler<CreateResearcherCareerStage, ResearcherCareerStage>
    {
        private readonly IResearcherCareerStageRepository _researcherCareerStageRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public CreateResearcherCareerStageHandler(IResearcherCareerStageRepository researcherCareerStageRepository, IStaticDataCache staticDataCache)
        {
            _researcherCareerStageRepository = researcherCareerStageRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<ResearcherCareerStage> Handle(CreateResearcherCareerStage request, CancellationToken cancellationToken)
        {
            var updated = await _researcherCareerStageRepository.InsertResearcherCareerStage(new ResearcherCareerStage {
                    Name = request.Name,
                    Default = request.Default,
                    SortOrder = await _researcherCareerStageRepository.CountResearcherCareerStage(),
                });
            
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

