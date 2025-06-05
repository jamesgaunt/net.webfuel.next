using Webfuel.Domain;
using Webfuel.Domain.StaticData;

namespace Webfuel.Tools.ConsoleApp;

public interface ISupportRequestFix
{
    Task FixSupportRequests();
}

[Service(typeof(ISupportRequestFix))]
internal class SupportRequestFix : ISupportRequestFix
{
    private readonly ISupportRequestRepository _supportRequestRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IStaticDataService _staticDataService;
    private readonly IUserSortService _userSortService;

    public SupportRequestFix(
        ISupportRequestRepository supportRequestRepository,
        IProjectRepository projectRepository,
        IStaticDataService staticDataService,
        IUserSortService userSortService)
    {
        _supportRequestRepository = supportRequestRepository;
        _projectRepository = projectRepository;
        _staticDataService = staticDataService;
        _userSortService = userSortService;
    }

    public async Task FixSupportRequests()
    {
        var projects = await _projectRepository.SelectProject();
        var supportRequests = await _supportRequestRepository.SelectSupportRequest();

        foreach (var original in supportRequests)
        {
            if (original.StatusId != SupportRequestStatusEnum.ClosedOutOfRemit)
                continue;

            var updated = original.Copy();

            updated.StatusId = SupportRequestStatusEnum.ClosedOutOfRemit;

            await _supportRequestRepository.UpdateSupportRequest(original: original, updated: updated);
        }
    }
}
