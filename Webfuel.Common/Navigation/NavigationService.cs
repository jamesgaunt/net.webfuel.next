namespace Webfuel.Common;

public interface INavigationService : IClientConfigurationProvider
{
}

[Service(typeof(INavigationService), typeof(IClientConfigurationProvider))]
internal class NavigationService : INavigationService
{
    private readonly IIdentityAccessor _identityAccessor;

    public NavigationService(IIdentityAccessor identityAccessor)
    {
        _identityAccessor = identityAccessor;
    }

    public Task ProvideClientConfiguration(ClientConfiguration clientConfiguration)
    {
        // Side Menu
        {
            clientConfiguration.SideMenu.AddChild(name: "My Activity", action: "/home/my-activity", icon: "fas fa-fw fa-calculator");

            if (_identityAccessor.Claims.CanTriageSupportRequests)
                clientConfiguration.SideMenu.AddChild(name: "Triage", action: "/support-request/support-request-list", icon: "far fa-fw fa-random");

            clientConfiguration.SideMenu.AddChild(name: "Projects", action: "/project/project-list", icon: "fas fa-fw fa-books");

            if (_identityAccessor.Claims.CanEditUsers)
                clientConfiguration.SideMenu.AddChild(name: "Users", action: "/user/user-list", icon: "fas fa-fw fa-users");

            if (_identityAccessor.Claims.CanEditReports)
                clientConfiguration.SideMenu.AddChild(name: "Reports", action: "/reporting/report-list", icon: "fas fa-fw fa-file-spreadsheet");

            if (_identityAccessor.Claims.CanAccessConfiguration)
                clientConfiguration.SideMenu.AddChild(name: "Configuration", action: "/configuration/configuration-menu", icon: "fas fa-fw fa-cogs");
        }

        // Settings Menu
        {
            if (_identityAccessor.Claims.CanEditUserGroups)
            {
                clientConfiguration.SettingsMenu.AddChild(name: "User Groups", action: "/user/user-group-list", icon: "fas fa-users-cog");
            }

            if (_identityAccessor.Claims.Developer)
            {
                clientConfiguration.SettingsMenu.AddChild(name: "User Logins", action: "/developer/user-login", icon: "fas fa-sign-in");
                clientConfiguration.SettingsMenu.AddChild(name: "Email Templates", action: "/configuration/email-template-list", icon: "fas fa-envelope-open-text");
            }

            if (_identityAccessor.Claims.CanEditStaticData)
            {
                clientConfiguration.SettingsMenu.AddChild(name: "Report Groups", action: "/reporting/report-group-list", icon: "fas fa-layer-group");
                clientConfiguration.SettingsMenu.AddChild(name: "Triage Templates", action: "/configuration/triage-template-list", icon: "fas fa-envelope-open-text");
                clientConfiguration.SettingsMenu.AddChild(name: "Global Files", action: "/configuration/global-file-list", icon: "fas fa-file");
            }
        }

        return Task.CompletedTask;
    }
}
