using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    public interface IFundingStreamReferenceProvider: IReportReferenceProvider
    {
    }
    
    [Service(typeof(IFundingStreamReferenceProvider))]
    internal class FundingStreamReferenceProvider : IFundingStreamReferenceProvider
    {
        private readonly IFundingStreamRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public FundingStreamReferenceProvider(IFundingStreamRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<ReportReference?> GetReportReference(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            var item = staticData.FundingStream.FirstOrDefault(x => x.Id == id);
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
            var result = await _repository.QueryFundingStream(query);
            
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
