using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    public interface ITenantAccessor
    {
        Tenant Tenant { get; }
    }

    [ServiceImplementation(typeof(ITenantAccessor))]
    internal class TenantAccessor: ITenantAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Tenant Tenant
        {
            get
            {
                return TenantStatic.Tenants[0];
            }
        }

        //Tenant ResolveTenant(HttpContext context)
        //{
        //
        //}
    }
}
