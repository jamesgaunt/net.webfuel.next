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
        
        
        public CreateResearcherOrganisationTypeHandler(IResearcherOrganisationTypeRepository researcherOrganisationTypeRepository)
        {
            _researcherOrganisationTypeRepository = researcherOrganisationTypeRepository;
        }
        
        public async Task<ResearcherOrganisationType> Handle(CreateResearcherOrganisationType request, CancellationToken cancellationToken)
        {
            return await _researcherOrganisationTypeRepository.InsertResearcherOrganisationType(new ResearcherOrganisationType {
                    Name = request.Name,
                    Default = request.Default,
                    Hidden = request.Hidden,
                    FreeText = request.FreeText,
                    SortOrder = await _researcherOrganisationTypeRepository.CountResearcherOrganisationType(),
                });
        }
    }
}

