using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public class ReportRequest
    {
        public Guid ReportId { get; }

        public Dictionary<string, object?> Arguments { get; set; } = new Dictionary<string, object?>();
    }
}
