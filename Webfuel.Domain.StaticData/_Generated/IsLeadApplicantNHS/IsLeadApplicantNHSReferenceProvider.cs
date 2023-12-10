using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    public interface IIsLeadApplicantNHSReferenceProvider: IReportReferenceProvider
    {
    }
    
    [Service(typeof(IIsLeadApplicantNHSReferenceProvider))]
    internal class IsLeadApplicantNHSReferenceProvider : IIsLeadApplicantNHSReferenceProvider
    {
        private readonly IIsLeadApplicantNHSRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public IsLeadApplicantNHSReferenceProvider(IIsLeadApplicantNHSRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<ReportReference?> GetReportReference(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            var item = staticData.IsLeadApplicantNHS.FirstOrDefault(x => x.Id == id);
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
            var result = await _repository.QueryIsLeadApplicantNHS(query);
            
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
