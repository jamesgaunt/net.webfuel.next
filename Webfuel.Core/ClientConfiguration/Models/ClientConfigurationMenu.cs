using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    public class ClientConfigurationMenu
    {
        public string Icon { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Action { get; set; } = string.Empty;

        public List<ClientConfigurationMenu>? Children { get; set; }
    }
}
