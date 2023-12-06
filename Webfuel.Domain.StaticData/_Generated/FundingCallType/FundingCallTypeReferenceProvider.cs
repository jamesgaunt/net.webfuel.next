using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    public interface IFundingCallTypeReferenceProvider: IReportReferenceProvider
    {
    }
    
    [Service(typeof(IFundingCallTypeReferenceProvider))]
    internal class FundingCallTypeReferenceProvider : IFundingCallTypeReferenceProvider
    {
        private readonly IFundingCallTypeRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public FundingCallTypeReferenceProvider(IFundingCallTypeRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<ReportReference?> GetReportReference(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            var item = staticData.FundingCallType.FirstOrDefault(x => x.Id == id);
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
            var result = await _repository.QueryFundingCallType(query);
            
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
