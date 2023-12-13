using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    [Service(typeof(IReportMapper<IsLeadApplicantNHS>))]
    internal class IsLeadApplicantNHSReportMapper : IReportMapper<IsLeadApplicantNHS>
    {
        private readonly IIsLeadApplicantNHSRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public IsLeadApplicantNHSReportMapper(IIsLeadApplicantNHSRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<object?> Get(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            return staticData.IsLeadApplicantNHS.FirstOrDefault(x => x.Id == id);
        }
        
        public async Task<QueryResult<object>> Query(Query query)
        {
            var result = await _repository.QueryIsLeadApplicantNHS(query);
            
            return new QueryResult<object>
            {
                TotalCount = result.TotalCount,
                Items = result.Items
            };
        }
        
        public Guid Id(object reference)
        {
            if (reference is not IsLeadApplicantNHS entity)
            throw new Exception($"Cannot get id of type {reference.GetType()}");
            return entity.Id;
        }
        
        public string DisplayName(object reference)
        {
            if (reference is not IsLeadApplicantNHS entity)
            throw new Exception($"Cannot get display name of type {reference.GetType()}");
            return entity.Name;
        }
    }
}
