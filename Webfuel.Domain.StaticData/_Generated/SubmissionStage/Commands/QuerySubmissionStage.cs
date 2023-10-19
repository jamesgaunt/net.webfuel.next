using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QuerySubmissionStage: Query, IRequest<QueryResult<SubmissionStage>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(SubmissionStage.Name), Search);
            return this;
        }
    }
    internal class QuerySubmissionStageHandler : IRequestHandler<QuerySubmissionStage, QueryResult<SubmissionStage>>
    {
        private readonly ISubmissionStageRepository _submissionStageRepository;
        
        
        public QuerySubmissionStageHandler(ISubmissionStageRepository submissionStageRepository)
        {
            _submissionStageRepository = submissionStageRepository;
        }
        
        public async Task<QueryResult<SubmissionStage>> Handle(QuerySubmissionStage request, CancellationToken cancellationToken)
        {
            return await _submissionStageRepository.QuerySubmissionStage(request.ApplyCustomFilters());
        }
    }
}

