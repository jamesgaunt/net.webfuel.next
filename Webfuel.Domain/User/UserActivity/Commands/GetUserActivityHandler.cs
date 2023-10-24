using FluentValidation;
using MediatR;

namespace Webfuel.Domain
{

    internal class GetUserActivityHandler : IRequestHandler<GetUserActivity, UserActivity?>
    {
        private readonly IUserActivityRepository _userActivityRepository;

        public GetUserActivityHandler(IUserActivityRepository userActivityRepository)
        {
            _userActivityRepository = userActivityRepository;
        }

        public async Task<UserActivity?> Handle(GetUserActivity request, CancellationToken cancellationToken)
        {
            return await _userActivityRepository.GetUserActivity(request.Id);
        }
    }
}
