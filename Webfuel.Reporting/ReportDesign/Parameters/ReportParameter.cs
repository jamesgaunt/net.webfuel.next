using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public class ReportParameter
    {
        public string Name { get; internal set; } = String.Empty;

        public string Description { get; internal set; } = String.Empty;

        public bool IsRequired { get; internal set; }
    }
}
