using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryEthnicity: Query, IRequest<QueryResult<Ethnicity>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(Ethnicity.Name), Search);
            return this;
        }
    }
    internal class QueryEthnicityHandler : IRequestHandler<QueryEthnicity, QueryResult<Ethnicity>>
    {
        private readonly IEthnicityRepository _ethnicityRepository;
        
        
        public QueryEthnicityHandler(IEthnicityRepository ethnicityRepository)
        {
            _ethnicityRepository = ethnicityRepository;
        }
        
        public async Task<QueryResult<Ethnicity>> Handle(QueryEthnicity request, CancellationToken cancellationToken)
        {
            return await _ethnicityRepository.QueryEthnicity(request.ApplyCustomFilters());
        }
    }
}

