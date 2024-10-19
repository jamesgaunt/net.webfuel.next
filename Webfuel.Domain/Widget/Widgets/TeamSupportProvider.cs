using System.Text.Json;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain;

public interface ITeamSupportProvider : IWidgetProvider
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

    // Public API

    public Task<Widget> InitialiseWidget(Widget widget)
    {
        widget.HeaderText = "Team Summary";
        widget.DataJson = SafeJsonSerializer.Serialize(new DashboardMetrics());
        return Task.FromResult(widget);
    }

    public async Task<WidgetTaskStatus> BeginProcessing(WidgetTask task)
    {
        if (task.Widget.DataVersion == VERSION && task.Widget.DataTimestamp > GlobalTimestamp)
            return WidgetTaskStatus.Complete;

        var data = await GenerateData();

        task.Widget.DataJson = JsonSerializer.Serialize(data, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        task.Widget.DataVersion = VERSION;
        task.Widget.DataTimestamp = DateTimeOffset.UtcNow;
        task.Widget = await _widgetRepository.UpdateWidget(task.Widget);

        return WidgetTaskStatus.Complete;
    }

    public Task<WidgetTaskStatus> ContinueProcessing(WidgetTask task)
    {
        return Task.FromResult(WidgetTaskStatus.Complete);
    }

    public Task<bool> AuthoriseAccess()
    {
        return Task.FromResult(true);
    }

    public Task<string> ValidateConfig(string configJson)
    {
        return Task.FromResult(configJson);
    }

    // Generators (real time generation)

    async Task<DashboardMetrics> GenerateData()
    {
        var data = new DashboardMetrics
        {
            Metrics = await GenerateMetrics()
        };
        return data;
    }

    async Task<List<DashboardMetric>> GenerateMetrics()
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
            RouterParams = $"{{ \"show\": \"all\", \"supportTeam\": \"{supportTeam.Id}\" }}",
            BackgroundColor = "#d6bdcc"
        };
    }

    public static void FlushSupportMetrics()
    {
        GlobalTimestamp = DateTimeOffset.UtcNow;
    }

    static DateTimeOffset GlobalTimestamp = DateTimeOffset.UtcNow;
}
