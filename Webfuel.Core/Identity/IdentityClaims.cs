using Microsoft.Identity.Client;
using System.Reflection.Metadata.Ecma335;

namespace Webfuel
{
    [ApiType]
    public class IdentityClaims
    {
        public bool Developer { get; set; }

        // User Group Claims
        
        public bool Administrator { get; set; }

        public bool CanEditUsers { get; set; }
        public bool CanEditUserGroups { get; set; }
        public bool CanEditStaticData { get; set; }
        public bool CanEditReports { get; set; }
        public bool CanUnlockProjects { get; set; }
        public bool CanTriageSupportRequests { get; set; }

        // Implied Claims
        public bool CanAccessConfiguration => CanEditUserGroups || CanEditStaticData;

        public void Sanitize()
        {
            if (Developer || Administrator)
            {
                CanEditUsers = true;
                CanEditUserGroups = true;
                CanEditReports = true;
                CanEditStaticData = true;
                CanUnlockProjects = true;
                CanTriageSupportRequests = true;
            }
        }
    }
}
