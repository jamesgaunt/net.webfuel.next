using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    [ApiType]
    public class IdentityUser
    {
        public Guid Id { get; set; }

        public string Email { get; set; } = String.Empty;
    }
}
