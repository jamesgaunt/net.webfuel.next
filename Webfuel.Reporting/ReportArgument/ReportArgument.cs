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
        public required string Name { get; set; }

        public required Guid FilterId { get; set; }

        public required Guid FieldId { get; set; }

        public required Guid ReportProviderId { get; set; }

        public required ReportFieldType FieldType { get; set; }

        public required ReportFilterType FilterType { get; set; }

        // Condition 

        public int Condition { get; set; }

        public List<ReportFilterCondition> Conditions { get; set; } = new List<ReportFilterCondition>();

        // Value

        public List<Guid>? GuidsValue { get; set; }

        public double? DoubleValue { get; set; }
        
        public string? StringValue { get; set; }
        
        public DateOnly? DateValue { get; set; }
    }
}
