using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteResearcherOrganisationType: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteResearcherOrganisationTypeHandler : IRequestHandler<DeleteResearcherOrganisationType>
    {
        private readonly IResearcherOrganisationTypeRepository _researcherOrganisationTypeRepository;
        
        
        public DeleteResearcherOrganisationTypeHandler(IResearcherOrganisationTypeRepository researcherOrganisationTypeRepository)
        {
            _researcherOrganisationTypeRepository = researcherOrganisationTypeRepository;
        }
        
        public async Task Handle(DeleteResearcherOrganisationType request, CancellationToken cancellationToken)
        {
            await _researcherOrganisationTypeRepository.DeleteResearcherOrganisationType(request.Id);
        }
    }
}

