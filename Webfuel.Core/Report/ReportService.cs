using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    public interface IReportService
    {
        Task<IReportGeneratorResponse> InitialiseReport(IReportRequest request);

        Task<IReportGeneratorResponse> GenerateReport(IReportGeneratorRequest request);
    }

    internal class ReportService
    {
        private readonly IIdentityAccessor _identityAccessor;
        private readonly IServiceProvider _serviceProvider;

        public ReportService(IdentityAccessor identityAccessor, IServiceProvider serviceProvider) 
        { 
            _identityAccessor = identityAccessor;
            _serviceProvider = serviceProvider;
        }


    }
}
