using MediatR;

namespace Webfuel.Domain
{
    internal class DeleteSupportTeamHandler : IRequestHandler<DeleteSupportTeam>
    {
        private readonly ISupportTeamRepository _supportTeamRepository;

        public DeleteSupportTeamHandler(ISupportTeamRepository supportTeamRepository)
        {
            _supportTeamRepository = supportTeamRepository;
        }

        public async Task Handle(DeleteSupportTeam request, CancellationToken cancellationToken)
        {
            await _supportTeamRepository.DeleteSupportTeam(request.Id);
        }
    }
}
