using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Common
{
    public interface IReportService
    {
        Task<ReportProgress> RegisterReport(ReportTask task);

        Task<ReportProgress> GenerateReport(Guid taskId);
    }

    internal class ReportService
    {
        private readonly IServiceProvider _serviceProvider;

        public ReportService(IServiceProvider serviceProvider) 
        { 
            _serviceProvider = serviceProvider;
        }
    }
}
