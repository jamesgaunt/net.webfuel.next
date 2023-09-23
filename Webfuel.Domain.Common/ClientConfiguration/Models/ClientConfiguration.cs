using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain.Common
{
    [ApiType]
    public class ClientConfiguration
    {
        internal ClientConfiguration(IdentityUser user, IdentityClaims claims)
        {
            Email = user.Email;
        }

        public string Email { get; init; }
    }
}
