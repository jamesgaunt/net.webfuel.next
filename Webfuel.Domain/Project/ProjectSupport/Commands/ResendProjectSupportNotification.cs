using MediatR;
using Webfuel.Domai;
using Webfuel.Domain.Dashboard;

namespace Webfuel.Domain
{
    // Resends the team support request email

    public class ResendProjectSupportNotification : IRequest<ProjectSupport>
    {
        public required Guid Id { get; set; }
    }

    internal class ResendProjectSupportNotificationHandler : IRequestHandler<ResendProjectSupportNotification, ProjectSupport>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectSupportRepository _projectSupportRepository;
        private readonly IProjectAdviserService _projectAdviserService;

        public ResendProjectSupportNotificationHandler(
            IProjectRepository projectRepository,
            IProjectSupportRepository projectSupportRepository,
            IProjectAdviserService projectAdviserService)
        {
            _projectRepository = projectRepository;
            _projectSupportRepository = projectSupportRepository;
            _projectAdviserService = projectAdviserService;
        }

        public async Task<ProjectSupport> Handle(ResendProjectSupportNotification request, CancellationToken cancellationToken)
        {
            var existing = await _projectSupportRepository.RequireProjectSupport(request.Id);
            if (existing.SupportRequestedTeamId == null)
                return existing;

            var project = await _projectRepository.RequireProject(existing.ProjectId);

            await _projectAdviserService.SendTeamSupportRequestedEmail(project: project, supportTeamId: existing.SupportRequestedTeamId.Value);

            return existing;
        }
    }
}
