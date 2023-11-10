using MediatR;

namespace Webfuel.Domain
{
    internal class DeleteSupportTeamUserHandler : IRequestHandler<DeleteSupportTeamUser>
    {
        private readonly ISupportTeamUserRepository _supportTeamUserRepository;

        public DeleteSupportTeamUserHandler(ISupportTeamUserRepository supportTeamUserRepository)
        {
            _supportTeamUserRepository = supportTeamUserRepository;
        }

        public async Task Handle(DeleteSupportTeamUser request, CancellationToken cancellationToken)
        {
            var existing = await _supportTeamUserRepository.GetSupportTeamUserByUserIdAndSupportTeamId(
                userId: request.UserId,
                supportTeamId: request.SupportTeamId);

            if (existing == null)
                return;

            await _supportTeamUserRepository.DeleteSupportTeamUser(existing.Id);
        }
    }
}
