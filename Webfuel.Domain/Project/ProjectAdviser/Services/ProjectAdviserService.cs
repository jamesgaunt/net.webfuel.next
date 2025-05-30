﻿using Webfuel.Common;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain;

public interface IProjectAdviserService
{
    Task<List<Guid>> SelectProjectAdviserUserIdsByProjectId(Guid projectId);

    Task SyncProjectAdvisersAndSendEmails(Project original, Project updated, List<Guid> projectAdviserUserIds);

    Task SendTeamSupportRequestedEmail(Project project, Guid supportTeamId, Guid requestingTeamId);
}

[Service(typeof(IProjectAdviserService))]
internal class ProjectAdviserService : IProjectAdviserService
{
    private readonly IUserRepository _userRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectAdviserRepository _projectAdviserRepository;
    private readonly IEmailTemplateService _emailTemplateService;
    private readonly IStaticDataService _staticDataService;
    private readonly ISupportTeamUserRepository _supportTeamUserRepository;

    public ProjectAdviserService(
        IUserRepository userRepository,
        IProjectRepository projectRepository,
        IProjectAdviserRepository projectAdviserRepository,
        IEmailTemplateService emailTemplateService,
        IStaticDataService staticDataService,
        ISupportTeamUserRepository supportTeamUserRepository)
    {
        _userRepository = userRepository;
        _projectRepository = projectRepository;
        _projectAdviserRepository = projectAdviserRepository;
        _emailTemplateService = emailTemplateService;
        _staticDataService = staticDataService;
        _supportTeamUserRepository = supportTeamUserRepository;
    }

    public async Task<List<Guid>> SelectProjectAdviserUserIdsByProjectId(Guid projectId)
    {
        return (await _projectAdviserRepository.SelectProjectAdviserByProjectId(projectId))
            .Select(x => x.UserId)
            .ToList();
    }

    public async Task SyncProjectAdvisersAndSendEmails(Project original, Project updated, List<Guid> projectAdviserUserIds)
    {
        // Lead Adviser
        if (updated.LeadAdviserUserId.HasValue && original.LeadAdviserUserId != updated.LeadAdviserUserId)
        {
            var user = await _userRepository.GetUser(updated.LeadAdviserUserId.Value);
            if (user != null)
            {
                await SendLeadAdviserAssignedEmail(updated, user);
            }
        }

        // Support Advisers
        {
            var existing = await _projectAdviserRepository.SelectProjectAdviserByProjectId(updated.Id);

            // Delete
            foreach (var adviser in existing)
            {
                if (!projectAdviserUserIds.Contains(adviser.UserId))
                {
                    await _projectAdviserRepository.DeleteProjectAdviser(adviser.Id);
                }
            }

            // Insert
            foreach (var userId in projectAdviserUserIds)
            {
                if (!existing.Any(x => x.UserId == userId))
                {
                    var user = await _userRepository.GetUser(userId);
                    if (user == null)
                        continue;

                    await _projectAdviserRepository.InsertProjectAdviser(new ProjectAdviser
                    {
                        ProjectId = updated.Id,
                        UserId = userId
                    });

                    await SendSupportAdviserAssignedEmail(updated, user);
                }
            }
        }
    }

    public async Task SendSupportAdviserAssignedEmail(Project project, User user)
    {
        var replacements = new Dictionary<string, string>
        {
            { "PROJECT_TITLE", project.Title },
            { "PROJECT_REFERENCE", project.PrefixedNumber },
            { "ADVISER_NAME", user.FullName },
            { "ADVISER_EMAIL", user.Email }
        };

        await _emailTemplateService.SendEmail("Support Advisor Assigned", replacements);
    }

    public async Task SendLeadAdviserAssignedEmail(Project project, User user)
    {
        var replacements = new Dictionary<string, string>
        {
            { "PROJECT_TITLE", project.Title },
            { "PROJECT_REFERENCE", project.PrefixedNumber },
            { "ADVISER_NAME", user.FullName },
            { "ADVISER_EMAIL", user.Email }
        };

        await _emailTemplateService.SendEmail("Lead Advisor Assigned", replacements);
    }

    public async Task SendTeamSupportRequestedEmail(Project project, Guid supportTeamId, Guid requestingTeamId)
    {
        var supportTeam = await _staticDataService.RequireSupportTeam(supportTeamId);
        var requestingTeam = await _staticDataService.GetSupportTeam(requestingTeamId);

        var supportTeamLeads = await _supportTeamUserRepository.SelectSupportTeamUserBySupportTeamIdAndIsTeamLead(supportTeamId, isTeamLead: true);

        foreach (var supportTeamLead in supportTeamLeads)
        {
            var user = await _userRepository.GetUser(supportTeamLead.UserId);
            if (user == null)
                continue;

            var replacements = new Dictionary<string, string>
            {
                { "PROJECT_TITLE", project.Title },
                { "PROJECT_REFERENCE", project.PrefixedNumber },
                { "ADVISER_NAME", user.FullName },
                { "ADVISER_EMAIL", user.Email },
                { "SUPPORT_TEAM_NAME", supportTeam.Name },
                { "REQUESTING_TEAM_NAME", requestingTeam?.Name ?? "Unknown Team" },
            };

            await _emailTemplateService.SendEmail("Team Support Requested", replacements);
        }
    }
}
