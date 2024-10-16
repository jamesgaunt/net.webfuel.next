using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Common
{
    public interface IEmailTemplateService
    {
        Task SendEmail(string templateName, IDictionary<string, string> replacements);
    }

    [Service(typeof(IEmailTemplateService))]
    internal class EmailTemplateService: IEmailTemplateService
    {
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        private readonly IEmailService _emailService;

        public EmailTemplateService(IEmailTemplateRepository emailTemplateRepository, IIdentityAccessor identityAccessor, IEmailService emailService)
        {
            _emailTemplateRepository = emailTemplateRepository;
            _emailService = emailService;
        }

        public async Task SendEmail(string templateName, IDictionary<string, string> replacements)
        {
            var template = await _emailTemplateRepository.GetEmailTemplateByName(templateName);
            if (template == null)
                return; // Nothing to do!

            await _emailService.SendAsync(
                sendTo: Replace(template.SendTo, replacements),
                sendCc: Replace(template.SendCc, replacements),
                sendBcc: Replace(template.SendBcc, replacements),
                sentBy: Replace(template.SentBy, replacements),
                replyTo: Replace(template.ReplyTo, replacements),
                subject: Replace(template.Subject, replacements),
                htmlBody: Format(Replace(template.HtmlTemplate, replacements)),
                entityId: null);
        }

        string Format(string template)
        {
            template = template.Replace("\n", "<br/>");
            return template;
        }

        string Replace(string input, IDictionary<string, string> replacements)
        {
            foreach(var replacement in replacements)
            {
                if (!input.Contains('#'))
                    break;
                input = input.Replace($"#{replacement.Key}#", replacement.Value);
            }
            return input;
        }
    }
}
