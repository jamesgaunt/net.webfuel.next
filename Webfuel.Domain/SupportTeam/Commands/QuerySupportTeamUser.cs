using MediatR;

namespace Webfuel.Domain
{
    public class QuerySupportTeamUser : Query, IRequest<QueryResult<SupportTeamUser>>
    {
        public Guid? UserId { get; set; }

        public Guid? SupportTeamId { get; set; }

        public Query ApplyCustomFilters()
        {
            this.Equal(nameof(SupportTeamUser.UserId), UserId, UserId != null);
            this.Equal(nameof(SupportTeamUser.SupportTeamId), SupportTeamId, SupportTeamId != null);
            return this;
        }
    }

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
