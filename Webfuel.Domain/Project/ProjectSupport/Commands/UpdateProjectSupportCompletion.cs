using MediatR;

namespace Webfuel.Domain;

public class UpdateProjectSupportCompletion : IRequest<ProjectSupport>
{
    public required Guid Id { get; set; }

    public required DateOnly? SupportRequestedCompletedDate { get; set; }

    public required string SupportRequestedCompletedNotes { get; set; }
}

internal class UpdateProjectSupportCompletionHandler : IRequestHandler<UpdateProjectSupportCompletion, ProjectSupport>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectSupportRepository _projectSupportRepository;
    private readonly IProjectEnrichmentService _projectEnrichmentService;
    private readonly IUserActivityRepository _userActivityRepository;
    private readonly IUserSortService _userSortService;

    public UpdateProjectSupportCompletionHandler(
        IProjectRepository projectRepository,
        IProjectSupportRepository projectSupportRepository,
        IProjectEnrichmentService projectEnrichmentService,
        IUserActivityRepository userActivityRepository,
        IUserSortService userSortService)
    {
        _projectRepository = projectRepository;
        _projectSupportRepository = projectSupportRepository;
        _projectEnrichmentService = projectEnrichmentService;
        _userActivityRepository = userActivityRepository;
        _userSortService = userSortService;
    }

    public async Task<ProjectSupport> Handle(UpdateProjectSupportCompletion request, CancellationToken cancellationToken)
    {
        var projectSupport = await _projectSupportRepository.RequireProjectSupport(request.Id);

        var project = await _projectRepository.GetProjectByProjectSupportGroupId(projectSupport.ProjectSupportGroupId);
        if (project != null && project.Locked)
            throw new InvalidOperationException("Unable to edit a locked project");

        var updated = projectSupport.Copy();
        updated.SupportRequestedCompletedDate = request.SupportRequestedCompletedDate;
        updated.SupportRequestedCompletedNotes = request.SupportRequestedCompletedNotes;

        projectSupport = await _projectSupportRepository.UpdateProjectSupport(updated: updated, original: projectSupport);

        if (project != null)
        {
            var updatedProject = project.Copy();
            await _projectEnrichmentService.CalculateSupportMetricsForProject(updatedProject);
            await _projectRepository.UpdateProject(original: project, updated: updatedProject);
        }

        TeamSupportProvider.FlushSupportMetrics();

        return projectSupport;
    }
}