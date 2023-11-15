using MediatR;

namespace Webfuel.Domain
{
    internal class QueryUserLoginHandler : IRequestHandler<QueryUserLogin, QueryResult<UserLogin>>
    {
        private readonly IUserLoginRepository _userLoginRepository;

        public QueryUserLoginHandler(IUserLoginRepository userLoginRepository)
        {
            _userLoginRepository = userLoginRepository;
        }

        public async Task<QueryResult<UserLogin>> Handle(QueryUserLogin request, CancellationToken cancellationToken)
        {
            return await _userLoginRepository.QueryUserLogin(request.ApplyCustomFilters());
        }
    }
}
