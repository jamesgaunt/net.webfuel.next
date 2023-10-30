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
                Id = Tenant.RSS_ICL, 
                Name = "RSS Imperial College London" , 
                DatabaseLogin = "login_rss_icl@webfuel",
                DatabasePassword = "APAxTFbB2AoGdmbRUuHe"
            },
        };
    }
}
