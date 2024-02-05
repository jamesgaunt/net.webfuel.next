using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    [Service(typeof(IReportMapper<IsResubmission>))]
    internal class IsResubmissionReportMapper : IReportMapper<IsResubmission>
    {
        private readonly IIsResubmissionRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public IsResubmissionReportMapper(IIsResubmissionRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<object?> Get(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            return staticData.IsResubmission.FirstOrDefault(x => x.Id == id);
        }
        
        public async Task<QueryResult<object>> Query(Query query)
        {
            query.Contains(nameof(IsResubmission.Name), query.Search);
            
            var result = await _repository.QueryIsResubmission(query);
            
            return new QueryResult<object>
            {
                TotalCount = result.TotalCount,
                Items = result.Items
            };
        }
        
        public Guid Id(object reference)
        {
            if (reference is not IsResubmission entity)
            throw new Exception($"Cannot get id of type {reference.GetType()}");
            return entity.Id;
        }
        
        public string DisplayName(object reference)
        {
            if (reference is not IsResubmission entity)
            throw new Exception($"Cannot get display name of type {reference.GetType()}");
            return entity.Name;
        }
    }
}
