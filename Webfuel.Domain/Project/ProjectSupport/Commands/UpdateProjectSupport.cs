using MediatR;

namespace Webfuel.Domain;

public class UpdateProjectSupport : IRequest<ProjectSupport>
{
    public required Guid Id { get; set; }

    public DateOnly Date { get; set; }

    public required List<Guid> TeamIds { get; set; }

    public required List<Guid> AdviserIds { get; set; }

    public required List<Guid> SupportProvidedIds { get; set; }

    public string Description { get; set; } = String.Empty;

    public required Decimal WorkTimeInHours { get; set; }

    public Guid? SupportRequestedTeamId { get; set; }

    public required Guid IsPrePostAwardId { get; set; }

    public required List<ProjectSupportFile> Files { get; set; }
}

internal class UpdateProjectSupportHandler : IRequestHandler<UpdateProjectSupport, ProjectSupport>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectSupportRepository _projectSupportRepository;
    private readonly IProjectEnrichmentService _projectEnrichmentService;
    private readonly IProjectAdviserService _projectAdviserService;
    private readonly IUserActivityRepository _userActivityRepository;
    private readonly IUserSortService _userSortService;
    private readonly IHtmlSanitizerService _htmlSanitizerService;

    public UpdateProjectSupportHandler(
        IProjectRepository projectRepository,
        IProjectSupportRepository projectSupportRepository,
        IProjectEnrichmentService projectEnrichmentService,
        IProjectAdviserService projectAdviserService,
        IUserActivityRepository userActivityRepository,
        IUserSortService userSortService,
        IHtmlSanitizerService htmlSanitizerService)
    {
        _projectRepository = projectRepository;
        _projectSupportRepository = projectSupportRepository;
        _projectEnrichmentService = projectEnrichmentService;
        _projectAdviserService = projectAdviserService;
        _userActivityRepository = userActivityRepository;
        _userSortService = userSortService;
        _htmlSanitizerService = htmlSanitizerService;
    }

    public async Task<ProjectSupport> Handle(UpdateProjectSupport request, CancellationToken cancellationToken)
    {
        await Sanitize(request);

        request.Description = _htmlSanitizerService.SanitizeHtml(request.Description);

        var original = await _projectSupportRepository.RequireProjectSupport(request.Id);

        var project = await _projectRepository.GetProjectByProjectSupportGroupId(original.ProjectSupportGroupId);
        if (project != null && project.Locked)
            throw new InvalidOperationException("Unable to edit a locked project");

        var updated = original.Copy();
        updated.Date = request.Date;
        updated.TeamIds = request.TeamIds;
        updated.AdviserIds = request.AdviserIds;
        updated.SupportProvidedIds = request.SupportProvidedIds;
        updated.Description = request.Description;
        updated.WorkTimeInHours = request.WorkTimeInHours;
        updated.SupportRequestedTeamId = request.SupportRequestedTeamId;
        updated.IsPrePostAwardId = request.IsPrePostAwardId;
        updated.Files = request.Files;

        // Calculated
        updated.CalculatedMinutes = (int)(updated.WorkTimeInHours * 60) * updated.AdviserIds.Count;

        if (updated.SupportRequestedTeamId != original.SupportRequestedTeamId)
        {
            updated.SupportRequestedAt = null;
            updated.SupportRequestedCompletedAt = null;
            updated.SupportRequestedCompletedDate = null;
            updated.SupportRequestedCompletedByUserId = null;
            updated.SupportRequestedCompletedNotes = String.Empty;

            if (updated.SupportRequestedTeamId != null)
                updated.SupportRequestedAt = DateOnly.FromDateTime(DateTime.Today);
        }

        var sendTeamSupportRequestedEmail =
            updated.SupportRequestedTeamId.HasValue && updated.SupportRequestedTeamId != original.SupportRequestedTeamId;

        var cb = new RepositoryCommandBuffer();
        {
            original = await _projectSupportRepository.UpdateProjectSupport(updated: updated, original: original, commandBuffer: cb);
            await SyncroniseUserActivity(project, original, cb);
        }
        await cb.Execute();

        if (project != null)
        {
            var updatedProject = project.Copy();
            await _projectEnrichmentService.CalculateSupportMetricsForProject(updatedProject);
            await _projectRepository.UpdateProject(original: project, updated: updatedProject);
        }

        if (project != null && sendTeamSupportRequestedEmail)
            await _projectAdviserService.SendTeamSupportRequestedEmail(
                project: project,
                supportTeamId: updated.SupportRequestedTeamId!.Value,
                requestingTeamId: updated.TeamIds.FirstOrDefault());

        TeamSupportProvider.FlushSupportMetrics();
        TeamActivityProvider.FlushTeamActivityMetrics();

        return original;
    }

    async Task SyncroniseUserActivity(Project? project, ProjectSupport projectSupport, RepositoryCommandBuffer cb)
    {
        var existingUserActivity = await _userActivityRepository.SelectUserActivityByProjectSupportId(projectSupport.Id);

        foreach (var userActivity in existingUserActivity)
        {
            if (projectSupport.AdviserIds.Contains(userActivity.UserId))
            {
                // Update
                var updated = userActivity.Copy();
                updated.Date = projectSupport.Date;
                updated.Description = projectSupport.Description;
                updated.WorkTimeInHours = projectSupport.WorkTimeInHours;
                updated.ProjectSupportProvidedIds = projectSupport.SupportProvidedIds;

                await _userActivityRepository.UpdateUserActivity(updated: updated, original: userActivity, commandBuffer: cb);
            }
            else
            {
                // Delete
                await _userActivityRepository.DeleteUserActivity(userActivity.Id, commandBuffer: cb);
            }
        }

        // Insert
        {
            foreach (var adviserId in projectSupport.AdviserIds)
            {
                if (!existingUserActivity.Any(p => p.UserId == adviserId))
                {
                    await _userActivityRepository.InsertUserActivity(new UserActivity
                    {
                        UserId = adviserId,
                        Date = projectSupport.Date,
                        Description = projectSupport.Description,
                        WorkTimeInHours = projectSupport.WorkTimeInHours,

                        ProjectSupportId = projectSupport.Id,
                        ProjectPrefixedNumber = project == null ? String.Empty : project.PrefixedNumber,
                        ProjectSupportProvidedIds = projectSupport.SupportProvidedIds
                    }, cb);
                }
            }
        }
    }

    public async Task Sanitize(UpdateProjectSupport request)
    {
        if (request.WorkTimeInHours < 0)
            request.WorkTimeInHours = 0;
        if (request.WorkTimeInHours > 8)
            request.WorkTimeInHours = 8;

        request.AdviserIds = await _userSortService.SortUserIds(request.AdviserIds);
    }
}