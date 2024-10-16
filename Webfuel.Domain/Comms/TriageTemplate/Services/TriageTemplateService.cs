using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain;

// TODO: CONVERT TO A COMMAND

public interface ITriageTemplateService
{
    Task SendEmail(string templateName, IDictionary<string, string> replacements);
}

[Service(typeof(ITriageTemplateService))]
internal class TriageTemplateService: ITriageTemplateService
{
    private readonly ITriageTemplateRepository _triageTemplateRepository;
    private readonly IEmailService _emailService;

    public TriageTemplateService(ITriageTemplateRepository triageTemplateRepository, IIdentityAccessor identityAccessor, IEmailService emailService)
    {
        _triageTemplateRepository = triageTemplateRepository;
        _emailService = emailService;
    }

    public async Task SendEmail(string templateName, IDictionary<string, string> replacements)
    {
        var template = await _triageTemplateRepository.GetTriageTemplateByName(templateName);
        if (template == null)
            return; // Nothing to do!

        /*
        await _emailService.SendAsync(
            sendTo: Replace(template.SendTo, replacements),
            sendCc: Replace(template.SendCc, replacements),
            sendBcc: Replace(template.SendBcc, replacements),
            sentBy: Replace(template.SentBy, replacements),
            replyTo: Replace(template.ReplyTo, replacements),
            subject: Replace(template.Subject, replacements),
            htmlBody: Format(Replace(template.HtmlTemplate, replacements)),
            entityId: null);
        */
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
            input = input.Replace(replacement.Key, replacement.Value);
        }
        return input;
    }
}
