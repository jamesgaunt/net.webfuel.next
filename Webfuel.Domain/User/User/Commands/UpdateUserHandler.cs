using MediatR;

namespace Webfuel.Domain
{
    internal class UpdateUserHandler : IRequestHandler<UpdateUser, User>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(UpdateUser request, CancellationToken cancellationToken)
        {
            var original = await _userRepository.RequireUser(request.Id);

            var updated = original.Copy();
            updated.Email = request.Email;
            updated.UserGroupId = request.UserGroupId;

            return await _userRepository.UpdateUser(original: original, updated: updated); ;
        }
    }
}
