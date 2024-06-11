using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class UpdateResearcherLocation: IRequest<ResearcherLocation>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class UpdateResearcherLocationHandler : IRequestHandler<UpdateResearcherLocation, ResearcherLocation>
    {
        private readonly IResearcherLocationRepository _researcherLocationRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public UpdateResearcherLocationHandler(IResearcherLocationRepository researcherLocationRepository, IStaticDataCache staticDataCache)
        {
            _researcherLocationRepository = researcherLocationRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<ResearcherLocation> Handle(UpdateResearcherLocation request, CancellationToken cancellationToken)
        {
            var original = await _researcherLocationRepository.RequireResearcherLocation(request.Id);
            
            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Default = request.Default;
            updated.Hidden = request.Hidden;
            updated.FreeText = request.FreeText;
            
            updated = await _researcherLocationRepository.UpdateResearcherLocation(original: original, updated: updated);
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

