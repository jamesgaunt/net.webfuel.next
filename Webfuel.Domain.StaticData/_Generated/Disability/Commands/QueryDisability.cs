using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryDisability: Query, IRequest<QueryResult<Disability>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(Disability.Name), Search);
            return this;
        }
    }
    internal class QueryDisabilityHandler : IRequestHandler<QueryDisability, QueryResult<Disability>>
    {
        private readonly IDisabilityRepository _disabilityRepository;
        
        
        public QueryDisabilityHandler(IDisabilityRepository disabilityRepository)
        {
            _disabilityRepository = disabilityRepository;
        }
        
        public async Task<QueryResult<Disability>> Handle(QueryDisability request, CancellationToken cancellationToken)
        {
            return await _disabilityRepository.QueryDisability(request.ApplyCustomFilters());
        }
    }
}

