using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    [ApiType]
    public abstract class ReportParameter
    {

        public abstract ReportParameterType ParmeterType { get; }


    }
}
