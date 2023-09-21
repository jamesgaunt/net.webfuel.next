using Azure.Core;
using MediatR;

namespace Webfuel.Domain.Common
{
    public class QueryUserGroup : Query, IRequest<QueryResult<UserGroup>>
    {
        public string Search { get; set; } = String.Empty;

        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(UserGroup.Name), Search);
            return this;
        }
    }

    internal class QueryUserGroupHandler : IRequestHandler<QueryUserGroup, QueryResult<UserGroup>>
    {
        private readonly IUserGroupRepository _userGroupRepository;

        public QueryUserGroupHandler(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }

        public async Task<QueryResult<UserGroup>> Handle(QueryUserGroup request, CancellationToken cancellationToken)
        {
            return await _userGroupRepository.QueryUserGroupAsync(request.ApplyCustomFilters());
        }
    }
}
