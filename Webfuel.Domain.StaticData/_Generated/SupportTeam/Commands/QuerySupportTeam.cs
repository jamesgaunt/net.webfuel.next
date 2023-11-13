using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QuerySupportTeam: Query, IRequest<QueryResult<SupportTeam>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(SupportTeam.Name), Search);
            return this;
        }
    }
    internal class QuerySupportTeamHandler : IRequestHandler<QuerySupportTeam, QueryResult<SupportTeam>>
    {
        private readonly ISupportTeamRepository _supportTeamRepository;
        
        
        public QuerySupportTeamHandler(ISupportTeamRepository supportTeamRepository)
        {
            _supportTeamRepository = supportTeamRepository;
        }
        
        public async Task<QueryResult<SupportTeam>> Handle(QuerySupportTeam request, CancellationToken cancellationToken)
        {
            return await _supportTeamRepository.QuerySupportTeam(request.ApplyCustomFilters());
        }
    }
}

