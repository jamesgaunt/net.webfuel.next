using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class SortResearcherOrganisationType: IRequest
    {
        public required IEnumerable<Guid> Ids { get; set; }
    }
    internal class SortResearcherOrganisationTypeHandler : IRequestHandler<SortResearcherOrganisationType>
    {
        private readonly IResearcherOrganisationTypeRepository _researcherOrganisationTypeRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public SortResearcherOrganisationTypeHandler(IResearcherOrganisationTypeRepository researcherOrganisationTypeRepository, IStaticDataCache staticDataCache)
        {
            _researcherOrganisationTypeRepository = researcherOrganisationTypeRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(SortResearcherOrganisationType request, CancellationToken cancellationToken)
        {
            var items = await _researcherOrganisationTypeRepository.SelectResearcherOrganisationType();
            
            var index = 0;
            foreach (var id in request.Ids)
            {
                var original = items.FirstOrDefault(p => p.Id == id);
                if (original != null && original.SortOrder != index)
                {
                    var updated = original.Copy();
                    updated.SortOrder = index;
                    await _researcherOrganisationTypeRepository.UpdateResearcherOrganisationType(updated: updated, original: original);
                }
                index++;
            }
            _staticDataCache.FlushStaticData();
        }
    }
}

