using DocumentFormat.OpenXml.ExtendedProperties;
using MediatR;

namespace Webfuel.Common
{
    public class UpdateEmailTemplate : IRequest<EmailTemplate>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string SendTo { get; set; }
        public required string SendCc { get; set; }
        public required string SendBcc { get; set; }
        public required string SentBy { get; set; }
        public required string ReplyTo { get; set; }
        public required string Subject { get; set; }
        public required string HtmlTemplate { get; set; }
    }
    internal class UpdateEmailTemplateHandler : IRequestHandler<UpdateEmailTemplate, EmailTemplate>
    {
        private readonly IEmailTemplateRepository _emailTemplateRepository;

        public UpdateEmailTemplateHandler(IEmailTemplateRepository emailTemplateRepository)
        {
            _emailTemplateRepository = emailTemplateRepository;
        }

        public async Task<EmailTemplate> Handle(UpdateEmailTemplate request, CancellationToken cancellationToken)
        {
            var original = await _emailTemplateRepository.RequireEmailTemplate(request.Id);

            var updated = original.Copy();
            updated.Name = request.Name;
            updated.SendTo = request.SendTo;
            updated.SendCc = request.SendCc;
            updated.SendBcc = request.SendBcc;
            updated.SentBy = request.SentBy;
            updated.ReplyTo = request.ReplyTo;
            updated.Subject = request.Subject;
            updated.HtmlTemplate = request.HtmlTemplate;

            updated = await _emailTemplateRepository.UpdateEmailTemplate(original: original, updated: updated);
            return updated;
        }
    }
}

