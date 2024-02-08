using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    [ApiEnum]
    public enum ReportColumnCollection
    {
        Default = 0,

        Sum = 100,
        Avg = 200,
        Min = 300,
        Max = 400,
        Count = 500,

        List = 1000,
        ListDistinct = 1100,
    }
}
