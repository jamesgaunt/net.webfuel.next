using MediatR;
using Webfuel.Domain;
using Webfuel.Domain.StaticData;

namespace Webfuel.Common;

public class SendTriageEmail : IRequest
{
    public required Guid SupportRequestId { get; set; }

    public required SendEmailRequest SendEmailRequest { get; set; }
}

internal class SendTriageEmailHandler : IRequestHandler<SendTriageEmail>
{
    private readonly IMediator _mediator;
    private readonly ISupportRequestRepository _supportRequestRepository;

    public SendTriageEmailHandler(IMediator mediator, ISupportRequestRepository supportRequestRepository)
    {
        _mediator = mediator;
        _supportRequestRepository = supportRequestRepository;
    }

    public async Task Handle(SendTriageEmail request, CancellationToken cancellationToken)
    {
        var supportRequest = await _supportRequestRepository.RequireSupportRequest(request.SupportRequestId);

        await _mediator.Send(request.SendEmailRequest, cancellationToken);

        // Store this email in the support request history

        var description = request.SendEmailRequest.HtmlBody;

        if (request.SendEmailRequest.Attachments.Count > 0)
        {
            description += "<br/><br/><b>Attachments:</b><br/>";
            foreach (var attachment in request.SendEmailRequest.Attachments)
            {
                description += $"{attachment.FileName}<br/>";
            }
        }

        var createProjectSupport = new CreateProjectSupport
        {
            ProjectSupportGroupId = supportRequest.ProjectSupportGroupId,
            Description = description,
            TeamIds = new List<Guid> { SupportTeamEnum.TriageTeam },
            AdviserIds = new List<Guid>(),
            SupportProvidedIds = new List<Guid>(),
            WorkTimeInHours = 0,
            IsPrePostAwardId = IsPrePostAwardEnum.PreAward,
            Files = new List<ProjectSupportFile>()
        };

        await _mediator.Send(createProjectSupport, cancellationToken);
    }
}
