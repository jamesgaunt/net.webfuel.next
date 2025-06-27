using MediatR;

namespace Webfuel.Common;

public class SendEmailAttachment
{
    public Guid? FileStorageEntryId { get; set; }

    public string FileName { get; set; } = String.Empty;

    public Int64 SizeBytes { get; set; }
}

public class SendEmailRequest : IRequest
{
    public required string Subject { get; set; }
    public required string HtmlBody { get; set; }

    public required string SendTo { get; set; }
    public string SendCc { get; set; } = String.Empty;

    public required string SentBy { get; set; }
    public required string ReplyTo { get; set; }

    public required Guid? EntityId { get; set; }

    public required Guid? LocalFileStorageGroupId { get; set; }
    public required Guid? GlobalFileStorageGroupId { get; set; }

    public List<SendEmailAttachment> Attachments { get; set; } = new List<SendEmailAttachment>();
}

internal class SendEmailHandler : IRequestHandler<SendEmailRequest>
{
    private readonly IConfigurationService _configurationService;
    private readonly IEmailService _emailService;
    private readonly IFileStorageService _fileStorageService;

    public SendEmailHandler(
        IConfigurationService configurationService,
        IEmailService emailService,
        IFileStorageService fileStorageService)

    {
        _configurationService = configurationService;
        _emailService = emailService;
        _fileStorageService = fileStorageService;
    }

    public async Task Handle(SendEmailRequest request, CancellationToken cancellationToken)
    {
        var attachments = await MapAttachments(request);

        await _emailService.SendAsync(
            sendTo: request.SendTo,
            sendCc: request.SendCc,
            sendBcc: "james.gaunt@webfuel.com",
            sentBy: request.SentBy,
            replyTo: request.ReplyTo,
            subject: request.Subject,
            htmlBody: request.HtmlBody,
            entityId: request.EntityId,
            attachments: attachments);

        foreach (var attachment in attachments)
        {
            // Dispose of the memory stream to free up resources
            attachment.Stream.Dispose();
        }
    }

    async Task<List<EmailRelayAttachment>> MapAttachments(SendEmailRequest request)
    {
        var attachments = new List<EmailRelayAttachment>();

        foreach (var attachment in request.Attachments)
        {
            if (attachment.FileStorageEntryId.HasValue)
            {
                var ms = await _fileStorageService.LoadDirect(attachment.FileStorageEntryId.Value);
                if (ms != null)
                {
                    attachments.Add(new EmailRelayAttachment(attachment.FileName, ms));
                }
            }
        }
        return attachments;
    }
}
