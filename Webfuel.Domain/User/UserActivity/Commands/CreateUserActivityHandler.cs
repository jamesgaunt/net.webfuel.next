using MediatR;

namespace Webfuel.Domain
{
    internal class CreateUserActivityHandler : IRequestHandler<CreateUserActivity, UserActivity>
    {
        private readonly IUserActivityRepository _userActivityRepository;
        private readonly IIdentityAccessor _identityAccessor;

        public CreateUserActivityHandler(IUserActivityRepository userActivityRepository, IIdentityAccessor identityAccessor)
        {
            _userActivityRepository = userActivityRepository;
            _identityAccessor = identityAccessor;
        }

        public async Task<UserActivity> Handle(CreateUserActivity request, CancellationToken cancellationToken)
        {
            var userActivity = new UserActivity();

            userActivity.UserId = _identityAccessor.User?.Id ?? throw new InvalidOperationException("No current user");
            userActivity.Date = request.Date;
            userActivity.WorkActivityId = request.WorkActivityId;
            userActivity.Description = request.Description;

            return await _userActivityRepository.InsertUserActivity(userActivity);
        }
    }
}