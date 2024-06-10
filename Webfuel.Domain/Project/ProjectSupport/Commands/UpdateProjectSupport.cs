using MediatR;
using Webfuel.Domai;
using Webfuel.Domain.Dashboard;

namespace Webfuel.Domain
{
    public class UpdateProjectSupport : IRequest<ProjectSupport>
    {
        public required Guid Id { get; set; }

        public DateOnly Date { get; set; }

        public required List<Guid> TeamIds { get; set; }

        public required List<Guid> AdviserIds { get; set; }

        public required List<Guid> SupportProvidedIds { get; set; }

        public string Description { get; set; } = String.Empty;

        public required Decimal WorkTimeInHours { get; set; }

        public Guid? SupportRequestedTeamId { get; set; }

        public required Guid IsPrePostAwardId { get; set; }
    }

    internal class UpdateProjectSupportHandler : IRequestHandler<UpdateProjectSupport, ProjectSupport>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectSupportRepository _projectSupportRepository;
        private readonly IProjectEnrichmentService _projectEnrichmentService;
        private readonly IUserActivityRepository _userActivityRepository;
        private readonly IUserSortService _userSortService;

        public UpdateProjectSupportHandler(
            IProjectRepository projectRepository, 
            IProjectSupportRepository projectSupportRepository,
            IProjectEnrichmentService projectEnrichmentService,
            IUserActivityRepository userActivityRepository, 
            IUserSortService userSortService)
        {
            _projectRepository = projectRepository;
            _projectSupportRepository = projectSupportRepository;
            _projectEnrichmentService = projectEnrichmentService;
            _userActivityRepository = userActivityRepository;
            _userSortService = userSortService;
        }

        public async Task<ProjectSupport> Handle(UpdateProjectSupport request, CancellationToken cancellationToken)
        {
            await Sanitize(request);

            var projectSupport = await _projectSupportRepository.RequireProjectSupport(request.Id);

            var project = await _projectRepository.RequireProject(projectSupport.ProjectId);
            if (project.Locked)
                throw new InvalidOperationException("Unable to edit a locked project");

            var updated = projectSupport.Copy();
            updated.Date = request.Date;
            updated.TeamIds = request.TeamIds;
            updated.AdviserIds = request.AdviserIds;
            updated.SupportProvidedIds = request.SupportProvidedIds;
            updated.Description = request.Description;
            updated.WorkTimeInHours = request.WorkTimeInHours;
            updated.SupportRequestedTeamId = request.SupportRequestedTeamId;
            updated.IsPrePostAwardId = request.IsPrePostAwardId;

            // Calculated
            updated.CalculatedMinutes = (int)(updated.WorkTimeInHours * 60) * updated.AdviserIds.Count;

            if (updated.SupportRequestedTeamId != projectSupport.SupportRequestedTeamId)
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

            {
                var updatedProject = project.Copy();
                await _projectEnrichmentService.CalculateSupportMetricsForProject(updatedProject);
                await _projectRepository.UpdateProject(original: project, updated: updatedProject);
            }

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