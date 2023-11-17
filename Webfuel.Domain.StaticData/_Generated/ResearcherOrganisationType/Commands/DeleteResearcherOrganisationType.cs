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
        private readonly IStaticDataCache _staticDataCache;
        
        
        public DeleteResearcherOrganisationTypeHandler(IResearcherOrganisationTypeRepository researcherOrganisationTypeRepository, IStaticDataCache staticDataCache)
        {
            _researcherOrganisationTypeRepository = researcherOrganisationTypeRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(DeleteResearcherOrganisationType request, CancellationToken cancellationToken)
        {
            await _researcherOrganisationTypeRepository.DeleteResearcherOrganisationType(request.Id);
            _staticDataCache.FlushStaticData();
        }
    }
}

