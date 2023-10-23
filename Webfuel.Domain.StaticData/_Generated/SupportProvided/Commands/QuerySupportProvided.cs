using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QuerySupportProvided: Query, IRequest<QueryResult<SupportProvided>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(SupportProvided.Name), Search);
            return this;
        }
    }
    internal class QuerySupportProvidedHandler : IRequestHandler<QuerySupportProvided, QueryResult<SupportProvided>>
    {
        private readonly ISupportProvidedRepository _supportProvidedRepository;
        
        
        public QuerySupportProvidedHandler(ISupportProvidedRepository supportProvidedRepository)
        {
            _supportProvidedRepository = supportProvidedRepository;
        }
        
        public async Task<QueryResult<SupportProvided>> Handle(QuerySupportProvided request, CancellationToken cancellationToken)
        {
            return await _supportProvidedRepository.QuerySupportProvided(request.ApplyCustomFilters());
        }
    }
}

