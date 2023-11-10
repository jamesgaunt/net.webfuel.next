using MediatR;

namespace Webfuel.Domain
{
    internal class UpdateSupportTeamHandler : IRequestHandler<UpdateSupportTeam, SupportTeam>
    {
        private readonly ISupportTeamRepository _supportTeamRepository;

        public UpdateSupportTeamHandler(ISupportTeamRepository supportTeamRepository)
        {
            _supportTeamRepository = supportTeamRepository;
        }

        public async Task<SupportTeam> Handle(UpdateSupportTeam request, CancellationToken cancellationToken)
        {
            var original = await _supportTeamRepository.RequireSupportTeam(request.Id);

            var updated = original.Copy();
            updated.Name = request.Name;

            return await _supportTeamRepository.UpdateSupportTeam(original: original, updated: updated); 
        }
    }
}
