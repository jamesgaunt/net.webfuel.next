using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public partial class UserActivity
    {
        public bool IsProjectActivity => WorkActivityId == null && ProjectId != null;
    }
}
