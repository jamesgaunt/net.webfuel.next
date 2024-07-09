using MediatR;

namespace Webfuel.Domain
{
    public class InsertSupportTeamUser : IRequest
    {
        public Guid SupportTeamId { get; set; }

        public Guid UserId { get; set; }
    }

    internal class InsertSupportTeamUserHandler : IRequestHandler<InsertSupportTeamUser>
    {
        private readonly ISupportTeamUserRepository _supportTeamUserRepository;

        public InsertSupportTeamUserHandler(ISupportTeamUserRepository supportTeamUserRepository)
        {
            _supportTeamUserRepository = supportTeamUserRepository;
        }

        public async Task Handle(InsertSupportTeamUser request, CancellationToken cancellationToken)
        {
            var existing = await _supportTeamUserRepository.GetSupportTeamUserByUserIdAndSupportTeamId(
                userId: request.UserId,
                supportTeamId: request.SupportTeamId);

            if (existing != null)
                return;

            await _supportTeamUserRepository.InsertSupportTeamUser(new SupportTeamUser
            {
                UserId = request.UserId,
                SupportTeamId = request.SupportTeamId
            });
        }
    }
}
