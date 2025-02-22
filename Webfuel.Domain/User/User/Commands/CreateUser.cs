﻿using MediatR;

namespace Webfuel.Domain
{
    public class CreateUser : IRequest<User>
    {
        public required string Email { get; set; }

        public required string Title { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required Guid UserGroupId { get; set; }
    }

    internal class CreateUserHandler : IRequestHandler<CreateUser, User>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            return await _userRepository.InsertUser(new User
            {
                Email = request.Email,
                Title = request.Title,
                FirstName = request.FirstName,
                LastName = request.LastName,
                FullName = request.FirstName + " " + request.LastName,
                UserGroupId = request.UserGroupId,
                CreatedAt = DateTimeOffset.UtcNow
            });
        }
    }
}
