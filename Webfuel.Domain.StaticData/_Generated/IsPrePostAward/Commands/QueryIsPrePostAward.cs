using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryIsPrePostAward: Query, IRequest<QueryResult<IsPrePostAward>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(IsPrePostAward.Name), Search);
            return this;
        }
    }
    internal class QueryIsPrePostAwardHandler : IRequestHandler<QueryIsPrePostAward, QueryResult<IsPrePostAward>>
    {
        private readonly IIsPrePostAwardRepository _isPrePostAwardRepository;
        
        
        public QueryIsPrePostAwardHandler(IIsPrePostAwardRepository isPrePostAwardRepository)
        {
            _isPrePostAwardRepository = isPrePostAwardRepository;
        }
        
        public async Task<QueryResult<IsPrePostAward>> Handle(QueryIsPrePostAward request, CancellationToken cancellationToken)
        {
            return await _isPrePostAwardRepository.QueryIsPrePostAward(request.ApplyCustomFilters());
        }
    }
}

