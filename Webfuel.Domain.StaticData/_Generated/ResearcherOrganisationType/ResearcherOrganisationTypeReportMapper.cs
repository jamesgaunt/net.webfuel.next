using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    [Service(typeof(IReportMapper<ResearcherOrganisationType>))]
    internal class ResearcherOrganisationTypeReportMapper : IReportMapper<ResearcherOrganisationType>
    {
        private readonly IResearcherOrganisationTypeRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public ResearcherOrganisationTypeReportMapper(IResearcherOrganisationTypeRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<object?> Get(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            return staticData.ResearcherOrganisationType.FirstOrDefault(x => x.Id == id);
        }
        
        public async Task<QueryResult<ReferenceLookup>> Lookup(Query query)
        {
            query.Contains(nameof(ResearcherOrganisationType.Name), query.Search);
            
            var result = await _repository.QueryResearcherOrganisationType(query);
            
            return new QueryResult<ReferenceLookup>
            {
                TotalCount = result.TotalCount,
                Items = result.Items.Select(p => new ReferenceLookup
                    {
                        Id = p.Id,
                        Name = p.Name
                    }).ToList()
            };
        }
        
        public Guid Id(object reference)
        {
            if (reference is not ResearcherOrganisationType entity)
            throw new Exception($"Cannot get id of type {reference.GetType()}");
            return entity.Id;
        }
        
        public string Name(object reference)
        {
            if (reference is not ResearcherOrganisationType entity)
            throw new Exception($"Cannot get name of type {reference.GetType()}");
            return entity.Name;
        }
    }
}
