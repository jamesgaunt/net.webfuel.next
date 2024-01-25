using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain
{
    [Service(typeof(IIdentityClaimsProvider))]
    internal class UserClaimsProvider : IIdentityClaimsProvider
    {
        private readonly IUserRepository _userRepository;
        private readonly ISupportTeamUserRepository _supportTeamUserRepository;

        public UserClaimsProvider(IUserRepository userRepository, ISupportTeamUserRepository supportTeamUserRepository)
        {
            _userRepository = userRepository;
            _supportTeamUserRepository = supportTeamUserRepository; 
        }

        public async Task ProvideIdentityClaims(IdentityUser user, IdentityClaims claims)
        {
            var _user = await _userRepository.RequireUser(user.Id);

            claims.Developer |= _user.Developer;

            claims.CanTriageSupportRequests |=
                await _supportTeamUserRepository.GetSupportTeamUserByUserIdAndSupportTeamId(userId: user.Id, supportTeamId: SupportTeamEnum.TriageTeam)
                != null;
        }
    }
}
