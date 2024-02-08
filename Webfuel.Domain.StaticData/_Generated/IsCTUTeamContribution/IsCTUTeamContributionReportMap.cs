using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    [Service(typeof(IReportMap<IsCTUTeamContribution>))]
    internal class IsCTUTeamContributionReportMap : IReportMap<IsCTUTeamContribution>
    {
        private readonly IIsCTUTeamContributionRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public IsCTUTeamContributionReportMap(IIsCTUTeamContributionRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<object?> Get(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            return staticData.IsCTUTeamContribution.FirstOrDefault(x => x.Id == id);
        }
        
        public async Task<QueryResult<ReferenceLookup>> Query(Query query)
        {
            query.Contains(nameof(IsCTUTeamContribution.Name), query.Search);
            
            var result = await _repository.QueryIsCTUTeamContribution(query);
            
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
            if (reference is not IsCTUTeamContribution entity)
            throw new Exception($"Cannot get id of type {reference.GetType()}");
            return entity.Id;
        }
        
        public string Name(object reference)
        {
            if (reference is not IsCTUTeamContribution entity)
            throw new Exception($"Cannot get name of type {reference.GetType()}");
            return entity.Name;
        }
    }
}
