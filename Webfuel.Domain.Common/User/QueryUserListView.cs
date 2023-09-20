using Azure.Core;
using MediatR;

namespace Webfuel.Domain.Common
{
    public class QueryUserListView : Query, IRequest<QueryResult<UserListView>>
    {
        public string Search { get; set; } = String.Empty;

        public override RepositoryQuery ToRepositoryQuery()
        {
            var q = base.ToRepositoryQuery();
            q.All(a => 
                a.Contains(nameof(UserListView.Email), Search));
            return q;
        }
    }

    internal class QueryUserHandler : IRequestHandler<QueryUserListView, QueryResult<UserListView>>
    {
        private readonly IUserListViewRepository _userListViewRepository;

        public QueryUserHandler(IUserListViewRepository userListViewRepository)
        {
            _userListViewRepository = userListViewRepository;
        }

        public async Task<QueryResult<UserListView>> Handle(QueryUserListView request, CancellationToken cancellationToken)
        {
            return await _userListViewRepository.QueryUserListViewAsync(request.ToRepositoryQuery());
        }
    }
}
