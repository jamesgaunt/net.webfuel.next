using MediatR;

namespace Webfuel.Domain
{
    internal class QuerySupportTeamUserHandler : IRequestHandler<QuerySupportTeamUser, QueryResult<SupportTeamUser>>
    {
        private readonly ISupportTeamUserRepository _supportTeamUserRepository;

        public QuerySupportTeamUserHandler(ISupportTeamUserRepository supportTeamUserRepository)
        {
            _supportTeamUserRepository = supportTeamUserRepository;
        }

        public async Task<QueryResult<SupportTeamUser>> Handle(QuerySupportTeamUser request, CancellationToken cancellationToken)
        {
            return await _supportTeamUserRepository.QuerySupportTeamUser(request.ApplyCustomFilters());
        }
    }
}
