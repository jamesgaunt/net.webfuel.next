using MediatR;

namespace Webfuel.Domain
{
    internal class QueryUserGroupHandler : IRequestHandler<QueryUserGroup, QueryResult<UserGroup>>
    {
        private readonly IUserGroupRepository _userGroupRepository;

        public QueryUserGroupHandler(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }

        public async Task<QueryResult<UserGroup>> Handle(QueryUserGroup request, CancellationToken cancellationToken)
        {
            return await _userGroupRepository.QueryUserGroup(request.ApplyCustomFilters());
        }
    }
}
