using MediatR;
using Microsoft.AspNetCore.Http;

namespace Webfuel.Domain
{
    internal class UpdateProjectTeamSupportHandler : IRequestHandler<UpdateProjectTeamSupport, ProjectTeamSupport>
    {
        private readonly IProjectTeamSupportRepository _projectTeamSupportRepository;

        public UpdateProjectTeamSupportHandler(
            IProjectTeamSupportRepository projectTeamSupportRepository)
        {
            _projectTeamSupportRepository = projectTeamSupportRepository;
        }

        public async Task<ProjectTeamSupport> Handle(UpdateProjectTeamSupport request, CancellationToken cancellationToken)
        {
            var existing = await _projectTeamSupportRepository.RequireProjectTeamSupport(request.Id);

            var updated = existing.Copy();

            updated.CreatedNotes = request.CreatedNotes;
            updated.CompletedNotes = request.CompletedNotes;

            return await _projectTeamSupportRepository.UpdateProjectTeamSupport(updated: updated, original: existing);
        }
    }
}