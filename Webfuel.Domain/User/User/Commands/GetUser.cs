using FluentValidation;
using MediatR;

namespace Webfuel.Domain
{
    public class GetUser : IRequest<User?>
    {
        public Guid Id { get; set; }
    }

    internal class GetUserHandler : IRequestHandler<GetUser, User?>
    {
        private readonly IUserRepository _userRepository;

        public GetUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> Handle(GetUser request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetUser(request.Id);
        }
    }
}
