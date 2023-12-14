using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public class ReportArgument
    {
        public Guid FilterId { get; set; }

        public int Condition { get; set; }

        public List<Guid>? GuidsValue { get; set; }
        public double? DoubleValue { get; set; }
        public string? StringValue { get; set; }
        public DateOnly? DateValue { get; set; }
        public Boolean? BooleanValue { get; set; }
    }
}
