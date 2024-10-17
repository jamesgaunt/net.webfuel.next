using MediatR;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain
{
    internal class CreateUserActivityHandler : IRequestHandler<CreateUserActivity, UserActivity>
    {
        private readonly IUserActivityRepository _userActivityRepository;
        private readonly IIdentityAccessor _identityAccessor;

        public CreateUserActivityHandler(
            IUserActivityRepository userActivityRepository, 
            IIdentityAccessor identityAccessor)
        {
            _userActivityRepository = userActivityRepository;
            _identityAccessor = identityAccessor;
        }

        public async Task<UserActivity> Handle(CreateUserActivity request, CancellationToken cancellationToken)
        {
            await Sanitize(request);

            var userActivity = new UserActivity();

            userActivity.UserId = _identityAccessor.User?.Id ?? throw new InvalidOperationException("No current user");
            userActivity.Date = request.Date;
            userActivity.WorkActivityId = request.WorkActivityId;
            userActivity.WorkTimeInHours = request.WorkTimeInHours;
            userActivity.Description = request.Description;

            TeamActivityProvider.FlushTeamActivityMetrics();

            return await _userActivityRepository.InsertUserActivity(userActivity);
        }

        public Task Sanitize(CreateUserActivity request)
        {
            if (request.WorkTimeInHours < 0)
                request.WorkTimeInHours = 0;
            if (request.WorkTimeInHours > 8)
                request.WorkTimeInHours = 8;

            return Task.CompletedTask;
        }
    }
}