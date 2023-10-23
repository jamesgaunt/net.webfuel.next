using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryHowDidYouFindUs: Query, IRequest<QueryResult<HowDidYouFindUs>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(HowDidYouFindUs.Name), Search);
            return this;
        }
    }
    internal class QueryHowDidYouFindUsHandler : IRequestHandler<QueryHowDidYouFindUs, QueryResult<HowDidYouFindUs>>
    {
        private readonly IHowDidYouFindUsRepository _howDidYouFindUsRepository;
        
        
        public QueryHowDidYouFindUsHandler(IHowDidYouFindUsRepository howDidYouFindUsRepository)
        {
            _howDidYouFindUsRepository = howDidYouFindUsRepository;
        }
        
        public async Task<QueryResult<HowDidYouFindUs>> Handle(QueryHowDidYouFindUs request, CancellationToken cancellationToken)
        {
            return await _howDidYouFindUsRepository.QueryHowDidYouFindUs(request.ApplyCustomFilters());
        }
    }
}

