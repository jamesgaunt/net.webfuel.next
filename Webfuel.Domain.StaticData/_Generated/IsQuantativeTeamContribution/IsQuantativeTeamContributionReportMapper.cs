using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    [Service(typeof(IReportMapper<IsQuantativeTeamContribution>))]
    internal class IsQuantativeTeamContributionReportMapper : IReportMapper<IsQuantativeTeamContribution>
    {
        private readonly IIsQuantativeTeamContributionRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public IsQuantativeTeamContributionReportMapper(IIsQuantativeTeamContributionRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<object?> Get(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            return staticData.IsQuantativeTeamContribution.FirstOrDefault(x => x.Id == id);
        }
        
        public async Task<QueryResult<object>> Query(Query query)
        {
            var result = await _repository.QueryIsQuantativeTeamContribution(query);
            
            return new QueryResult<object>
            {
                TotalCount = result.TotalCount,
                Items = result.Items
            };
        }
        
        public Guid Id(object reference)
        {
            if (reference is not IsQuantativeTeamContribution entity)
            throw new Exception($"Cannot get id of type {reference.GetType()}");
            return entity.Id;
        }
        
        public string DisplayName(object reference)
        {
            if (reference is not IsQuantativeTeamContribution entity)
            throw new Exception($"Cannot get display name of type {reference.GetType()}");
            return entity.Name;
        }
    }
}
