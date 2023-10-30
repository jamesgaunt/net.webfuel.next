using MediatR;

namespace Webfuel.Domain
{

    internal class QueryUserActivityHandler : IRequestHandler<QueryUserActivity, QueryResult<UserActivity>>
    {
        private readonly IUserActivityRepository _userActivityRepository;
        private readonly IIdentityAccessor _identityAccessor;

        public QueryUserActivityHandler(IUserActivityRepository userActivityRepository, IIdentityAccessor identityAccessor)
        {
            _userActivityRepository = userActivityRepository;
            _identityAccessor = identityAccessor;
        }

        public async Task<QueryResult<UserActivity>> Handle(QueryUserActivity request, CancellationToken cancellationToken)
        {
            if(request.UserId == null)
                request.UserId = _identityAccessor.User?.Id ?? throw new InvalidOperationException("No current user");

            return await _userActivityRepository.QueryUserActivity(request.ApplyCustomFilters());
        }
    }
}
