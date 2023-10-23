using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryIsTeamMembersConsulted: Query, IRequest<QueryResult<IsTeamMembersConsulted>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(IsTeamMembersConsulted.Name), Search);
            return this;
        }
    }
    internal class QueryIsTeamMembersConsultedHandler : IRequestHandler<QueryIsTeamMembersConsulted, QueryResult<IsTeamMembersConsulted>>
    {
        private readonly IIsTeamMembersConsultedRepository _isTeamMembersConsultedRepository;
        
        
        public QueryIsTeamMembersConsultedHandler(IIsTeamMembersConsultedRepository isTeamMembersConsultedRepository)
        {
            _isTeamMembersConsultedRepository = isTeamMembersConsultedRepository;
        }
        
        public async Task<QueryResult<IsTeamMembersConsulted>> Handle(QueryIsTeamMembersConsulted request, CancellationToken cancellationToken)
        {
            return await _isTeamMembersConsultedRepository.QueryIsTeamMembersConsulted(request.ApplyCustomFilters());
        }
    }
}

