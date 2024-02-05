using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    [Service(typeof(IReportMapper<IsInternationalMultiSiteStudy>))]
    internal class IsInternationalMultiSiteStudyReportMapper : IReportMapper<IsInternationalMultiSiteStudy>
    {
        private readonly IIsInternationalMultiSiteStudyRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public IsInternationalMultiSiteStudyReportMapper(IIsInternationalMultiSiteStudyRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<object?> Get(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            return staticData.IsInternationalMultiSiteStudy.FirstOrDefault(x => x.Id == id);
        }
        
        public async Task<QueryResult<object>> Query(Query query)
        {
            query.Contains(nameof(IsInternationalMultiSiteStudy.Name), query.Search);
            
            var result = await _repository.QueryIsInternationalMultiSiteStudy(query);
            
            return new QueryResult<object>
            {
                TotalCount = result.TotalCount,
                Items = result.Items
            };
        }
        
        public Guid Id(object reference)
        {
            if (reference is not IsInternationalMultiSiteStudy entity)
            throw new Exception($"Cannot get id of type {reference.GetType()}");
            return entity.Id;
        }
        
        public string DisplayName(object reference)
        {
            if (reference is not IsInternationalMultiSiteStudy entity)
            throw new Exception($"Cannot get display name of type {reference.GetType()}");
            return entity.Name;
        }
    }
}
