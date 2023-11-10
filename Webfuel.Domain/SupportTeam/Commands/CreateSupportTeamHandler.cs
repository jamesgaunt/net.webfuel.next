using MediatR;

namespace Webfuel.Domain
{
    internal class CreateSupportTeamHandler : IRequestHandler<CreateSupportTeam, SupportTeam>
    {
        private readonly ISupportTeamRepository _supportTeamRepository;

        public CreateSupportTeamHandler(ISupportTeamRepository supportTeamRepository)
        {
            _supportTeamRepository = supportTeamRepository;
        }

        public async Task<SupportTeam> Handle(CreateSupportTeam request, CancellationToken cancellationToken)
        {
            return await _supportTeamRepository.InsertSupportTeam(new SupportTeam { Name = request.Name });
        }
    }
}
