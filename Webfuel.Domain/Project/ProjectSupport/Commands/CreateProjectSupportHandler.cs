using MediatR;
using Microsoft.Identity.Client;
using Webfuel.Domain.Dashboard;

namespace Webfuel.Domain
{
    internal class CreateProjectSupportHandler : IRequestHandler<CreateProjectSupport, ProjectSupport>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectSupportRepository _projectSupportRepository;
        private readonly IUserActivityRepository _userActivityRepository;
        private readonly IUserSortService _userSortService;

        public CreateProjectSupportHandler(
            IProjectRepository projectRepository,
            IProjectSupportRepository projectSupportRepository,
            IUserActivityRepository userActivityRepository,
            IUserSortService userSortService)
        {
            _projectRepository = projectRepository;
            _projectSupportRepository = projectSupportRepository;
            _userActivityRepository = userActivityRepository;
            _userSortService = userSortService;
        }

        public async Task<ProjectSupport> Handle(CreateProjectSupport request, CancellationToken cancellationToken)
        {
            await Sanitize(request);

            var projectSupport = new ProjectSupport();

            projectSupport.ProjectId = request.ProjectId;
            projectSupport.Date = request.Date ?? DateOnly.FromDateTime(DateTime.Now);
            projectSupport.TeamIds = request.TeamIds;
            projectSupport.AdviserIds = request.AdviserIds;
            projectSupport.SupportProvidedIds = request.SupportProvidedIds;
            projectSupport.Description = request.Description;
            projectSupport.WorkTimeInHours = request.WorkTimeInHours;
            projectSupport.SupportRequestedTeamId = request.SupportRequestedTeamId;

            var cb = new RepositoryCommandBuffer();
            {
                projectSupport = await _projectSupportRepository.InsertProjectSupport(projectSupport, cb);
                await SyncroniseUserActivity(projectSupport, cb);
            }
            await cb.Execute();

            DashboardService.FlushSupportMetrics();

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
                    WorkTimeInHours = projectSupport.WorkTimeInHours,

                    ProjectId = projectSupport.ProjectId,
                    ProjectSupportId = projectSupport.Id,
                    ProjectPrefixedNumber = project.PrefixedNumber,
                    ProjectSupportProvidedIds = projectSupport.SupportProvidedIds
                }, cb);
            }
        }

        public async Task Sanitize(CreateProjectSupport request)
        {
            if (request.WorkTimeInHours < 0)
                request.WorkTimeInHours = 0;
            if (request.WorkTimeInHours > 8)
                request.WorkTimeInHours = 8;

            request.AdviserIds = await _userSortService.SortUserIds(request.AdviserIds);
        }
    }
}