using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public enum ReportColumnAggregation
    {
        None = 0,

        Group = 10,

        Sum = 100,
        Avg = 200,
        Min = 300,
        Max = 400,
        StdDev = 50,
        Median = 600,
        Mode = 700,
        Range= 800,

        List = 1000,
        ListDistinct = 1100,
    }
}
