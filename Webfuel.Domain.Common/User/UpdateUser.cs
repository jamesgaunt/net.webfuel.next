using FluentValidation;
using MediatR;

namespace Webfuel.Domain.Common
{
    public class UpdateUser : IRequest<User>
    {
        public Guid Id { get; set; }

        public string Email { get; set; } = String.Empty;
    }

    internal class UpdateUserHandler : IRequestHandler<UpdateUser, User>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(UpdateUser request, CancellationToken cancellationToken)
        {
            var original = await _userRepository.RequireUserAsync(request.Id);

            var updated = original.Copy();
            updated.Email = request.Email;

            return await _userRepository.UpdateUserAsync(original: original, updated: updated); ;
        }
    }
}
