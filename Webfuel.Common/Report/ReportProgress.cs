using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Common
{
    public class ReportProgress
    {
        public Guid TaskId { get; set; }
        public int ProgressPercentage { get; set; }
        public bool Complete { get; set; }
    }
}
