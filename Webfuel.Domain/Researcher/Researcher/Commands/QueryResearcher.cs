using MediatR;

namespace Webfuel.Domain
{
    public class QueryResearcher : Query, IRequest<QueryResult<Researcher>>
    {
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(Researcher.Email), Search);
            return this;
        }
    }

    internal class QueryResearcherHandler : IRequestHandler<QueryResearcher, QueryResult<Researcher>>
    {
        private readonly IResearcherRepository _researcherRepository;

        public QueryResearcherHandler(IResearcherRepository researcherRepository)
        {
            _researcherRepository = researcherRepository;
        }

        public async Task<QueryResult<Researcher>> Handle(QueryResearcher request, CancellationToken cancellationToken)
        {
            return await _researcherRepository.QueryResearcher(request.ApplyCustomFilters());
        }
    }
}
