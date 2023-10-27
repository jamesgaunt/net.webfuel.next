using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Webfuel.Domain
{
    internal class ResetUserPasswordHandler : IRequestHandler<ResetUserPassword>
    {
        private readonly IUserRepository _userRepository;

        public ResetUserPasswordHandler(IUserRepository userRepository, IIdentityAccessor identityAccessor)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(ResetUserPassword request, CancellationToken cancellationToken)
        {
            if (request.NewPassword != request.ConfirmNewPassword)
                throw new InvalidOperationException("New passwords do not match");

            AuthenticationUtility.EnforcePasswordRequirements(request.NewPassword);

            var user = await _userRepository.GetUser(request.UserId);
            if (user == null)
                throw new InvalidOperationException("The specified user does not exist");

            if (user.PasswordResetToken == Guid.Empty || user.PasswordResetToken != request.PasswordResetToken)
                throw new InvalidOperationException("Invalid password reset token");

            if (user.PasswordResetValidUntil < DateTimeOffset.UtcNow)
                throw new InvalidOperationException("Password reset token has expired");

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
