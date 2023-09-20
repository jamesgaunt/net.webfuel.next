using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    internal static class TenantStatic
    {
        public static IReadOnlyList<Tenant> Tenants = new List<Tenant>
        {
            new Tenant { Name = "Development" , DatabaseSchema = "rssdev", DatabasePassword = "multitenantpwd" },
            new Tenant { Name = "RSS Leicester", DatabaseSchema = "rsslct", DatabasePassword = "multitenantpwd" },
            new Tenant { Name = "RSS London", DatabaseSchema = "rssldn", DatabasePassword = "multitenantpwd" },
        };
    }
}
