using Microsoft.Identity.Client;
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
        public string FileName { get; set; } = String.Empty;
        public string WorksheetName { get; set; } = String.Empty;
        public List<ReportColumn> Columns { get; set; } = new List<ReportColumn>();
    }
}
