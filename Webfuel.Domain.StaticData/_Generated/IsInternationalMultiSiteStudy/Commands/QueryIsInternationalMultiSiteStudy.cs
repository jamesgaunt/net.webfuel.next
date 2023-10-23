using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryIsInternationalMultiSiteStudy: Query, IRequest<QueryResult<IsInternationalMultiSiteStudy>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(IsInternationalMultiSiteStudy.Name), Search);
            return this;
        }
    }
    internal class QueryIsInternationalMultiSiteStudyHandler : IRequestHandler<QueryIsInternationalMultiSiteStudy, QueryResult<IsInternationalMultiSiteStudy>>
    {
        private readonly IIsInternationalMultiSiteStudyRepository _isInternationalMultiSiteStudyRepository;
        
        
        public QueryIsInternationalMultiSiteStudyHandler(IIsInternationalMultiSiteStudyRepository isInternationalMultiSiteStudyRepository)
        {
            _isInternationalMultiSiteStudyRepository = isInternationalMultiSiteStudyRepository;
        }
        
        public async Task<QueryResult<IsInternationalMultiSiteStudy>> Handle(QueryIsInternationalMultiSiteStudy request, CancellationToken cancellationToken)
        {
            return await _isInternationalMultiSiteStudyRepository.QueryIsInternationalMultiSiteStudy(request.ApplyCustomFilters());
        }
    }
}

