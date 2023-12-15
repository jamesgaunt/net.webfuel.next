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
        public string Name { get; set; } = String.Empty;

         public Guid FilterId { get; set; }

        public Guid FieldId { get; set; }

        public Guid ReportProviderId { get; set; }

        public ReportFieldType FieldType { get; set; }

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
