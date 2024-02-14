using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryWillStudyUseCTU: Query, IRequest<QueryResult<WillStudyUseCTU>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(WillStudyUseCTU.Name), Search);
            return this;
        }
    }
    internal class QueryWillStudyUseCTUHandler : IRequestHandler<QueryWillStudyUseCTU, QueryResult<WillStudyUseCTU>>
    {
        private readonly IWillStudyUseCTURepository _willStudyUseCTURepository;
        
        
        public QueryWillStudyUseCTUHandler(IWillStudyUseCTURepository willStudyUseCTURepository)
        {
            _willStudyUseCTURepository = willStudyUseCTURepository;
        }
        
        public async Task<QueryResult<WillStudyUseCTU>> Handle(QueryWillStudyUseCTU request, CancellationToken cancellationToken)
        {
            return await _willStudyUseCTURepository.QueryWillStudyUseCTU(request.ApplyCustomFilters());
        }
    }
}

