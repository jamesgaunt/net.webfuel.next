using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    public interface ISupportRequestStatusReferenceProvider: IReportReferenceProvider
    {
    }
    
    [Service(typeof(ISupportRequestStatusReferenceProvider))]
    internal class SupportRequestStatusReferenceProvider : ISupportRequestStatusReferenceProvider
    {
        private readonly ISupportRequestStatusRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public SupportRequestStatusReferenceProvider(ISupportRequestStatusRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<ReportReference?> GetReportReference(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            var item = staticData.SupportRequestStatus.FirstOrDefault(x => x.Id == id);
            if (item == null)
            return null;
            
            return new ReportReference
            {
                Id = item.Id,
                Name = item.Name,
            };
        }
        
        public async Task<QueryResult<ReportReference>> QueryReportReference(Query query)
        {
            var result = await _repository.QuerySupportRequestStatus(query);
            
            return new QueryResult<ReportReference>
            {
                TotalCount = result.TotalCount,
                Items = result.Items.Select(x => new ReportReference
                    {
                        Id = x.Id,
                        Name = x.Name,
                    }).ToList(),
            };
        }
    }
}
