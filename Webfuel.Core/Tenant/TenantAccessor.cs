﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    public interface ITenantAccessor
    {
        TenantData Tenant { get; }
    }

    [Service(typeof(ITenantAccessor))]
    internal class TenantAccessor: ITenantAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public TenantData Tenant
        {
            get
            {
                return TenantStatic.Tenants[0];
            }
        }
    }
}
