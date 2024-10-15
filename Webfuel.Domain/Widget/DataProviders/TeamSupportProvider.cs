using System.Text.Json;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain;

[ApiType]
public class TeamSupportData
{
    public List<DashboardMetric> SupportMetrics { get; set; } = new List<DashboardMetric>();
}

public interface ITeamSupportProvider : IWidgetDataProvider
{
}

[Service(typeof(ITeamSupportProvider))]
internal class TeamSupportProvider : ITeamSupportProvider
{
    const int VERSION = 1;

    private readonly IWidgetRepository _widgetRepository;
    private readonly IStaticDataService _staticDataService;
    private readonly IProjectRepository _projectRepository;
    private readonly IIdentityAccessor _identityAccessor;

    public TeamSupportProvider(
        IWidgetRepository widgetRepository,
        IStaticDataService staticDataService,
        IProjectRepository projectRepository,
        IIdentityAccessor identityAccessor)
    {
        _widgetRepository = widgetRepository;
        _staticDataService = staticDataService;
        _projectRepository = projectRepository;
        _identityAccessor = identityAccessor;
    }

    public async Task<WidgetDataResponse> GenerateData(WidgetDataTask task)
    {
        var widget = task.Widget;
        if (widget.CachedDataVersion == VERSION && widget.CachedDataTimestamp > GlobalTimestamp)
            return new WidgetDataResponse { Complete = true, Data = widget.CachedData };

        var data = await GenerateData();

        widget.CachedData = JsonSerializer.Serialize(data, new JsonSerializerOptions {  PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        widget.CachedDataVersion = VERSION;
        widget.CachedDataTimestamp = DateTimeOffset.UtcNow;

        await _widgetRepository.UpdateWidget(widget);

        return new WidgetDataResponse { Complete = true, Data = widget.CachedData };
    }

    // Generators (real time generation)

    async Task<TeamSupportData> GenerateData()
    {
        var data = new TeamSupportData
        {
            SupportMetrics = await GenerateTeamSupportMetrics()
        };
        return data;
    }

    async Task<List<DashboardMetric>> GenerateTeamSupportMetrics()
    {
        var result = new List<DashboardMetric>();
        var staticData = await _staticDataService.GetStaticData();

        foreach (var supportTeam in staticData.SupportTeam)
        {
            result.Add(await GenerateSupportTeamMetric(supportTeam));
        }

        return result;
    }

    async Task<DashboardMetric> GenerateSupportTeamMetric(SupportTeam supportTeam)
    {
        var query = new Query();
        query.Equal(nameof(Project.StatusId), ProjectStatusEnum.Active);
        query.SQL($"EXISTS (SELECT Id FROM [ProjectSupport] AS ps WHERE ps.[ProjectId] = e.Id AND ps.[SupportRequestedTeamId] = '{supportTeam.Id}' AND ps.[SupportRequestedCompletedAt] IS NULL)");
        var queryResult = await _projectRepository.QueryProject(query, selectItems: false, countTotal: true);

        return new DashboardMetric
        {
            Name = supportTeam.Name,
            Count = queryResult.TotalCount,
            Icon = "fas fa-user-headset",
            RouterLink = "/project/project-list",
            RouterParams = $"{{ \"supportTeam\": \"{supportTeam.Id}\" }}",
            BackgroundColor = "#d6bdcc"
        };
    }

    public static void FlushSupportMetrics()
    {
        GlobalTimestamp = DateTimeOffset.UtcNow;
    }

    static DateTimeOffset GlobalTimestamp = DateTimeOffset.UtcNow;
}
