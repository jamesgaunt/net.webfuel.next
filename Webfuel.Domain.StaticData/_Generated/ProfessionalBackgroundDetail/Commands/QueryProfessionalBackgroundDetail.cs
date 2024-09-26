using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryProfessionalBackgroundDetail: Query, IRequest<QueryResult<ProfessionalBackgroundDetail>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(ProfessionalBackgroundDetail.Name), Search);
            return this;
        }
    }
    internal class QueryProfessionalBackgroundDetailHandler : IRequestHandler<QueryProfessionalBackgroundDetail, QueryResult<ProfessionalBackgroundDetail>>
    {
        private readonly IProfessionalBackgroundDetailRepository _professionalBackgroundDetailRepository;
        
        
        public QueryProfessionalBackgroundDetailHandler(IProfessionalBackgroundDetailRepository professionalBackgroundDetailRepository)
        {
            _professionalBackgroundDetailRepository = professionalBackgroundDetailRepository;
        }
        
        public async Task<QueryResult<ProfessionalBackgroundDetail>> Handle(QueryProfessionalBackgroundDetail request, CancellationToken cancellationToken)
        {
            return await _professionalBackgroundDetailRepository.QueryProfessionalBackgroundDetail(request.ApplyCustomFilters());
        }
    }
}

