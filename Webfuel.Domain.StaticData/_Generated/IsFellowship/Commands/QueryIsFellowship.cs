using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryIsFellowship: Query, IRequest<QueryResult<IsFellowship>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(IsFellowship.Name), Search);
            return this;
        }
    }
    internal class QueryIsFellowshipHandler : IRequestHandler<QueryIsFellowship, QueryResult<IsFellowship>>
    {
        private readonly IIsFellowshipRepository _isFellowshipRepository;
        
        
        public QueryIsFellowshipHandler(IIsFellowshipRepository isFellowshipRepository)
        {
            _isFellowshipRepository = isFellowshipRepository;
        }
        
        public async Task<QueryResult<IsFellowship>> Handle(QueryIsFellowship request, CancellationToken cancellationToken)
        {
            return await _isFellowshipRepository.QueryIsFellowship(request.ApplyCustomFilters());
        }
    }
}

