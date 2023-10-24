using MediatR;

namespace Webfuel.Domain
{
    internal class UpdateUserActivityHandler : IRequestHandler<UpdateUserActivity, UserActivity>
    {
        private readonly IUserActivityRepository _userActivityRepository;

        public UpdateUserActivityHandler(IUserActivityRepository userActivityRepository)
        {
            _userActivityRepository = userActivityRepository;
        }

        public async Task<UserActivity> Handle(UpdateUserActivity request, CancellationToken cancellationToken)
        {
            var userActivity = await _userActivityRepository.RequireUserActivity(request.Id);

            var updated = userActivity.Copy();

            userActivity.Date = request.Date;
            userActivity.WorkActivityId = request.WorkActivityId;
            userActivity.Description = request.Description;

            return await _userActivityRepository.UpdateUserActivity(updated: updated, original: userActivity);
        }
    }
}