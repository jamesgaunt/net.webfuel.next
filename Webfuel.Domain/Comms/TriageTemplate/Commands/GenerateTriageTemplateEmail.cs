using MediatR;
using Microsoft.AspNetCore.Mvc.Localization;
using Webfuel.Common;

namespace Webfuel.Domain;

public class GenerateTriageTemplateEmail : IRequest<SendEmailRequest>
{
    public Guid TriageTemplateId { get; set; }

    public Guid SupportRequestId { get; set; }
}

internal class GenerateTriageTemplateEmailHandler : IRequestHandler<GenerateTriageTemplateEmail, SendEmailRequest>
{
    private readonly ITriageTemplateRepository _triageTemplateRepository;
    private readonly ISupportRequestRepository _supportRequestRepository;
    private readonly IUserRepository _userRepository;
    private readonly IIdentityAccessor _identityAccessor;

    public GenerateTriageTemplateEmailHandler(
        ITriageTemplateRepository triageTemplateRepository,
        ISupportRequestRepository supportRequestRepository,
        IUserRepository userRepository,
        IIdentityAccessor identityAccessor)
    {
        _triageTemplateRepository = triageTemplateRepository;
        _supportRequestRepository = supportRequestRepository;
        _userRepository = userRepository;
        _identityAccessor = identityAccessor;
    }

    public async Task<SendEmailRequest> Handle(GenerateTriageTemplateEmail request, CancellationToken cancellationToken)
    {
        var triageTemplate = await _triageTemplateRepository.RequireTriageTemplate(request.TriageTemplateId);
        var supportRequest = await _supportRequestRepository.RequireSupportRequest(request.SupportRequestId);
        var user = await _userRepository.RequireUser(_identityAccessor.User?.Id ?? Guid.Empty);

        var subject = DoReplacements(triageTemplate.Subject, supportRequest, user);
        var htmlBody = DoReplacements(triageTemplate.HtmlTemplate, supportRequest, user);

        htmlBody += STANDARD_FOOTER;

        var result = new SendEmailRequest
        {
            Subject = subject,
            HtmlBody = htmlBody,
            SendTo = user.Email,
            SentBy = "admin@rssimperialpartners.org.uk",
            ReplyTo = "admin@rssimperialpartners.org.uk",
            EntityId = supportRequest.Id
        };

        return result;
    }

    string DoReplacements(string input, SupportRequest supportRequest, User user)
    {
        input = input.Replace("PROJECT_TITLE", supportRequest.Title);
        input = input.Replace("ADVISER_NAME", user.FullName);
        input = input.Replace("RESEARCH_TEAM_CONTACT_NAME", supportRequest.TeamContactFullName);
        input = input.Replace("TARGET_SUBMISSION_DATE", supportRequest.TargetSubmissionDate?.ToString("dd/MM/yyyy"));
        return input;
    }

    static readonly string STANDARD_FOOTER = @"
<div><img src='https://www.webfuel.com/external/rss-transparent-icl-email-footer.png' alt='NIHR Research Support Service' /></div>
<b>Imperial College London and Partners Hub</b><br/>
Free support for researchers in England to design and deliver innovative studies and improve the quality and efficiency of research nationwide<br/>
Access free support here: <a href='https://www.imperial.ac.uk/nihr-research-support-service/'>https://www.imperial.ac.uk/nihr-research-support-service/</a><br/>
<b>Follow us on <a href='https://www.linkedin.com/company/nihr-rss-imperial-partners-hub'>LinkedIn</a></b>
<b>Sign up to our <a href='https://mailchi.mp/601a4eb137d5/nihr-research-support-service-imperial-college-london-partners'>mailing list</a></b>
";
}
