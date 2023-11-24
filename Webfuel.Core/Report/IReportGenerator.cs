using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    internal interface IReportGenerator
    {
        Task<IReportGeneratorResponse> GenerateReport(IReportGeneratorRequest request);
    }
}
