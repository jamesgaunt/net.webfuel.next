﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain.Common
{
    [ServiceImplementation(typeof(IIdentityClaimsProvider))]
    internal class UserGroupClaimsProvider : IIdentityClaimsProvider
    {
        private readonly IUserGroupRepository _userGroupRepository;

        public UserGroupClaimsProvider(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }

        public Task ProvideIdentityClaims(IdentityUser user, IdentityClaims claims)
        {
            throw new NotImplementedException();
        }
    }
}