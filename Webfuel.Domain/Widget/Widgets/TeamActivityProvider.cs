using DocumentFormat.OpenXml.Office2010.Drawing;
using DocumentFormat.OpenXml.Presentation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using System.Text.Json;
using Webfuel.Domain.StaticData;
using Webfuel.Reporting;
using Webfuel.Terminal;

namespace Webfuel.Domain;

[ApiType]
public class TeamActivityData
{
    public List<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();

    public class TeamMember
    {
        public string Name { get; set; } = String.Empty;

        public decimal ProjectSupportHours { get; set; }

        public decimal UserActivityHours { get; set; }

        public decimal TotalHours { get; set; }

        public decimal? FTE { get; set; }

        public decimal? FTETotalHours { get; set; }
    }
}

[ApiType]
public class TeamActivityConfig
{
    public int Weeks { get; set; } = 2;

    public Guid SupportTeamId { get; set; } = SupportTeamEnum.TriageTeam;
}

public interface ITeamActivityProvider : IWidgetProvider
{
}

[Service(typeof(ITeamActivityProvider))]
internal class TeamActivityProvider : ITeamActivityProvider
{
    const int VERSION = 1;

    private readonly IMediator _mediator;
    private readonly IReportService _reportService;
    private readonly IWidgetRepository _widgetRepository;
    private readonly IReportGeneratorService _reportGeneratorService;
    private readonly IStaticDataService _staticDataService;
    private readonly IServiceProvider _serviceProvider;

    public TeamActivityProvider(
        IMediator mediator,
        IWidgetRepository widgetRepository,
        IReportService reportService,
        IReportGeneratorService reportGeneratorService,
        IStaticDataService staticDataService,
        IServiceProvider serviceProvider)
    {
        _mediator = mediator;
        _reportService = reportService;
        _widgetRepository = widgetRepository;
        _reportGeneratorService = reportGeneratorService;
        _staticDataService = staticDataService;
        _serviceProvider = serviceProvider;
    }

    // Public API

    public Task<Widget> InitialiseWidget(Widget widget)
    {
        widget.HeaderText = "Team Activity";
        widget.DataJson = SafeJsonSerializer.Serialize(new TeamActivityData());
        widget.ConfigJson = SafeJsonSerializer.Serialize(new TeamActivityConfig());
        return Task.FromResult(widget);
    }

    public async Task<WidgetTaskStatus> BeginProcessing(WidgetTask task)
    {
        if (task.Widget.DataVersion == VERSION && task.Widget.DataTimestamp > GlobalTimestamp)
            return WidgetTaskStatus.Complete;

        task.Widget.ConfigJson = await ValidateConfig(task.Widget.ConfigJson);
        var config = SafeJsonSerializer.Deserialize<TeamActivityConfig>(task.Widget.ConfigJson);

        var sDate = DateOnly.FromDateTime(DateTime.Today).AddDays(-7 * config.Weeks);
        var eDate = DateOnly.FromDateTime(DateTime.Today);

        var report = await _reportService.GetDefaultNamedReport("Team Activity Report", ReportProviderEnum.CustomReport);

        var runReport = new RunReport
        {
            ReportId = report.Id,
            TypedArguments = new TeamActivityReportArguments
            {
                SupportTeamId = config.SupportTeamId,
                StartDate = sDate,
                EndDate = eDate
            }
        };

        try
        {
            task.State = await _mediator.Send(runReport);
        }
        catch
        {
            return WidgetTaskStatus.Cancelled;
        }

        return WidgetTaskStatus.Processing;
    }

    public async Task<WidgetTaskStatus> ContinueProcessing(WidgetTask task)
    {
        if (task.State is not ReportStep reportStep)
            throw new InvalidOperationException("Widget task does not contain valid state");

        // Generate the Report Task

        try
        {
            task.State = reportStep = await _reportGeneratorService.GenerateReport(reportStep.TaskId);
            if (!reportStep.Complete)
                return WidgetTaskStatus.Processing;
        }
        catch
        {
            return WidgetTaskStatus.Cancelled;
        }

        if (_reportGeneratorService.ExtractReportData(reportStep.TaskId) is not TeamActivityReportData reportData)
            throw new InvalidOperationException("Report did not return the expected data type");

        // We have the final report data

        var config = SafeJsonSerializer.Deserialize<TeamActivityConfig>(task.Widget.ConfigJson);

        task.Widget.DataJson = SafeJsonSerializer.Serialize(MapReportData(reportData));
        task.Widget.DataVersion = VERSION;
        task.Widget.DataTimestamp = DateTimeOffset.UtcNow;

        var supportTeam = await _staticDataService.GetSupportTeam(config.SupportTeamId);

        task.Widget.HeaderText = (supportTeam?.Name ?? "ERROR: Invalid Team");
        task.Widget.HeaderText += $" ({config.Weeks} week{(config.Weeks == 1 ? "" : "s")})";

        task.Widget = await _widgetRepository.UpdateWidget(task.Widget);

        return WidgetTaskStatus.Complete;
    }

    public async Task<bool> AuthoriseAccess()
    {
        var identityAccessor = _serviceProvider.GetRequiredService<IIdentityAccessor>();
        if (identityAccessor.User == null)
            return false;

        if (identityAccessor.Claims.Developer || identityAccessor.Claims.Administrator)
            return true;

        var supportTeams = await _serviceProvider.GetRequiredService<ISupportTeamUserRepository>().SelectSupportTeamUserByUserId(identityAccessor.User.Id);
        if (supportTeams.Any(p => p.IsTeamLead))
            return true;

        return false;
    }

    public Task<string> ValidateConfig(string configJson)
    {
        var config = SafeJsonSerializer.Deserialize<TeamActivityConfig>(configJson);

        if (config.Weeks < 1)
            config.Weeks = 1;
        if (config.Weeks > 8)
            config.Weeks = 8;

        return Task.FromResult(SafeJsonSerializer.Serialize(config));
    }

    // Implementation

    TeamActivityData MapReportData(TeamActivityReportData reportData)
    {
        // Map data from the report into data for the widget

        var data = new TeamActivityData();
        foreach (var user in reportData.Users)
        {
            var totalHours = user.ProjectSupportHours + user.UserActivityHours;
            var fte = user.User.FullTimeEquivalentForRSS;

            data.TeamMembers.Add(new TeamActivityData.TeamMember
            {
                Name = user.User.FullName,
                ProjectSupportHours = user.ProjectSupportHours,
                UserActivityHours = user.UserActivityHours,
                TotalHours = totalHours,
                FTE = fte,
                FTETotalHours = fte > 0 ? totalHours / fte : null
            });
        }
        return data;
    }

    // Cache

    public static void FlushTeamActivityMetrics()
    {
        GlobalTimestamp = DateTimeOffset.UtcNow;
    }

    static DateTimeOffset GlobalTimestamp = DateTimeOffset.UtcNow;
}
