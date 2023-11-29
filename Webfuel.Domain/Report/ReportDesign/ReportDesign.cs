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
        public Guid ReportProviderId { get; set; }

        public List<ReportColumn> Columns { get; } = new List<ReportColumn>();
    }
}
