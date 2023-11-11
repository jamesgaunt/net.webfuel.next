using MediatR;
using Microsoft.AspNetCore.Http;

namespace Webfuel.Domain
{
    internal class CreateProjectTeamSupportHandler : IRequestHandler<CreateProjectTeamSupport, ProjectTeamSupport>
    {
        private readonly IProjectTeamSupportRepository _projectTeamSupportRepository;
        private readonly IIdentityAccessor _identityAccessor;

        public CreateProjectTeamSupportHandler(
            IProjectTeamSupportRepository projectTeamSupportRepository,
            IIdentityAccessor identityAccessor)
        {
            _projectTeamSupportRepository = projectTeamSupportRepository;
            _identityAccessor = identityAccessor;
        }

        public async Task<ProjectTeamSupport> Handle(CreateProjectTeamSupport request, CancellationToken cancellationToken)
        {
            var projectTeamSupport = new ProjectTeamSupport();

            projectTeamSupport.ProjectId = request.ProjectId;
            projectTeamSupport.SupportTeamId = request.SupportTeamId;
            projectTeamSupport.CreatedNotes = request.CreatedNotes;
            projectTeamSupport.CreatedAt = DateTimeOffset.UtcNow;

            if(_identityAccessor.User != null)
                projectTeamSupport.CreatedById = _identityAccessor.User.Id;

            return await _projectTeamSupportRepository.InsertProjectTeamSupport(projectTeamSupport);
        }
    }
}