using DocumentFormat.OpenXml.Presentation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using System.Text.Json;
using Webfuel.Domain.StaticData;
using Webfuel.Reporting;

namespace Webfuel.Domain;

[ApiType]
public class TeamActivityData
{
    public List<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();

    public class TeamMember
    {
        public required string Name { get; set; }

        public required decimal ProjectSupportHours { get; set; }

        public required decimal UserActivityHours { get; set; }

        public required decimal TotalHours { get; set; }
    }
}

[ApiType]
public class TeamActivityConfig
{
    public Guid? SupportTeamId { get; set; }
}

public interface ITeamActivityProvider : IWidgetDataProvider
{
}

[Service(typeof(ITeamActivityProvider))]
internal class TeamActivityProvider : ITeamActivityProvider
{
    const int VERSION = 1;

    private readonly IMediator _mediator;
    private readonly IWidgetRepository _widgetRepository;
    private readonly IReportGeneratorService _reportGeneratorService;
    private readonly IServiceProvider _serviceProvider;

    public TeamActivityProvider(
        IMediator mediator,
        IWidgetRepository widgetRepository,
        IReportGeneratorService reportGeneratorService,
        IServiceProvider serviceProvider)
    {
        _mediator = mediator;
        _widgetRepository = widgetRepository;
        _reportGeneratorService = reportGeneratorService;
        _serviceProvider = serviceProvider;
    }

    // Public API

    public async Task RefreshTask(WidgetRefreshTask task)
    {
        await RunReport(task);
    }

    public async Task<Widget> ValidateWidget(Widget widget)
    {
        var original = widget.Copy();

        widget.DataJson = SafeJsonSerializer.Cycle<TeamActivityData>(widget.DataJson);
        widget.ConfigJson = SafeJsonSerializer.Cycle<TeamActivityConfig>(widget.ConfigJson);
        widget.HeaderText = "Team Activity";

        return  await _widgetRepository.UpdateWidget(original: original, updated: widget);
    }

    public async Task<Widget> UpdateConfig(Widget widget, string configJson)
    {
        var original = widget.Copy();

        widget.ConfigJson = SafeJsonSerializer.Cycle<TeamActivityConfig>(configJson);
        
        if(widget.ConfigJson != original.ConfigJson)
        {
            await _widgetRepository.UpdateWidget(original: original, updated: widget);
        }

        return widget;
    }

    // Implementation

    async Task RunReport(WidgetRefreshTask task)
    {
        var config = SafeJsonSerializer.Deserialize<TeamActivityConfig>(task.Widget.ConfigJson);

        if (task.State is not ReportStep reportStep)
        {
            // Initialise the Report Task

            var report = await _serviceProvider.GetRequiredService<IReportService>().GetDefaultNamedReport("Team Activity Report", ReportProviderEnum.CustomReport);

            var runReport = new RunReport
            {
                ReportId = report.Id,
                TypedArguments = new TeamActivityReportArguments
                {
                    SupportTeamId = config.SupportTeamId,
                    StartDate = DateOnly.FromDateTime(DateTime.Today).AddDays(-14),
                    EndDate = DateOnly.FromDateTime(DateTime.Today)
                }
            };

            task.State = await _mediator.Send(runReport);
            return;
        }

        // Generate the Report Task

        task.State = reportStep = await _reportGeneratorService.GenerateReport(reportStep.TaskId);

        if (reportStep.Complete)
        {
            if (_reportGeneratorService.ExtractReportData(reportStep.TaskId) is not TeamActivityReportData reportData)
                throw new InvalidOperationException("Report did not return the expected data type");

            // We have the final report data

            task.Widget.DataJson = JsonSerializer.Serialize(MapReportData(reportData), SerializerOptions);
            task.Widget.DataVersion = VERSION;
            task.Widget.DataTimestamp = DateTimeOffset.UtcNow;
            task.Complete = true;
        }
    }

    TeamActivityData MapReportData(TeamActivityReportData reportData)
    {
        // Map data from the report into data for the widget

        var data = new TeamActivityData();
        foreach(var user in reportData.Users)
        {
            data.TeamMembers.Add(new TeamActivityData.TeamMember
            {
                Name = user.User.FullName,
                ProjectSupportHours = user.ProjectSupportHours,
                UserActivityHours = user.UserActivityHours,
                TotalHours = user.ProjectSupportHours + user.UserActivityHours
            });
        }
        return data;
    }

    static JsonSerializerOptions SerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    // Cache

    public static void FlushTeamActivityMetrics()
    {
        GlobalTimestamp = DateTimeOffset.UtcNow;
    }

    static DateTimeOffset GlobalTimestamp = DateTimeOffset.UtcNow;
}
