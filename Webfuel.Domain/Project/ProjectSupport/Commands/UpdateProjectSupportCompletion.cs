using MediatR;

namespace Webfuel.Domain
{
    public class UpdateProjectSupportCompletion : IRequest<ProjectSupport>
    {
        public required Guid Id { get; set; }

        public required string SupportRequestedCompletedNotes { get; set; }
    }

    internal class UpdateProjectSupportCompletionHandler : IRequestHandler<UpdateProjectSupportCompletion, ProjectSupport>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectSupportRepository _projectSupportRepository;
        private readonly IProjectEnrichmentService _projectEnrichmentService;
        private readonly IUserActivityRepository _userActivityRepository;
        private readonly IUserSortService _userSortService;

        public UpdateProjectSupportCompletionHandler(
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

        public async Task<ProjectSupport> Handle(UpdateProjectSupportCompletion request, CancellationToken cancellationToken)
        {
            var projectSupport = await _projectSupportRepository.RequireProjectSupport(request.Id);

            var project = await _projectRepository.RequireProject(projectSupport.ProjectId);
            if (project.Locked)
                throw new InvalidOperationException("Unable to edit a locked project");

            var updated = projectSupport.Copy();
            updated.SupportRequestedCompletedNotes = request.SupportRequestedCompletedNotes;

            projectSupport = await _projectSupportRepository.UpdateProjectSupport(updated: updated, original: projectSupport);

            {
                var updatedProject = project.Copy();
                await _projectEnrichmentService.CalculateSupportMetricsForProject(updatedProject);
                await _projectRepository.UpdateProject(original: project, updated: updatedProject);
            }

            TeamSupportProvider.FlushSupportMetrics();

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
    }
}