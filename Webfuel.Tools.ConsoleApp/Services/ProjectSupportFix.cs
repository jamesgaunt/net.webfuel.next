using Webfuel.Domain;
using Webfuel.Domain.StaticData;

namespace Webfuel.Tools.ConsoleApp;

public interface IProjectSupportFix
{
    Task FixProjectSupports();
}

[Service(typeof(IProjectSupportFix))]
internal class ProjectSupportFix : IProjectSupportFix
{
    private readonly IProjectSupportRepository _projectSupportRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IStaticDataService _staticDataService;
    private readonly IUserSortService _userSortService;

    public ProjectSupportFix(
        IProjectSupportRepository projectSupportRepository,
        IProjectRepository projectRepository,
        IStaticDataService staticDataService,
        IUserSortService userSortService)
    {
        _projectSupportRepository = projectSupportRepository;
        _projectRepository = projectRepository;
        _staticDataService = staticDataService;
        _userSortService = userSortService;
    }

    public async Task FixProjectSupports()
    {
        var projectSupports = await _projectSupportRepository.SelectProjectSupport();

        foreach (var original in projectSupports)
        {
            var updated = original.Copy();

            if (updated.SupportRequestedTeamId.HasValue && updated.SupportRequestedAt == null)
            {
                updated.SupportRequestedAt = updated.Date;
            }

            if (updated.SupportRequestedTeamId.HasValue && updated.SupportRequestedCompletedAt.HasValue && updated.SupportRequestedCompletedDate == null)
            {
                updated.SupportRequestedCompletedDate = DateOnly.FromDateTime(updated.SupportRequestedCompletedAt.Value.DateTime);
            }

            await _projectSupportRepository.UpdateProjectSupport(original: original, updated: updated);
        }
    }
}
