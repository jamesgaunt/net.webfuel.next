using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateResearcherOrganisationType: IRequest<ResearcherOrganisationType>
    {
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class CreateResearcherOrganisationTypeHandler : IRequestHandler<CreateResearcherOrganisationType, ResearcherOrganisationType>
    {
        private readonly IResearcherOrganisationTypeRepository _researcherOrganisationTypeRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public CreateResearcherOrganisationTypeHandler(IResearcherOrganisationTypeRepository researcherOrganisationTypeRepository, IStaticDataCache staticDataCache)
        {
            _researcherOrganisationTypeRepository = researcherOrganisationTypeRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<ResearcherOrganisationType> Handle(CreateResearcherOrganisationType request, CancellationToken cancellationToken)
        {
            var updated = await _researcherOrganisationTypeRepository.InsertResearcherOrganisationType(new ResearcherOrganisationType {
                    Name = request.Name,
                    Default = request.Default,
                    Hidden = request.Hidden,
                    FreeText = request.FreeText,
                    SortOrder = await _researcherOrganisationTypeRepository.CountResearcherOrganisationType(),
                });
            
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

