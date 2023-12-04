using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public class ReportData
    {
        public List<ReportDataCol> Cols { get; } = new List<ReportDataCol>();

        public List<ReportDataRow> Rows { get; } = new List<ReportDataRow>();
    }

    public class ReportDataCol
    {
        public required string Title { get; init; }

        public bool Grouped { get; set; }
    }

    public class ReportDataRow
    {
        public Dictionary<string, object?> Values { get; } = new Dictionary<string, object?>(); 

    }

}
