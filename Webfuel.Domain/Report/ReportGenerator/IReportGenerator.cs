using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public interface IReportGenerator
    {
        Task GenerateReport(ReportTask task);
    }
}
