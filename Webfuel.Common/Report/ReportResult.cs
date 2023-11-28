using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Common
{
    public class ReportResult
    {
        public MemoryStream? MemoryStream { get; set; }

        public required string ContentType { get; set; }

        public required string FileDownloadName { get; set; }
    }
}
