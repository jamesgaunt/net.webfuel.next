using MediatR;

namespace Webfuel.Domain
{
    internal class GetSupportTeamHandler : IRequestHandler<GetSupportTeam, SupportTeam?>
    {
        private readonly ISupportTeamRepository _supportTeamRepository;

        public GetSupportTeamHandler(ISupportTeamRepository supportTeamRepository)
        {
            _supportTeamRepository = supportTeamRepository;
        }

        public async Task<SupportTeam?> Handle(GetSupportTeam request, CancellationToken cancellationToken)
        {
            return await _supportTeamRepository.GetSupportTeam(request.Id);
        }
    }
}
