using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    [Service(typeof(IReportReferenceProvider<SupportRequestStatus>))]
    internal class SupportRequestStatusReportReferenceProvider : IReportReferenceProvider<SupportRequestStatus>
    {
        private readonly ISupportRequestStatusRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public SupportRequestStatusReportReferenceProvider(ISupportRequestStatusRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<ReportReference?> Get(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            var entity = staticData.SupportRequestStatus.FirstOrDefault(x => x.Id == id);
            
            return entity == null ? null : new ReportReference
            {
                Id = entity.Id,
                Name = entity.Name,
                Entity = entity
            };
        }
        
        public async Task<QueryResult<ReportReference>> Query(Query query)
        {
            var result = await _repository.QuerySupportRequestStatus(query);
            
            return new QueryResult<ReportReference>
            {
                TotalCount = result.TotalCount,
                Items = result.Items.Select(x => new ReportReference
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Entity = x
                    }).ToList()
            };
        }
    }
}
