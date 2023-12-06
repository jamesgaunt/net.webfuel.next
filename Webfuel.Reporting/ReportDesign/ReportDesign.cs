using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    [ApiType]
    public class ReportDesign
    {
        public List<ReportColumn> Columns { get; set; } = new List<ReportColumn>();

        public List<ReportFilter> Filters { get; set; } = new List<ReportFilter>();
    }
}
