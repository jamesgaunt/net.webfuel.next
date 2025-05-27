using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryFullOutcome: Query, IRequest<QueryResult<FullOutcome>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(FullOutcome.Name), Search);
            return this;
        }
    }
    internal class QueryFullOutcomeHandler : IRequestHandler<QueryFullOutcome, QueryResult<FullOutcome>>
    {
        private readonly IFullOutcomeRepository _fullOutcomeRepository;
        
        
        public QueryFullOutcomeHandler(IFullOutcomeRepository fullOutcomeRepository)
        {
            _fullOutcomeRepository = fullOutcomeRepository;
        }
        
        public async Task<QueryResult<FullOutcome>> Handle(QueryFullOutcome request, CancellationToken cancellationToken)
        {
            return await _fullOutcomeRepository.QueryFullOutcome(request.ApplyCustomFilters());
        }
    }
}

