using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    public interface IIsPPIEAndEDIContributionReferenceProvider: IReportReferenceProvider
    {
    }
    
    [Service(typeof(IIsPPIEAndEDIContributionReferenceProvider))]
    internal class IsPPIEAndEDIContributionReferenceProvider : IIsPPIEAndEDIContributionReferenceProvider
    {
        private readonly IIsPPIEAndEDIContributionRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public IsPPIEAndEDIContributionReferenceProvider(IIsPPIEAndEDIContributionRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<ReportReference?> GetReportReference(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            var item = staticData.IsPPIEAndEDIContribution.FirstOrDefault(x => x.Id == id);
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
            var result = await _repository.QueryIsPPIEAndEDIContribution(query);
            
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
