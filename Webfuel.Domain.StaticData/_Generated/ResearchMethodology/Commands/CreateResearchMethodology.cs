using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateResearchMethodology: IRequest<ResearchMethodology>
    {
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class CreateResearchMethodologyHandler : IRequestHandler<CreateResearchMethodology, ResearchMethodology>
    {
        private readonly IResearchMethodologyRepository _researchMethodologyRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public CreateResearchMethodologyHandler(IResearchMethodologyRepository researchMethodologyRepository, IStaticDataCache staticDataCache)
        {
            _researchMethodologyRepository = researchMethodologyRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<ResearchMethodology> Handle(CreateResearchMethodology request, CancellationToken cancellationToken)
        {
            var updated = await _researchMethodologyRepository.InsertResearchMethodology(new ResearchMethodology {
                    Name = request.Name,
                    Default = request.Default,
                    Hidden = request.Hidden,
                    FreeText = request.FreeText,
                    SortOrder = await _researchMethodologyRepository.CountResearchMethodology(),
                });
            
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

