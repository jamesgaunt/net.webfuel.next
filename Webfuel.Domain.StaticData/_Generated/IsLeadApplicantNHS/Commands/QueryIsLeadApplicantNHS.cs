using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryIsLeadApplicantNHS: Query, IRequest<QueryResult<IsLeadApplicantNHS>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(IsLeadApplicantNHS.Name), Search);
            return this;
        }
    }
    internal class QueryIsLeadApplicantNHSHandler : IRequestHandler<QueryIsLeadApplicantNHS, QueryResult<IsLeadApplicantNHS>>
    {
        private readonly IIsLeadApplicantNHSRepository _isLeadApplicantNHSRepository;
        
        
        public QueryIsLeadApplicantNHSHandler(IIsLeadApplicantNHSRepository isLeadApplicantNHSRepository)
        {
            _isLeadApplicantNHSRepository = isLeadApplicantNHSRepository;
        }
        
        public async Task<QueryResult<IsLeadApplicantNHS>> Handle(QueryIsLeadApplicantNHS request, CancellationToken cancellationToken)
        {
            return await _isLeadApplicantNHSRepository.QueryIsLeadApplicantNHS(request.ApplyCustomFilters());
        }
    }
}

