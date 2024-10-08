using Webfuel.Domain;
using Webfuel.Domain.StaticData;

namespace Webfuel.Tools.ConsoleApp
{
    public interface IProjectFix
    {
        Task FixProjects();
    }

    [Service(typeof(IProjectFix))]
    internal class ProjectFix : IProjectFix
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IStaticDataService _staticDataService;
        private readonly IProjectEnrichmentService _projectEnrichmentService;

        public ProjectFix(
            IProjectRepository projectRepository,
            IStaticDataService staticDataService,
            IProjectEnrichmentService projectEnrichmentService)
        {
            _projectRepository = projectRepository;
            _staticDataService = staticDataService;
            _projectEnrichmentService = projectEnrichmentService;
        }

        public async Task FixProjects()
        {
            var projects = await _projectRepository.SelectProject();

            foreach (var original in projects)
            {
                var updated = original.Copy();

                await _projectEnrichmentService.CalculateSupportMetricsForProject(updated);

                await _projectRepository.UpdateProject(original: original, updated: updated);
            }
        }
    }
}
