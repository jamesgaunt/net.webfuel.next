using Azure.Core.Serialization;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain;

[ApiType]
public class ProjectSummaryData
{
    public List<DashboardMetric> ProjectMetrics { get; set; } = new List<DashboardMetric>();
}

[ApiType]
public class ProjectSummaryConfig
{
}

public interface IProjectSummaryProvider: IWidgetDataProvider
{
}

[Service(typeof(IProjectSummaryProvider))]
internal class ProjectSummaryProvider : IProjectSummaryProvider
{
    const int VERSION = 1;

    private readonly IWidgetRepository _widgetRepository;
    private readonly IProjectRepository _projectRepository;

    public ProjectSummaryProvider(
        IWidgetRepository widgetRepository,
        IProjectRepository projectRepository)
    {
        _widgetRepository = widgetRepository;
        _projectRepository = projectRepository;
    }

    // Public API

    public async Task RefreshTask(WidgetRefreshTask task)
    {
        var data = await GenerateData();

        task.Widget.DataJson = JsonSerializer.Serialize(data, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        task.Widget.DataVersion = VERSION;
        task.Widget.DataTimestamp = DateTimeOffset.UtcNow;
        task.Complete = true;
    }

    public async Task<Widget> ValidateWidget(Widget widget)
    {
        var original = widget.Copy();

        widget.DataJson = SafeJsonSerializer.Cycle<ProjectSummaryData>(widget.DataJson);
        widget.ConfigJson = SafeJsonSerializer.Cycle<ProjectSummaryConfig>(widget.ConfigJson);
        widget.HeaderText = "Project Summary";

        return await _widgetRepository.UpdateWidget(original: original, updated: widget);
    }

    public Task<Widget> UpdateConfig(Widget widget, string configJson)
    {
        return Task.FromResult(widget);
    }

    // Generators (real time generation)

    async Task<ProjectSummaryData> GenerateData()
    {
        var data = new ProjectSummaryData
        {
            ProjectMetrics = await GenerateProjectMetrics()
        };
        return data;
    }

    async Task<List<DashboardMetric>> GenerateProjectMetrics()
    {
        var result = new List<DashboardMetric>();

        {
            var query = new Query();
            query.Equal(nameof(Project.StatusId), ProjectStatusEnum.Active);
            var queryResult = await _projectRepository.QueryProject(query, selectItems: false, countTotal: true);

            result.Add(new DashboardMetric
            {
                Name = "Active Projects",
                Count = queryResult.TotalCount,
                Icon = "fas fa-books",
                RouterLink = "/project/project-list",
                RouterParams = $"{{ \"show\": \"active\" }}",
                BackgroundColor = "#d6bdcc"
            });
        }

        {
            var query = new Query();
            query.Equal(nameof(Project.StatusId), ProjectStatusEnum.OnHold);
            var queryResult = await _projectRepository.QueryProject(query, selectItems: false, countTotal: true);

            result.Add(new DashboardMetric
            {
                Name = "On Hold Projects",
                Count = queryResult.TotalCount,
                Icon = "fas fa-books",
                RouterLink = "/project/project-list",
                RouterParams = $"{{ \"show\": \"on-hold\" }}",
                BackgroundColor = "#d6bdcc"
            });
        }

        {
            var query = new Query();
            query.Equal(nameof(Project.StatusId), ProjectStatusEnum.SubmittedOnHold);
            var queryResult = await _projectRepository.QueryProject(query, selectItems: false, countTotal: true);

            result.Add(new DashboardMetric
            {
                Name = "Submitted - On Hold Projects",
                Count = queryResult.TotalCount,
                Icon = "fas fa-books",
                RouterLink = "/project/project-list",
                RouterParams = $"{{ \"show\": \"submitted-on-hold\" }}",
                BackgroundColor = "#d6bdcc"
            });
        }

        // All Projects
        {
            result.Add(new DashboardMetric
            {
                Name = "All Projects",
                Count = await _projectRepository.CountProject(),
                Icon = "fas fa-books",
                RouterLink = "/project/project-list",
                RouterParams = $"{{ \"show\": \"all\" }}",
                BackgroundColor = "#d6bdcc"

            });
        }

        return result;
    }

    public static void FlushProjectMetrics()
    {
        GlobalTimestamp = DateTimeOffset.UtcNow;
    }

    static DateTimeOffset GlobalTimestamp = DateTimeOffset.UtcNow;
}
