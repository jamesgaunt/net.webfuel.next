using MediatR;

namespace Webfuel.Domain
{
    internal class DeleteUserActivityHandler : IRequestHandler<DeleteUserActivity>
    {
        private readonly IUserActivityRepository _userActivityRepository;

        public DeleteUserActivityHandler(IUserActivityRepository userActivityRepository)
        {
            _userActivityRepository = userActivityRepository;
        }

        public async Task Handle(DeleteUserActivity request, CancellationToken cancellationToken)
        {
            await _userActivityRepository.DeleteUserActivity(request.Id);
        }
    }
}
