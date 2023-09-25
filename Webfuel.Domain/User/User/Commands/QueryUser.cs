using Azure.Core;
using MediatR;

namespace Webfuel.Domain
{
    public class QueryUser : Query, IRequest<QueryResult<User>>
    {
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(User.Email), Search);
            return this;
        }
    }

    internal class QueryUserHandler : IRequestHandler<QueryUser, QueryResult<User>>
    {
        private readonly IUserRepository _userRepository;

        public QueryUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<QueryResult<User>> Handle(QueryUser request, CancellationToken cancellationToken)
        {
            return await _userRepository.QueryUser(request.ApplyCustomFilters());
        }
    }
}
