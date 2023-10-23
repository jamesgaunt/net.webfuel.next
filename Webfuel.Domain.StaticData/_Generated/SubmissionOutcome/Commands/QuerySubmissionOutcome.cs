using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QuerySubmissionOutcome: Query, IRequest<QueryResult<SubmissionOutcome>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(SubmissionOutcome.Name), Search);
            return this;
        }
    }
    internal class QuerySubmissionOutcomeHandler : IRequestHandler<QuerySubmissionOutcome, QueryResult<SubmissionOutcome>>
    {
        private readonly ISubmissionOutcomeRepository _submissionOutcomeRepository;
        
        
        public QuerySubmissionOutcomeHandler(ISubmissionOutcomeRepository submissionOutcomeRepository)
        {
            _submissionOutcomeRepository = submissionOutcomeRepository;
        }
        
        public async Task<QueryResult<SubmissionOutcome>> Handle(QuerySubmissionOutcome request, CancellationToken cancellationToken)
        {
            return await _submissionOutcomeRepository.QuerySubmissionOutcome(request.ApplyCustomFilters());
        }
    }
}

