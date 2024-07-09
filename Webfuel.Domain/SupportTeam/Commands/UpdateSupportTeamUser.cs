using MediatR;

namespace Webfuel.Domain
{
    public class UpdateSupportTeamUser : IRequest
    {
        public Guid SupportTeamId { get; set; }

        public Guid UserId { get; set; }

        public bool IsTeamLead { get; set; }
    }

    internal class UpdateSupportTeamUserHandler : IRequestHandler<UpdateSupportTeamUser>
    {
        private readonly ISupportTeamUserRepository _supportTeamUserRepository;

        public UpdateSupportTeamUserHandler(ISupportTeamUserRepository supportTeamUserRepository)
        {
            _supportTeamUserRepository = supportTeamUserRepository;
        }

        public async Task Handle(UpdateSupportTeamUser request, CancellationToken cancellationToken)
        {
            var existing = await _supportTeamUserRepository.GetSupportTeamUserByUserIdAndSupportTeamId(
                userId: request.UserId,
                supportTeamId: request.SupportTeamId);

            if (existing == null)
                return;

            var updated = existing.Copy();
            updated.IsTeamLead = request.IsTeamLead;

            await _supportTeamUserRepository.UpdateSupportTeamUser(original: existing, updated: updated);
        }
    }
}
