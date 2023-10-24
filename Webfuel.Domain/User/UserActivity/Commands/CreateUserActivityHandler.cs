using MediatR;

namespace Webfuel.Domain
{
    internal class CreateUserActivityHandler : IRequestHandler<CreateUserActivity, UserActivity>
    {
        private readonly IUserActivityRepository _userActivityRepository;

        public CreateUserActivityHandler(IUserActivityRepository userActivityRepository)
        {
            _userActivityRepository = userActivityRepository;
        }

        public async Task<UserActivity> Handle(CreateUserActivity request, CancellationToken cancellationToken)
        {
            var userActivity = new UserActivity();

            userActivity.UserId = request.UserId;
            userActivity.Date = request.Date;
            userActivity.WorkActivityId = request.WorkActivityId;
            userActivity.Description = request.Description;

            return await _userActivityRepository.InsertUserActivity(userActivity);
        }
    }
}