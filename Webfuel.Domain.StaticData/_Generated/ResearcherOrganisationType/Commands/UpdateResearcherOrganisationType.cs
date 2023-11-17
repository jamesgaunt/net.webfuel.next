using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class UpdateResearcherOrganisationType: IRequest<ResearcherOrganisationType>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class UpdateResearcherOrganisationTypeHandler : IRequestHandler<UpdateResearcherOrganisationType, ResearcherOrganisationType>
    {
        private readonly IResearcherOrganisationTypeRepository _researcherOrganisationTypeRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public UpdateResearcherOrganisationTypeHandler(IResearcherOrganisationTypeRepository researcherOrganisationTypeRepository, IStaticDataCache staticDataCache)
        {
            _researcherOrganisationTypeRepository = researcherOrganisationTypeRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<ResearcherOrganisationType> Handle(UpdateResearcherOrganisationType request, CancellationToken cancellationToken)
        {
            var original = await _researcherOrganisationTypeRepository.RequireResearcherOrganisationType(request.Id);
            
            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Default = request.Default;
            updated.Hidden = request.Hidden;
            updated.FreeText = request.FreeText;
            
            updated = await _researcherOrganisationTypeRepository.UpdateResearcherOrganisationType(original: original, updated: updated);
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

