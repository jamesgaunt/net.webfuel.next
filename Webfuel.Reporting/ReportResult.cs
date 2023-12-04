using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public class ReportResult
    {
        public MemoryStream? MemoryStream { get; set; }

        public string ContentType { get; set; } = String.Empty;

        public string Filename { get; set; } = String.Empty;
    }
}
