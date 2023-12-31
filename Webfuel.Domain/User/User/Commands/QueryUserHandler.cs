﻿using MediatR;

namespace Webfuel.Domain
{
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
