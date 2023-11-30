using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public class ProjectReportContext
    {
        public ProjectReportContext(Project item)
        {
            Item = item;
        }

        public Project Item { get; }
    }
}
