using MediatR;

namespace Webfuel.Domain
{
    internal class DeleteProjectTeamSupportHandler : IRequestHandler<DeleteProjectTeamSupport>
    {
        private readonly IProjectTeamSupportRepository _projectTeamSupportRepository;

        public DeleteProjectTeamSupportHandler(IProjectTeamSupportRepository projectTeamSupportRepository)
        {
            _projectTeamSupportRepository = projectTeamSupportRepository;
        }

        public async Task Handle(DeleteProjectTeamSupport request, CancellationToken cancellationToken)
        {
            await _projectTeamSupportRepository.DeleteProjectTeamSupport(request.Id);
        }
    }
}
