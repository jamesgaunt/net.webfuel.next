using Azure.Core;
using MediatR;

namespace Webfuel.Domain.Common
{
    public class QueryUserGroupListView : Query, IRequest<QueryResult<UserGroupListView>>
    {
        public string Search { get; set; } = String.Empty;

        public override RepositoryQuery ToRepositoryQuery()
        {
            var q = base.ToRepositoryQuery();
            q.All(a => 
                a.Contains(nameof(UserGroupListView.Name), Search));
            return q;
        }
    }

    internal class QueryUserGroupHandler : IRequestHandler<QueryUserGroupListView, QueryResult<UserGroupListView>>
    {
        private readonly IUserGroupListViewRepository _userGroupListViewRepository;

        public QueryUserGroupHandler(IUserGroupListViewRepository userGroupListViewRepository)
        {
            _userGroupListViewRepository = userGroupListViewRepository;
        }

        public async Task<QueryResult<UserGroupListView>> Handle(QueryUserGroupListView request, CancellationToken cancellationToken)
        {
            return await _userGroupListViewRepository.QueryUserGroupListViewAsync(request.ToRepositoryQuery());
        }
    }
}
