using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    [Service(typeof(IIdentityClaimsProvider))]
    internal class UserClaimsProvider : IIdentityClaimsProvider
    {
        private readonly IUserRepository _userRepository;

        public UserClaimsProvider(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ProvideIdentityClaims(IdentityUser user, IdentityClaims claims)
        {
            var _user = await _userRepository.RequireUser(user.Id);
            claims.Developer = _user.Developer;
        }
    }
}
