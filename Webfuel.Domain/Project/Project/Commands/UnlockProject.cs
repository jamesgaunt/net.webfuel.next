using MediatR;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain
{
    public class UnlockProject : IRequest<Project>
    {
        public required Guid Id { get; set; }
    }

    internal class UnlockProjectHandler : IRequestHandler<UnlockProject, Project>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectEnrichmentService _projectEnrichmentService;
        private readonly IProjectChangeLogService _projectChangeLogService;

        public UnlockProjectHandler(
            IProjectRepository projectRepository,
            IProjectEnrichmentService projectEnrichmentService,
            IProjectChangeLogService projectChangeLogService)
        {
            _projectRepository = projectRepository;
            _projectEnrichmentService = projectEnrichmentService;
            _projectChangeLogService = projectChangeLogService;
        }

        public async Task<Project> Handle(UnlockProject request, CancellationToken cancellationToken)
        {
            var original = await _projectRepository.RequireProject(request.Id);
            if (original.StatusId == ProjectStatusEnum.Active)
                return original; // Nothing to do

            var updated = original.Copy();

            updated.StatusId = ProjectStatusEnum.Active;

            await _projectEnrichmentService.EnrichProject(updated);
            updated = await _projectRepository.UpdateProject(updated: updated, original: original);

            await _projectChangeLogService.InsertChangeLog(original: original, updated: updated);
            return updated;
        }
    }
}
