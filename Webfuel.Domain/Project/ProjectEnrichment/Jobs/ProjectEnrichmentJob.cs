using Webfuel.Jobs;

namespace Webfuel.Domain;

public class ProjectEnrichmentJob : BackgroundJobRunner
{
    private readonly IProjectEnrichmentService _projectEnrichmentService;

    public ProjectEnrichmentJob(IProjectEnrichmentService projectEnrichmentService)
    {
        _projectEnrichmentService = projectEnrichmentService;
    }

    public override async Task<BackgroundJobResult> RunJob()
    {
        return await _projectEnrichmentService.ExecuteHeartbeat();
    }
}
