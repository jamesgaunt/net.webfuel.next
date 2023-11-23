using Microsoft.Identity.Client;
using System.Reflection.Metadata.Ecma335;

namespace Webfuel
{
    [ApiType]
    public class IdentityClaims
    {
        public bool Developer { get; set; }
        public bool CanEditUsers { get; set; }
        public bool CanEditUserGroups { get; set; }
        public bool CanEditStaticData { get; set; }
        public bool CanUnlockProjects { get; set; }
        public bool CanTriageSupportRequests { get; set; }

        // Computed
        public bool CanAccessConfiguration => CanEditUserGroups || CanEditStaticData;

        public void Sanitize()
        {
            if (Developer)
            {
                CanEditUsers = true;
                CanEditUserGroups = true;
                CanEditStaticData = true;
                CanUnlockProjects = true;
                CanTriageSupportRequests = true;
            }
        }
    }
}
