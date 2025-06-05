using MediatR;

namespace Webfuel.Domain;

public class EnrichProject : IRequest<Project>
{
    public Guid Id { get; set; } = Guid.Empty;
}

internal class EnrichProjectHandler : IRequestHandler<EnrichProject, Project>
{
    private readonly IProjectEnrichmentService _projectEnrichmentService;
    private readonly IProjectRepository _projectRepository;

    public EnrichProjectHandler(IProjectEnrichmentService projectEnrichmentService, IProjectRepository projectRepository)
    {
        _projectEnrichmentService = projectEnrichmentService;
        _projectRepository = projectRepository;
    }

    public async Task<Project> Handle(EnrichProject request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.RequireProject(request.Id);

        var updated = project.Copy();

        await _projectEnrichmentService.CalculateSupportMetricsForProject(updated);
        updated.HeartbeatExecutedAt = DateTimeOffset.UtcNow;

        await _projectRepository.UpdateProject(original: project, updated: updated);

        return updated;
    }
}
