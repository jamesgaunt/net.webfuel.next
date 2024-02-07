using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    [ApiType]
    public class ClientConfiguration
    {
        public Guid UserId { get; set; } = Guid.NewGuid();

        public string Email { get; set; } = String.Empty;

        public ClientConfigurationMenu SideMenu { get; set; } = new ClientConfigurationMenu();

        public ClientConfigurationMenu SettingsMenu { get; set; } = new ClientConfigurationMenu();

        public ClientConfigurationMenu StaticDataMenu { get; set; } = new ClientConfigurationMenu();

        public IdentityClaims Claims { get; set; } = new IdentityClaims();
    }
}
