using MediatR;

namespace Webfuel.Domain;

public class CompleteProjectSupport : IRequest<ProjectSupport>
{
    public required Guid Id { get; set; }

    public required DateOnly? SupportRequestedCompletedDate { get; set; }

    public required string SupportRequestedCompletedNotes { get; set; }
}

internal class CompleteProjectSupportHandler : IRequestHandler<CompleteProjectSupport, ProjectSupport>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectSupportRepository _projectSupportRepository;
    private readonly IProjectEnrichmentService _projectEnrichmentService;
    private readonly IIdentityAccessor _identityAccessor;

    public CompleteProjectSupportHandler(
        IProjectRepository projectRepository,
        IProjectSupportRepository projectSupportRepository,
        IProjectEnrichmentService projectEnrichmentService,
        IIdentityAccessor identityAccessor)
    {
        _projectRepository = projectRepository;
        _projectSupportRepository = projectSupportRepository;
        _projectEnrichmentService = projectEnrichmentService;
        _identityAccessor = identityAccessor;
    }

    public async Task<ProjectSupport> Handle(CompleteProjectSupport request, CancellationToken cancellationToken)
    {
        var existing = await _projectSupportRepository.RequireProjectSupport(request.Id);
        if (existing.SupportRequestedCompletedAt.HasValue)
            throw new InvalidOperationException("The specified support is already marked as completed");

        var project = await _projectRepository.GetProjectByProjectSupportGroupId(existing.ProjectSupportGroupId);
        if (project != null && project.Locked)
            throw new InvalidOperationException("Unable to edit a locked project");

        var updated = existing.Copy();

        updated.SupportRequestedCompletedAt = DateTimeOffset.UtcNow;
        updated.SupportRequestedCompletedDate = request.SupportRequestedCompletedDate ?? DateOnly.FromDateTime(DateTime.Today);
        updated.SupportRequestedCompletedNotes = request.SupportRequestedCompletedNotes;

        if (_identityAccessor.User != null)
            updated.SupportRequestedCompletedByUserId = _identityAccessor.User.Id;

        TeamSupportProvider.FlushSupportMetrics();

        updated = await _projectSupportRepository.UpdateProjectSupport(updated: updated, original: existing);

        if (project != null)
        {
            var updatedProject = project.Copy();
            await _projectEnrichmentService.CalculateSupportMetricsForProject(updatedProject);
            await _projectRepository.UpdateProject(original: project, updated: updatedProject);
        }

        return updated;
    }
}