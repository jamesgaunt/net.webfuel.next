using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    [Service(typeof(IIdentityClaimsProvider))]
    internal class UserGroupClaimsProvider : IIdentityClaimsProvider
    {
        private readonly IUserGroupRepository _userGroupRepository;

        public UserGroupClaimsProvider(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }

        public Task ProvideIdentityClaims(IdentityUser user, IdentityClaims claims)
        {
            return Task.FromResult<object?>(null);
        }
    }
}
