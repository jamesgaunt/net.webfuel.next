using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    public interface ISupportTeamReferenceProvider: IReportReferenceProvider
    {
    }
    
    [Service(typeof(ISupportTeamReferenceProvider))]
    internal class SupportTeamReferenceProvider : ISupportTeamReferenceProvider
    {
        private readonly ISupportTeamRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public SupportTeamReferenceProvider(ISupportTeamRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<ReportReference?> GetReportReference(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            var item = staticData.SupportTeam.FirstOrDefault(x => x.Id == id);
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
            var result = await _repository.QuerySupportTeam(query);
            
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
