using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    public interface IFundingBodyReferenceProvider: IReportReferenceProvider
    {
    }
    
    [Service(typeof(IFundingBodyReferenceProvider))]
    internal class FundingBodyReferenceProvider : IFundingBodyReferenceProvider
    {
        private readonly IFundingBodyRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public FundingBodyReferenceProvider(IFundingBodyRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<ReportReference?> GetReportReference(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            var item = staticData.FundingBody.FirstOrDefault(x => x.Id == id);
            if (item == null)
            return null;
            
            return new ReportReference
            {
                Id = item.Id,
                Name = item.Name,
                Entity = item,
            };
        }
        
        public async Task<QueryResult<ReportReference>> QueryReportReference(Query query)
        {
            var result = await _repository.QueryFundingBody(query);
            
            return new QueryResult<ReportReference>
            {
                TotalCount = result.TotalCount,
                Items = result.Items.Select(x => new ReportReference
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Entity = x,
                    }).ToList(),
            };
        }
    }
}
