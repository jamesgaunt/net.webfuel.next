using MediatR;

namespace Webfuel.Domain
{
    internal class UpdateProjectSupportHandler : IRequestHandler<UpdateProjectSupport, ProjectSupport>
    {
        private readonly IProjectSupportRepository _projectSupportRepository;
        private readonly IUserActivityRepository _userActivityRepository;
        private readonly IProjectRepository _projectRepository;

        public UpdateProjectSupportHandler(IProjectSupportRepository projectSupportRepository, IUserActivityRepository userActivityRepository, IProjectRepository projectRepository)
        {
            _projectSupportRepository = projectSupportRepository;
            _userActivityRepository = userActivityRepository;
            _projectRepository = projectRepository; 
        }

        public async Task<ProjectSupport> Handle(UpdateProjectSupport request, CancellationToken cancellationToken)
        {
            var projectSupport = await _projectSupportRepository.RequireProjectSupport(request.Id);

            var updated = projectSupport.Copy();
            updated.Date = request.Date;
            updated.Description = request.Description;
            updated.TeamIds = request.TeamIds;
            updated.AdviserIds = request.AdviserIds;
            updated.SupportProvidedIds = request.SupportProvidedIds;

            var cb = new RepositoryCommandBuffer();
            {
                projectSupport = await _projectSupportRepository.UpdateProjectSupport(updated: updated, original: projectSupport, commandBuffer: cb);
                await SyncroniseUserActivity(projectSupport, cb);
            }
            await cb.Execute();

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