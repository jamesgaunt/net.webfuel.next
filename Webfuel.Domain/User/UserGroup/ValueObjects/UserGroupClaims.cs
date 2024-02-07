using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public class UserGroupClaims
    {
        public bool CanEditUsers { get; set; }
        public bool CanEditUserGroups { get; set; }
        public bool CanEditStaticData { get; set; }
        public bool CanEditReports { get; set; }
        public bool CanUnlockProjects { get; set; }
        public bool CanTriageSupportRequests { get; set; }
    }
}
