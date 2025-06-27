using Webfuel.Common;

namespace Webfuel;

public interface IEmailService
{
    Task SendAsync(
        string sendTo,
        string sendCc,
        string sendBcc,
        string sentBy,
        string replyTo,
        string subject,
        string htmlBody,
        Guid? entityId,
        IEnumerable<EmailRelayAttachment>? attachments);

    Task<List<EmailLog>> SelectEmailLogByEntityId(Guid entityId);
}

[Service(typeof(IEmailService))]
internal class EmailService : IEmailService
{
    private readonly IEmailRelayService _emailRelayService;
    private readonly IEmailLogRepository _emailLogRepository;

    public EmailService(IEmailRelayService emailRelayService, IEmailLogRepository emailLogRepository)
    {
        _emailRelayService = emailRelayService;
        _emailLogRepository = emailLogRepository;
    }

    public async Task SendAsync(
        string sendTo,
        string sendCc,
        string sendBcc,
        string sentBy,
        string replyTo,
        string subject,
        string htmlBody,
        Guid? entityId,
        IEnumerable<EmailRelayAttachment>? attachments)
    {
        await _emailRelayService.SendAsync(
            accountName: "rss-ucl",
            sendTo: sendTo,
            sendCc: sendCc,
            sendBcc: sendBcc,
            replyTo: replyTo,
            sentBy: sentBy,
            subject: subject,
            htmlBody: htmlBody,
            attachments: attachments);

        await _emailLogRepository.InsertEmailLog(new EmailLog
        {
            SendTo = sendTo,
            SendCc = sendCc,
            SendBcc = sendBcc,
            ReplyTo = replyTo,
            SentBy = sentBy,
            Subject = subject,
            HtmlBody = htmlBody,
            SentAt = DateTimeOffset.UtcNow,
            EntityId = entityId ?? Guid.Empty
        });
    }

    public Task<List<EmailLog>> SelectEmailLogByEntityId(Guid entityId)
    {
        return _emailLogRepository.SelectEmailLogByEntityId(entityId);
    }
}