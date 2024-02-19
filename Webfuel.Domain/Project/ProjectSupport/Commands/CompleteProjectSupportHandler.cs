using MediatR;
using Microsoft.AspNetCore.Http;
using Webfuel.Domain.Dashboard;

namespace Webfuel.Domain
{
    internal class CompleteProjectSupportHandler : IRequestHandler<CompleteProjectSupport, ProjectSupport>
    {
        private readonly IProjectSupportRepository _projectSupportRepository;
        private readonly IIdentityAccessor _identityAccessor;

        public CompleteProjectSupportHandler(
            IProjectSupportRepository projectSupportRepository,
            IIdentityAccessor identityAccessor)
        {
            _projectSupportRepository = projectSupportRepository;
            _identityAccessor = identityAccessor;
        }

        public async Task<ProjectSupport> Handle(CompleteProjectSupport request, CancellationToken cancellationToken)
        {
            var existing = await _projectSupportRepository.RequireProjectSupport(request.Id);

            if (existing.SupportRequestedCompletedAt.HasValue)
                throw new InvalidOperationException("The specified support is already marked as completed");

            var updated = existing.Copy();

            updated.SupportRequestedCompletedAt = DateTimeOffset.UtcNow;
            updated.SupportRequestedCompletedNotes = request.SupportRequestedCompletedNotes;

            if(_identityAccessor.User != null)
                updated.SupportRequestedCompletedByUserId = _identityAccessor.User.Id;

            DashboardService.FlushSupportMetrics();

            return await _projectSupportRepository.UpdateProjectSupport(updated: updated, original: existing);
        }
    }
}