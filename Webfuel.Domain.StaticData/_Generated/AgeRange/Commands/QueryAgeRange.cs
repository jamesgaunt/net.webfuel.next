using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryAgeRange: Query, IRequest<QueryResult<AgeRange>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(AgeRange.Name), Search);
            return this;
        }
    }
    internal class QueryAgeRangeHandler : IRequestHandler<QueryAgeRange, QueryResult<AgeRange>>
    {
        private readonly IAgeRangeRepository _ageRangeRepository;
        
        
        public QueryAgeRangeHandler(IAgeRangeRepository ageRangeRepository)
        {
            _ageRangeRepository = ageRangeRepository;
        }
        
        public async Task<QueryResult<AgeRange>> Handle(QueryAgeRange request, CancellationToken cancellationToken)
        {
            return await _ageRangeRepository.QueryAgeRange(request.ApplyCustomFilters());
        }
    }
}

