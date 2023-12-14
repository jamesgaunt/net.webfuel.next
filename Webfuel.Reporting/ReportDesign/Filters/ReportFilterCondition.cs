using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public class ReportFilterCondition
    {
        public int Value { get; set; }

        public string DisplayName { get; set; } = String.Empty;

        public bool Unary { get; set; }
    }
}
