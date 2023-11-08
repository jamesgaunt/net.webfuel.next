using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Webfuel.Domain
{
    internal class UpdateUserPasswordHandler : IRequestHandler<UpdateUserPassword>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserPasswordHandler(IUserRepository userRepository, IIdentityAccessor identityAccessor)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(UpdateUserPassword request, CancellationToken cancellationToken)
        {
            AuthenticationUtility.EnforcePasswordRequirements(request.NewPassword);

            var user = await _userRepository.GetUser(request.UserId);
            if (user == null)
                throw new InvalidOperationException("The specified user does not exist");

            // Looks like we are ok to go ahead with this change
            var updated = user.Copy();
            updated.PasswordSalt = AuthenticationUtility.CreateSalt();
            updated.PasswordHash = AuthenticationUtility.CreateHash(request.NewPassword, updated.PasswordSalt);
            updated.PasswordResetAt = DateTimeOffset.UtcNow;
            updated.PasswordResetToken = Guid.Empty;
            updated.PasswordResetValidUntil = DateTimeOffset.UtcNow.AddDays(-1);
            await _userRepository.UpdateUser(updated: updated, original: user);
        }
    }
}
