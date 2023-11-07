using Microsoft.Identity.Client;

namespace Webfuel
{
    [ApiIgnore]
    public class IdentityClaims
    {
        public bool Developer { get; set; }

        public UserGroupClaims UserGroupClaims { get; } = new UserGroupClaims();
    }
}
