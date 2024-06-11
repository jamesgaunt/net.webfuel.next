using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateResearcherLocation: IRequest<ResearcherLocation>
    {
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class CreateResearcherLocationHandler : IRequestHandler<CreateResearcherLocation, ResearcherLocation>
    {
        private readonly IResearcherLocationRepository _researcherLocationRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public CreateResearcherLocationHandler(IResearcherLocationRepository researcherLocationRepository, IStaticDataCache staticDataCache)
        {
            _researcherLocationRepository = researcherLocationRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<ResearcherLocation> Handle(CreateResearcherLocation request, CancellationToken cancellationToken)
        {
            var updated = await _researcherLocationRepository.InsertResearcherLocation(new ResearcherLocation {
                    Name = request.Name,
                    Default = request.Default,
                    Hidden = request.Hidden,
                    FreeText = request.FreeText,
                    SortOrder = await _researcherLocationRepository.CountResearcherLocation(),
                });
            
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

