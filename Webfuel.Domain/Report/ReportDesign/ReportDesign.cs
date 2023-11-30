using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    [ApiType]
    public class ReportDesign
    {
        public List<ReportColumn> Columns { get; set; } = new List<ReportColumn>();
    }
}
