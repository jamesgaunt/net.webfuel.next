using MediatR;

namespace Webfuel.Domain
{
    public class DeleteProjectSupport : IRequest
    {
        public required Guid Id { get; set; }
    }

    internal class DeleteProjectSupportHandler : IRequestHandler<DeleteProjectSupport>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectSupportRepository _projectSupportRepository;
        private readonly IProjectEnrichmentService _projectEnrichmentService;
        private readonly IUserActivityRepository _userActivityRepository;

        public DeleteProjectSupportHandler(
            IProjectRepository projectRepository,
            IProjectSupportRepository projectSupportRepository,
            IProjectEnrichmentService projectEnrichmentService,
            IUserActivityRepository userActivityRepository)
        {
            _projectRepository = projectRepository;
            _projectSupportRepository = projectSupportRepository;
            _projectEnrichmentService = projectEnrichmentService;
            _userActivityRepository = userActivityRepository;
        }

        public async Task Handle(DeleteProjectSupport request, CancellationToken cancellationToken)
        {
            var existing = await _projectSupportRepository.GetProjectSupport(request.Id);
            if (existing == null)
                return;

            var project = await _projectRepository.RequireProject(existing.ProjectId);
            if (project.Locked)
                throw new InvalidOperationException("Unable to edit a locked project");

            var cb = new RepositoryCommandBuffer();
            {
                await SyncroniseUserActivity(request.Id, cb);
                await _projectSupportRepository.DeleteProjectSupport(request.Id, cb);
            }
            await cb.Execute();

            {
                var updatedProject = project.Copy();
                await _projectEnrichmentService.CalculateSupportMetricsForProject(updatedProject);
                await _projectRepository.UpdateProject(original: project, updated: updatedProject);
            }

            TeamSupportProvider.FlushSupportMetrics();
        }

        async Task SyncroniseUserActivity(Guid projectSupportId, RepositoryCommandBuffer cb)
        {
            var existingUserActivity = await _userActivityRepository.SelectUserActivityByProjectSupportId(projectSupportId);

            foreach (var userActivity in existingUserActivity)
                await _userActivityRepository.DeleteUserActivity(userActivity.Id, cb);
        }
    }
}
