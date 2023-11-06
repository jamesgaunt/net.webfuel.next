using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Webfuel.Domain
{
    internal class ChangeUserPasswordHandler : IRequestHandler<ChangeUserPassword>
    {
        private readonly IUserRepository _userRepository;
        private readonly IIdentityAccessor _identityAccessor;

        public ChangeUserPasswordHandler(IUserRepository userRepository, IIdentityAccessor identityAccessor)
        {
            _userRepository = userRepository;
            _identityAccessor = identityAccessor;   
        }

        public async Task Handle(ChangeUserPassword request, CancellationToken cancellationToken)
        {
            if (request.NewPassword != request.ConfirmNewPassword)
                throw new DomainException("New passwords do not match");

            AuthenticationUtility.EnforcePasswordRequirements(request.NewPassword);

            if (_identityAccessor.User == null)
                throw new InvalidOperationException("No current user");

            var user = await _userRepository.GetUser(_identityAccessor.User.Id);
            if (user == null)
                throw new InvalidOperationException("The specified user does not exist");

            if (!AuthenticationUtility.ValidatePassword(request.CurrentPassword, user.PasswordHash, user.PasswordSalt))
                throw new DomainException("Current password is not valid");

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
