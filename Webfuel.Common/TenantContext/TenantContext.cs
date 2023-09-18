using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Common
{
    public class TenantContext
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public TenantContext(Tenant tenant)
        {
            Id = tenant.Id;
            Name = tenant.Name;
        }
    }
}
