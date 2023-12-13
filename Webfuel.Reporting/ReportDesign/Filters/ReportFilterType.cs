using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    [ApiEnum]
    public enum ReportFilterType
    {
        String = 10,
        Number = 20,
        Boolean = 30,
        // DateTime = 40,
        Date = 50,

        Reference = 200,
        // ReferenceList = 210,

        Group = 1000,
        Expression = 2000,
    }

    [ApiEnum]
    public static class ReportFilterTypeIdentifiers
    {
        public static readonly Guid Group = new Guid("00000000-0000-0000-0000-000000000001");

        public static readonly Guid Expression = new Guid("00000000-0000-0000-0000-000000000002");
    }
}
