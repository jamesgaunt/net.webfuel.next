using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryProfessionalBackground: Query, IRequest<QueryResult<ProfessionalBackground>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(ProfessionalBackground.Name), Search);
            return this;
        }
    }
    internal class QueryProfessionalBackgroundHandler : IRequestHandler<QueryProfessionalBackground, QueryResult<ProfessionalBackground>>
    {
        private readonly IProfessionalBackgroundRepository _professionalBackgroundRepository;
        
        
        public QueryProfessionalBackgroundHandler(IProfessionalBackgroundRepository professionalBackgroundRepository)
        {
            _professionalBackgroundRepository = professionalBackgroundRepository;
        }
        
        public async Task<QueryResult<ProfessionalBackground>> Handle(QueryProfessionalBackground request, CancellationToken cancellationToken)
        {
            return await _professionalBackgroundRepository.QueryProfessionalBackground(request.ApplyCustomFilters());
        }
    }
}

