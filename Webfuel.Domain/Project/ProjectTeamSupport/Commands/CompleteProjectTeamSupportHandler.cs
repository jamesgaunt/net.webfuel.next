using MediatR;
using Microsoft.AspNetCore.Http;

namespace Webfuel.Domain
{
    internal class CompleteProjectTeamSupportHandler : IRequestHandler<CompleteProjectTeamSupport, ProjectTeamSupport>
    {
        private readonly IProjectTeamSupportRepository _projectTeamSupportRepository;
        private readonly IIdentityAccessor _identityAccessor;

        public CompleteProjectTeamSupportHandler(
            IProjectTeamSupportRepository projectTeamSupportRepository,
            IIdentityAccessor identityAccessor)
        {
            _projectTeamSupportRepository = projectTeamSupportRepository;
            _identityAccessor = identityAccessor;
        }

        public async Task<ProjectTeamSupport> Handle(CompleteProjectTeamSupport request, CancellationToken cancellationToken)
        {
            var existing = await _projectTeamSupportRepository.RequireProjectTeamSupport(request.Id);

            if (existing.CompletedAt.HasValue)
                throw new InvalidOperationException("The specified team support request is already marked as completed");

            var updated = existing.Copy();

            updated.CompletedAt = DateTimeOffset.UtcNow;
            updated.CompletedNotes = request.CompletedNotes;

            if(_identityAccessor.User != null)
                updated.CompletedById = _identityAccessor.User.Id;

            return await _projectTeamSupportRepository.UpdateProjectTeamSupport(updated: updated, original: existing);
        }
    }
}