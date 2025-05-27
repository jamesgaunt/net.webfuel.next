using MediatR;

namespace Webfuel.Domain;

public class UncompleteProjectSupport : IRequest<ProjectSupport>
{
    public required Guid Id { get; set; }
}

internal class UncompleteProjectSupportHandler : IRequestHandler<UncompleteProjectSupport, ProjectSupport>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectSupportRepository _projectSupportRepository;
    private readonly IProjectEnrichmentService _projectEnrichmentService;
    private readonly IIdentityAccessor _identityAccessor;

    public UncompleteProjectSupportHandler(
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

    public async Task<ProjectSupport> Handle(UncompleteProjectSupport request, CancellationToken cancellationToken)
    {
        var existing = await _projectSupportRepository.RequireProjectSupport(request.Id);
        if (existing.SupportRequestedCompletedAt == null)
            throw new InvalidOperationException("The specified support is not marked as completed");

        var project = await _projectRepository.RequireProject(existing.ProjectId);
        if (project.Locked)
            throw new InvalidOperationException("Unable to edit a locked project");

        var updated = existing.Copy();

        updated.SupportRequestedCompletedAt = null;
        updated.SupportRequestedCompletedDate = null;
        updated.SupportRequestedCompletedNotes = String.Empty;
        updated.SupportRequestedCompletedByUserId = null;

        TeamSupportProvider.FlushSupportMetrics();

        updated = await _projectSupportRepository.UpdateProjectSupport(updated: updated, original: existing);

        {
            var updatedProject = project.Copy();
            await _projectEnrichmentService.CalculateSupportMetricsForProject(updatedProject);
            await _projectRepository.UpdateProject(original: project, updated: updatedProject);
        }

        return updated;
    }
}