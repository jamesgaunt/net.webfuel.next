using MediatR;

namespace Webfuel.Domain
{

    internal class QueryUserActivityHandler : IRequestHandler<QueryUserActivity, QueryResult<UserActivity>>
    {
        private readonly IUserActivityRepository _userActivityRepository;

        public QueryUserActivityHandler(IUserActivityRepository userActivityRepository)
        {
            _userActivityRepository = userActivityRepository;
        }

        public async Task<QueryResult<UserActivity>> Handle(QueryUserActivity request, CancellationToken cancellationToken)
        {
            return await _userActivityRepository.QueryUserActivity(request.ApplyCustomFilters());
        }
    }
}
