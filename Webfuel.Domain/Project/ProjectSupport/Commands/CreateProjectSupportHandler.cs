using MediatR;
using Microsoft.Identity.Client;

namespace Webfuel.Domain
{
    internal class CreateProjectSupportHandler : IRequestHandler<CreateProjectSupport, ProjectSupport>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectSupportRepository _projectSupportRepository;
        private readonly IUserActivityRepository _userActivityRepository;

        public CreateProjectSupportHandler(
            IProjectRepository projectRepository,
            IProjectSupportRepository projectSupportRepository,
            IUserActivityRepository userActivityRepository)
        {
            _projectRepository = projectRepository;
            _projectSupportRepository = projectSupportRepository;
            _userActivityRepository = userActivityRepository;
        }

        public async Task<ProjectSupport> Handle(CreateProjectSupport request, CancellationToken cancellationToken)
        {
            var projectSupport = new ProjectSupport();

            projectSupport.ProjectId = request.ProjectId;
            projectSupport.Date = request.Date ?? DateOnly.FromDateTime(DateTime.Now);
            projectSupport.TeamIds = request.TeamIds;
            projectSupport.AdviserIds = request.AdviserIds;
            projectSupport.SupportProvidedIds = request.SupportProvidedIds;
            projectSupport.Description = request.Description;

            var cb = new RepositoryCommandBuffer();
            {
                projectSupport = await _projectSupportRepository.InsertProjectSupport(projectSupport, cb);
                await SyncroniseUserActivity(projectSupport, cb);
            }
            await cb.Execute();

            return projectSupport;
        }

        async Task SyncroniseUserActivity(ProjectSupport projectSupport, RepositoryCommandBuffer cb)
        {
            var project = await _projectRepository.RequireProject(projectSupport.ProjectId);

            foreach (var adviserId in projectSupport.AdviserIds)
            {
                await _userActivityRepository.InsertUserActivity(new UserActivity
                {
                    UserId = adviserId,
                    Date = projectSupport.Date,
                    Description = projectSupport.Description,

                    ProjectId = projectSupport.ProjectId,
                    ProjectSupportId = projectSupport.Id,
                    ProjectPrefixedNumber = project.PrefixedNumber,
                    ProjectSupportProvidedIds = projectSupport.SupportProvidedIds
                }, cb);
            }
        }
    }
}