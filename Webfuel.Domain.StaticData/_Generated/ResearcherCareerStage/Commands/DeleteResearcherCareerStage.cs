using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteResearcherCareerStage: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteResearcherCareerStageHandler : IRequestHandler<DeleteResearcherCareerStage>
    {
        private readonly IResearcherCareerStageRepository _researcherCareerStageRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public DeleteResearcherCareerStageHandler(IResearcherCareerStageRepository researcherCareerStageRepository, IStaticDataCache staticDataCache)
        {
            _researcherCareerStageRepository = researcherCareerStageRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(DeleteResearcherCareerStage request, CancellationToken cancellationToken)
        {
            await _researcherCareerStageRepository.DeleteResearcherCareerStage(request.Id);
            _staticDataCache.FlushStaticData();
        }
    }
}

