using MediatR;
using Microsoft.Identity.Client;

namespace Webfuel.Common
{
    public class SendEmailRequest : IRequest
    {
        public required string Subject { get; set; }
        public required string HtmlBody { get; set; }

        public required string SendTo { get; set; }
        public string SendCc { get; set; } = String.Empty;

        public required string SentBy { get; set; }
        public required string ReplyTo { get; set; }

        public required Guid? EntityId { get; set; }
    }

    internal class SendEmailHandler : IRequestHandler<SendEmailRequest>
    {
        private readonly IConfigurationService _configurationService;
        private readonly IEmailService _emailService;

        public SendEmailHandler(
            IConfigurationService configurationService, 
            IEmailService emailService)
            
        {
            _configurationService = configurationService;
            _emailService = emailService;
        }

        public async Task Handle(SendEmailRequest request, CancellationToken cancellationToken)
        {
            await _emailService.SendAsync(
                sendTo: String.Join(';', request.SendTo),
                sendCc: String.Join(';', request.SendCc),
                sendBcc: "james.gaunt@webfuel.com",
                sentBy: request.SentBy,
                replyTo: request.ReplyTo,
                subject: request.Subject,
                htmlBody: request.HtmlBody,
                entityId: request.EntityId);
        }
    }
}
