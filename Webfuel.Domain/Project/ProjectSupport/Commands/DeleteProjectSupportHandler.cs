using MediatR;

namespace Webfuel.Domain
{
    internal class DeleteProjectSupportHandler : IRequestHandler<DeleteProjectSupport>
    {
        private readonly IProjectSupportRepository _projectSupportRepository;
        private readonly IUserActivityRepository _userActivityRepository;

        public DeleteProjectSupportHandler(IProjectSupportRepository projectSupportRepository, IUserActivityRepository userActivityRepository)
        {
            _projectSupportRepository = projectSupportRepository;
            _userActivityRepository = userActivityRepository;
        }

        public async Task Handle(DeleteProjectSupport request, CancellationToken cancellationToken)
        {
            var cb = new RepositoryCommandBuffer();
            {
                await SyncroniseUserActivity(request.Id, cb);
                await _projectSupportRepository.DeleteProjectSupport(request.Id, cb);
            }
            await cb.Execute();
        }

        async Task SyncroniseUserActivity(Guid projectSupportId, RepositoryCommandBuffer cb)
        {
            var existingUserActivity = await _userActivityRepository.SelectUserActivityByProjectSupportId(projectSupportId);

            foreach (var userActivity in existingUserActivity)
                await _userActivityRepository.DeleteUserActivity(userActivity.Id, cb);
        }
    }
}
