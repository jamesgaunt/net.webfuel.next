using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    [ApiEnum]
    public enum ReportParameterGroupCondition
    {
        All = 0,
        Any = 1,
        None = 2  
    }

    [ApiType]
    public class ReportParameterGroup: ReportParameter
    {
        public override ReportParameterType ParmeterType => ReportParameterType.Group;

        public ReportParameterGroupCondition Condition { get; set; } = ReportParameterGroupCondition.All;

        public List<ReportParameter> Parameters { get; set; } = new List<ReportParameter>();
    }
}
