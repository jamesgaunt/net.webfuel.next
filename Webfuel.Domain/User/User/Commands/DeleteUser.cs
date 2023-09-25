using MediatR;

namespace Webfuel.Domain
{
    public class DeleteUser : IRequest
    {
        public Guid Id { get; set; }
    }

    internal class DeleteUserHandler : IRequestHandler<DeleteUser>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(DeleteUser request, CancellationToken cancellationToken)
        {
            await _userRepository.DeleteUser(request.Id);
        }
    }
}
