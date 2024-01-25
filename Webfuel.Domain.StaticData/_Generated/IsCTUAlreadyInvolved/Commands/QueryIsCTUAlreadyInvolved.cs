using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryIsCTUAlreadyInvolved: Query, IRequest<QueryResult<IsCTUAlreadyInvolved>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(IsCTUAlreadyInvolved.Name), Search);
            return this;
        }
    }
    internal class QueryIsCTUAlreadyInvolvedHandler : IRequestHandler<QueryIsCTUAlreadyInvolved, QueryResult<IsCTUAlreadyInvolved>>
    {
        private readonly IIsCTUAlreadyInvolvedRepository _isCTUAlreadyInvolvedRepository;
        
        
        public QueryIsCTUAlreadyInvolvedHandler(IIsCTUAlreadyInvolvedRepository isCTUAlreadyInvolvedRepository)
        {
            _isCTUAlreadyInvolvedRepository = isCTUAlreadyInvolvedRepository;
        }
        
        public async Task<QueryResult<IsCTUAlreadyInvolved>> Handle(QueryIsCTUAlreadyInvolved request, CancellationToken cancellationToken)
        {
            return await _isCTUAlreadyInvolvedRepository.QueryIsCTUAlreadyInvolved(request.ApplyCustomFilters());
        }
    }
}

