using MediatR;
using Microsoft.AspNetCore.Http;
using Webfuel.Domain.Dashboard;

namespace Webfuel.Domain
{
    internal class CreateProjectTeamSupportHandler : IRequestHandler<CreateProjectTeamSupport, ProjectTeamSupport>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectTeamSupportRepository _projectTeamSupportRepository;
        private readonly IIdentityAccessor _identityAccessor;

        public CreateProjectTeamSupportHandler(
            IProjectRepository projectRepository,
            IProjectTeamSupportRepository projectTeamSupportRepository,
            IIdentityAccessor identityAccessor)
        {
            _projectRepository = projectRepository;
            _projectTeamSupportRepository = projectTeamSupportRepository;
            _identityAccessor = identityAccessor;
        }

        public async Task<ProjectTeamSupport> Handle(CreateProjectTeamSupport request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.RequireProject(request.ProjectId);

            var projectTeamSupport = new ProjectTeamSupport();

            projectTeamSupport.ProjectId = request.ProjectId;
            projectTeamSupport.ProjectLabel = project.PrefixedNumber + " " + project.LeadApplicantLastName;
            projectTeamSupport.SupportTeamId = request.SupportTeamId;
            projectTeamSupport.CreatedNotes = request.CreatedNotes;
            projectTeamSupport.CreatedAt = DateTimeOffset.UtcNow;

            if(_identityAccessor.User != null)
                projectTeamSupport.CreatedByUserId = _identityAccessor.User.Id;

            DashboardService.FlushSupportTeams();

            return await _projectTeamSupportRepository.InsertProjectTeamSupport(projectTeamSupport);
        }
    }
}