using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    [ApiType]
    public class UserGroupClaims
    {
        public bool CanEditUsers { get; set; } = true;

        public bool CanEditStaticData { get; set; } = true;

        public bool CanEditResearchers { get; set; } = true;
    }
}
