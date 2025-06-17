using MediatR;

namespace Webfuel.Domain;

internal class UpdateUserActivityHandler : IRequestHandler<UpdateUserActivity, UserActivity>
{
    private readonly IUserActivityRepository _userActivityRepository;

    public UpdateUserActivityHandler(
        IUserActivityRepository userActivityRepository)
    {
        _userActivityRepository = userActivityRepository;
    }

    public async Task<UserActivity> Handle(UpdateUserActivity request, CancellationToken cancellationToken)
    {
        await Sanitize(request);

        var userActivity = await _userActivityRepository.RequireUserActivity(request.Id);
        var updated = userActivity.Copy();

        updated.Date = request.Date;
        updated.WorkActivityId = request.WorkActivityId;
        updated.WorkTimeInHours = request.WorkTimeInHours;
        updated.Description = request.Description;

        // Ensure project activity is cleared
        updated.ProjectPrefixedNumber = String.Empty;
        updated.ProjectSupportProvidedIds.Clear();

        TeamActivityProvider.FlushTeamActivityMetrics();

        return await _userActivityRepository.UpdateUserActivity(updated: updated, original: userActivity);
    }

    public Task Sanitize(UpdateUserActivity request)
    {
        if (request.WorkTimeInHours < 0)
            request.WorkTimeInHours = 0;
        if (request.WorkTimeInHours > 8)
            request.WorkTimeInHours = 8;

        return Task.CompletedTask;
    }
}