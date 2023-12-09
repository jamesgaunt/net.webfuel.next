using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    // Allows for the definition of a parameter that can be used to filter a report
    // Enables independent sorting of parameters and decluttering of the filter definition
    // Opens up design possibilities for the future
    // Reference from parameter to filter is one way, transactional modification maintains the integrity

    public class ReportParameter
    {
        public Guid FilterId { get; internal set; }

        public string Name { get; internal set; } = String.Empty;

        public string Description { get; internal set; } = String.Empty;

        public bool IsRequired { get; internal set; }
    }
}
