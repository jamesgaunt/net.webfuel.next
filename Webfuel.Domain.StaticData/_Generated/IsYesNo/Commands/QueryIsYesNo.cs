using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryIsYesNo: Query, IRequest<QueryResult<IsYesNo>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(IsYesNo.Name), Search);
            return this;
        }
    }
    internal class QueryIsYesNoHandler : IRequestHandler<QueryIsYesNo, QueryResult<IsYesNo>>
    {
        private readonly IIsYesNoRepository _isYesNoRepository;
        
        
        public QueryIsYesNoHandler(IIsYesNoRepository isYesNoRepository)
        {
            _isYesNoRepository = isYesNoRepository;
        }
        
        public async Task<QueryResult<IsYesNo>> Handle(QueryIsYesNo request, CancellationToken cancellationToken)
        {
            return await _isYesNoRepository.QueryIsYesNo(request.ApplyCustomFilters());
        }
    }
}

