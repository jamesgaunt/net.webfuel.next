using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    [Service(typeof(IIdentityClaimsProvider))]
    internal class UserGroupClaimsProvider : IIdentityClaimsProvider
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserGroupRepository _userGroupRepository;

        public UserGroupClaimsProvider(
            IUserRepository userRepository, 
            IUserGroupRepository userGroupRepository)
        {
            _userRepository = userRepository;
            _userGroupRepository = userGroupRepository;
        }

        public async Task ProvideIdentityClaims(IdentityUser user, IdentityClaims claims)
        {
            var _user = await _userRepository.RequireUser(user.Id);
            var userGroup = await _userGroupRepository.RequireUserGroup(_user.UserGroupId);

            claims.CanEditUsers |= userGroup.Claims.CanEditUsers;
            claims.CanEditUserGroups |= userGroup.Claims.CanEditUserGroups;
            claims.CanEditStaticData |= userGroup.Claims.CanEditStaticData;
            claims.CanEditReports |= userGroup.Claims.CanEditReports;
            claims.CanUnlockProjects |= userGroup.Claims.CanUnlockProjects;
            claims.CanTriageSupportRequests |= userGroup.Claims.CanTriageSupportRequests;
        }
    }
}
