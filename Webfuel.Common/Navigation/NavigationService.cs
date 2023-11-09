using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Common
{
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
            clientConfiguration.SideMenu.AddChild(name: "My Activity", action: "/home/my-activity", icon: "fas fa-fw fa-calculator");
            clientConfiguration.SideMenu.AddChild(name: "Projects", action: "/project/project-list", icon: "fas fa-fw fa-books");
            clientConfiguration.SideMenu.AddChild(name: "Requests", action: "/support-request/support-request-list", icon: "far fa-fw fa-question");
            clientConfiguration.SideMenu.AddChild(name: "Users", action: "/user/user-list", icon: "fas fa-fw fa-users");
            clientConfiguration.SideMenu.AddChild(name: "Configuration", action: "/configuration/configuration-menu", icon: "fas fa-fw fa-cogs");

            clientConfiguration.SettingsMenu.AddChild(name: "User Groups", action: "/user/user-group-list", icon: "fas fa-users-cog");

            return Task.CompletedTask;
        }
    }
}
