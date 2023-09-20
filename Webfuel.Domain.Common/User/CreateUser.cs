using FluentValidation;
using MediatR;

namespace Webfuel.Domain.Common
{
    public class CreateUser : IRequest<User>
    {
        public required string Email { get; set; }
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
            return await _userRepository.InsertUserAsync(new User { Email = request.Email });
        }
    }
}
