using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Webfuel.Common;

namespace Webfuel.Domain
{
    internal class SendUserPasswordResetEmailHandler : IRequestHandler<SendUserPasswordResetEmail>
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfigurationService _configuationService;
        private readonly IEmailRelayService _emailRelayService;

        public SendUserPasswordResetEmailHandler(IUserRepository userRepository, IConfigurationService configurationService, IEmailRelayService emailRelayService)
        {
            _userRepository = userRepository;
            _configuationService = configurationService;
            _emailRelayService = emailRelayService; 
        }

        public async Task Handle(SendUserPasswordResetEmail request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmail(request.Email);
            if (user == null)
                return; // We deliberately do not indicate whether this method succeeds or not

            // Generate and save a new reset token
            var updated = user.Copy();
            updated.PasswordResetToken = Guid.NewGuid();
            updated.PasswordResetValidUntil = DateTime.Now.AddHours(1);
            await _userRepository.UpdateUser(updated: updated, original: user);

            // Send the email
            var configuration = await _configuationService.GetConfiguration();

            var htmlBody =
$@"
<p>Thank you for requesting password reset instructions from {configuration.DomainName}.</p>
<p>In order to reset your password please click on the following link, and enter your new password:</p>
<p>{configuration.DomainName}/reset-password/{user.Id}/{updated.PasswordResetToken}</p>
<p>PLEASE NOTE: This link will remain valid for 1 hour only.</p>
<p>You will then be able to login at:</p>
<p>{configuration.DomainName}</p>
<p>To login use your email address [{user.Email}] and your new password.</p>
<p>If you have any difficulties please contact your system administrator.</p>
";

            await _emailRelayService.SendAsync(
                accountName: "rss-ucl",
                sendTo: user.Email,
                sendCc: String.Empty,
                sendBcc: "james.gaunt@webfuel.com",
                replyTo: configuration.ReplyTo,
                sentBy: configuration.ReplyTo,
                subject: "Password reset instructions for " + configuration.DomainName,
                htmlBody: htmlBody,
                attachments: null);
        }
    }
}
