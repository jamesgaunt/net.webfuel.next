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
        public string Name { get; set; } = String.Empty;

        public string Filename { get; set; } = String.Empty;

        public Guid ProviderId { get; set; }

        public List<ReportColumn> Columns { get; set; } = new List<ReportColumn>();

        public List<ReportParameter> Parameters { get; set; } = new List<ReportParameter>();
    }
}
