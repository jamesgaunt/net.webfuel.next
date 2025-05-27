using MediatR;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain;

public class UpdateProject : IRequest<Project>
{
    public required Guid Id { get; set; }
    public required Guid StatusId { get; set; }
    public DateOnly? ClosureDate { get; set; } = null;
    public string AdministratorComments { get; set; } = String.Empty;

    public string Title { get; set; } = String.Empty;

    // Ownership

    public Guid? LeadAdviserUserId { get; set; }

    // Annual Reporting

    public Guid? WillStudyUseCTUId { get; set; }
    public Guid? IsPaidRSSAdviserLeadId { get; set; }
    public Guid? IsPaidRSSAdviserCoapplicantId { get; set; }
    public List<Guid> RSSHubProvidingAdviceIds { get; set; } = new List<Guid>();
    public Decimal? MonetaryValueOfFundingApplication { get; set; }

    // Funding Stream

    public Guid? SubmittedFundingStreamId { get; set; }
    public string SubmittedFundingStreamFreeText { get; set; } = String.Empty;
    public string SubmittedFundingStreamName { get; set; } = String.Empty;

    // Submission Details

    public required DateOnly? OutlineSubmissionDeadline { get; set; }
    public required Guid? OutlineSubmissionStatusId { get; set; }
    public required DateOnly? OutlineOutcomeExpectedDate { get; set; }
    public required Guid? OutlineOutcomeId { get; set; }

    public required DateOnly? FullSubmissionDeadline { get; set; }
    public required Guid? FullSubmissionStatusId { get; set; }
    public required DateOnly? FullOutcomeExpectedDate { get; set; }
    public required Guid? FullOutcomeId { get; set; }

    // Clinical Trial Submissions

    public DateOnly? ProjectStartDate { get; set; } = null;
    public int? RecruitmentTarget { get; set; } = null;
    public int? NumberOfProjectSites { get; set; } = null;
    public Guid? IsInternationalMultiSiteStudyId { get; set; }

    // Other Details

    public bool SocialCare { get; set; }
    public bool PublicHealth { get; set; }

    // Project Advisers (not stored on the project)

    public List<Guid> ProjectAdviserUserIds { get; set; } = new List<Guid>();
}

internal class UpdateProjectHandler : IRequestHandler<UpdateProject, Project>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IStaticDataService _staticDataService;
    private readonly IProjectEnrichmentService _projectEnrichmentService;
    private readonly IProjectAdviserService _projectAdviserService;
    private readonly IProjectChangeLogService _projectChangeLogService;
    private readonly IIdentityAccessor _identityAccessor;

    public UpdateProjectHandler(
        IProjectRepository projectRepository,
        IStaticDataService staticDataService,
        IProjectEnrichmentService projectEnrichmentService,
        IProjectAdviserService projectAdviserService,
        IProjectChangeLogService projectChangeLogService,
        IIdentityAccessor identityAccessor)
    {
        _projectRepository = projectRepository;
        _staticDataService = staticDataService;
        _projectEnrichmentService = projectEnrichmentService;
        _projectAdviserService = projectAdviserService;
        _projectChangeLogService = projectChangeLogService;
        _identityAccessor = identityAccessor;
    }

    public async Task<Project> Handle(UpdateProject request, CancellationToken cancellationToken)
    {
        var original = await _projectRepository.RequireProject(request.Id);
        if (original.Locked)
            throw new InvalidOperationException("Unable to edit a locked project");

        var updated = ProjectMapper.Apply(request, original);

        if (!_identityAccessor.Claims.Administrator)
        {
            // If the user is not an administrator, they cannot change the administrator comments
            updated.AdministratorComments = original.AdministratorComments;
        }

        await _projectAdviserService.SyncProjectAdvisersAndSendEmails(
            updated: updated,
            original: original,
            projectAdviserUserIds: request.ProjectAdviserUserIds);

        if (updated.StatusId == request.StatusId)
        {
            await _projectEnrichmentService.EnrichProject(updated);
            updated = await _projectRepository.UpdateProject(original: original, updated: updated);
            await _projectChangeLogService.InsertChangeLog(original: original, updated: updated);
            return updated; // No change to status  
        }

        var newStatus = await _staticDataService.RequireProjectStatus(request.StatusId);

        if (newStatus.Id == ProjectStatusEnum.Closed || newStatus.Id == ProjectStatusEnum.ClosedNotSubmitted)
            updated.ClosureAttempted = true;

        // Enrich the project with the new status, to detect diagnostic errors
        await _projectEnrichmentService.EnrichProject(updated);

        updated.StatusId = newStatus.Id;
        updated.Locked = newStatus.Locked;
        updated.Discarded = newStatus.Discarded;

        if (newStatus.Id == ProjectStatusEnum.Closed || newStatus.Id == ProjectStatusEnum.ClosedNotSubmitted)
        {
            updated.ClosureDate = request.ClosureDate ?? updated.ClosureDate ?? DateOnly.FromDateTime(DateTime.Today);
        }

        updated = await _projectRepository.UpdateProject(original: original, updated: updated);

        ProjectSummaryProvider.FlushProjectMetrics();

        await _projectChangeLogService.InsertChangeLog(original: original, updated: updated);
        return updated;
    }
}
