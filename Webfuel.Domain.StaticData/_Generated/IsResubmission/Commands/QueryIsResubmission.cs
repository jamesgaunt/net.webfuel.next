using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryIsResubmission: Query, IRequest<QueryResult<IsResubmission>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(IsResubmission.Name), Search);
            return this;
        }
    }
    internal class QueryIsResubmissionHandler : IRequestHandler<QueryIsResubmission, QueryResult<IsResubmission>>
    {
        private readonly IIsResubmissionRepository _isResubmissionRepository;
        
        
        public QueryIsResubmissionHandler(IIsResubmissionRepository isResubmissionRepository)
        {
            _isResubmissionRepository = isResubmissionRepository;
        }
        
        public async Task<QueryResult<IsResubmission>> Handle(QueryIsResubmission request, CancellationToken cancellationToken)
        {
            return await _isResubmissionRepository.QueryIsResubmission(request.ApplyCustomFilters());
        }
    }
}

