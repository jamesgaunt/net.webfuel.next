using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    internal static class TenantStatic
    {
        public static IReadOnlyList<TenantData> Tenants = new List<TenantData>
        {
            new TenantData { 
                Id = Tenant.RSS_Development, 
                Name = "Development" , 
                DatabaseSchema = "rssdev", 
                DatabasePassword = "multitenantpwd" 
            },
            new TenantData { 
                Id = Tenant.RSS_Leicester, 
                Name = "RSS Leicester", 
                DatabaseSchema = "rsslct", 
                DatabasePassword = "multitenantpwd" 
            },
            new TenantData { 
                Id = Tenant.RSS_London,
                Name = "RSS London", 
                DatabaseSchema = "rssldn", 
                DatabasePassword = "multitenantpwd" 
            },
        };
    }
}
