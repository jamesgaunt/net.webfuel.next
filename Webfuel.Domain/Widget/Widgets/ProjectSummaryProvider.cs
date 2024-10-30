using Azure.Core.Serialization;
using DocumentFormat.OpenXml.Drawing;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain;


public interface IProjectSummaryProvider: IWidgetProvider
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

    public Task<Widget> InitialiseWidget(Widget widget)
    {
        widget.HeaderText = "Project Summary";
        widget.DataJson = SafeJsonSerializer.Serialize(new DashboardMetrics());
        return Task.FromResult(widget);
    }

    public async Task<WidgetTaskStatus> BeginProcessing(WidgetTask task)
    {
        if (task.Widget.DataVersion == VERSION && task.Widget.DataTimestamp > GlobalTimestamp)
            return WidgetTaskStatus.Complete;

        var data = await GenerateData();

        task.Widget.DataJson = SafeJsonSerializer.Serialize(data);
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

        // Last 7 days
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var query = new Query();
            query.All(x =>
            {
                x.GreaterThanOrEqual(nameof(Project.DateOfRequest), today.AddDays(-6), true);
                x.LessThanOrEqual(nameof(Project.DateOfRequest), today, true);
            });
            var queryResult = await _projectRepository.QueryProject(query, selectItems: false, countTotal: true);

            result.Add(new DashboardMetric
            {
                Name = "Last 7 days",
                Count = queryResult.TotalCount,
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
