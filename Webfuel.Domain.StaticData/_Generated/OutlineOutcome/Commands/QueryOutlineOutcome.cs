using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryOutlineOutcome: Query, IRequest<QueryResult<OutlineOutcome>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(OutlineOutcome.Name), Search);
            return this;
        }
    }
    internal class QueryOutlineOutcomeHandler : IRequestHandler<QueryOutlineOutcome, QueryResult<OutlineOutcome>>
    {
        private readonly IOutlineOutcomeRepository _outlineOutcomeRepository;
        
        
        public QueryOutlineOutcomeHandler(IOutlineOutcomeRepository outlineOutcomeRepository)
        {
            _outlineOutcomeRepository = outlineOutcomeRepository;
        }
        
        public async Task<QueryResult<OutlineOutcome>> Handle(QueryOutlineOutcome request, CancellationToken cancellationToken)
        {
            return await _outlineOutcomeRepository.QueryOutlineOutcome(request.ApplyCustomFilters());
        }
    }
}

