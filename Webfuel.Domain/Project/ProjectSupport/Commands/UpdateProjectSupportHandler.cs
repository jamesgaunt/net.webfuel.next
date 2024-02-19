using MediatR;
using Webfuel.Domain.Dashboard;

namespace Webfuel.Domain
{
    internal class UpdateProjectSupportHandler : IRequestHandler<UpdateProjectSupport, ProjectSupport>
    {
        private readonly IProjectSupportRepository _projectSupportRepository;
        private readonly IUserActivityRepository _userActivityRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUserSortService _userSortService;

        public UpdateProjectSupportHandler(IProjectSupportRepository projectSupportRepository, IUserActivityRepository userActivityRepository, IProjectRepository projectRepository, IUserSortService userSortService)
        {
            _projectSupportRepository = projectSupportRepository;
            _userActivityRepository = userActivityRepository;
            _projectRepository = projectRepository;
            _userSortService = userSortService;
        }

        public async Task<ProjectSupport> Handle(UpdateProjectSupport request, CancellationToken cancellationToken)
        {
            await Sanitize(request);

            var projectSupport = await _projectSupportRepository.RequireProjectSupport(request.Id);

            var updated = projectSupport.Copy();
            updated.Date = request.Date;
            updated.TeamIds = request.TeamIds;
            updated.AdviserIds = request.AdviserIds;
            updated.SupportProvidedIds = request.SupportProvidedIds;
            updated.Description = request.Description;
            updated.WorkTimeInHours = request.WorkTimeInHours;
            updated.SupportRequestedTeamId = request.SupportRequestedTeamId;
            updated.IsPrePostAwardId = request.IsPrePostAwardId;

            if(updated.SupportRequestedTeamId != projectSupport.SupportRequestedTeamId)
            {
                updated.SupportRequestedCompletedAt = null;
                updated.SupportRequestedCompletedByUserId = null;
                updated.SupportRequestedCompletedNotes = String.Empty;
            }

            var cb = new RepositoryCommandBuffer();
            {
                projectSupport = await _projectSupportRepository.UpdateProjectSupport(updated: updated, original: projectSupport, commandBuffer: cb);
                await SyncroniseUserActivity(projectSupport, cb);
            }
            await cb.Execute();

            DashboardService.FlushSupportMetrics();

            return projectSupport;
        }

        async Task SyncroniseUserActivity(ProjectSupport projectSupport, RepositoryCommandBuffer cb)
        {
            var existingUserActivity = await _userActivityRepository.SelectUserActivityByProjectSupportId(projectSupport.Id);

            foreach (var userActivity in existingUserActivity)
            {
                if (projectSupport.AdviserIds.Contains(userActivity.UserId))
                {
                    // Update
                    var updated = userActivity.Copy();
                    updated.Date = projectSupport.Date;
                    updated.Description = projectSupport.Description;
                    updated.WorkTimeInHours = projectSupport.WorkTimeInHours;
                    updated.ProjectSupportProvidedIds = projectSupport.SupportProvidedIds;

                    await _userActivityRepository.UpdateUserActivity(updated: updated, original: userActivity, commandBuffer: cb);
                }
                else
                {
                    // Delete
                    await _userActivityRepository.DeleteUserActivity(userActivity.Id, commandBuffer: cb);
                }
            }

            // Insert
            {
                string projectPrefixedNumber = existingUserActivity.Count > 0 ? existingUserActivity[0].ProjectPrefixedNumber : String.Empty;

                foreach (var adviserId in projectSupport.AdviserIds)
                {
                    if (!existingUserActivity.Any(p => p.UserId == adviserId))
                    {
                        if(projectPrefixedNumber == String.Empty)
                        {
                            var project = await _projectRepository.RequireProject(projectSupport.ProjectId);
                            projectPrefixedNumber = project.PrefixedNumber;
                        }

                        await _userActivityRepository.InsertUserActivity(new UserActivity
                        {
                            UserId = adviserId,
                            Date = projectSupport.Date,
                            Description = projectSupport.Description,
                            WorkTimeInHours = projectSupport.WorkTimeInHours,

                            ProjectId = projectSupport.ProjectId,
                            ProjectSupportId = projectSupport.Id,
                            ProjectPrefixedNumber = projectPrefixedNumber,
                            ProjectSupportProvidedIds = projectSupport.SupportProvidedIds
                        }, cb);
                    }
                }
            }
        }

        public async Task Sanitize(UpdateProjectSupport request)
        {
            if (request.WorkTimeInHours < 0)
                request.WorkTimeInHours = 0;
            if (request.WorkTimeInHours > 8)
                request.WorkTimeInHours = 8;

            request.AdviserIds = await _userSortService.SortUserIds(request.AdviserIds);
        }
    }
}