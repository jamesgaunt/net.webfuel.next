using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain
{
    public interface IReportService
    {
        Task<Report> GetDefaultNamedReport(string name, Guid reportProviderId);
    }

    [Service(typeof(IReportService))]
    internal class ReportService: IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IIdentityAccessor _identityAccessor;

        public ReportService(IReportRepository reportRepository, IIdentityAccessor identityAccessor)
        {
            _reportRepository = reportRepository;
            _identityAccessor = identityAccessor;
        }

        // Hardcoded developer id
        static Guid DeveloperId = Guid.Parse("b752fc3b-db1d-4ea3-9ac3-92299e44d3cd");

        public async Task<Report> GetDefaultNamedReport(string name, Guid reportProviderId)
        {
            var reports = await _reportRepository.SelectReportByNameAndReportProviderId(name, reportProviderId);
            if (reports.Count == 0)
                throw new InvalidOperationException($"The report '{name}' does not exist.");

            // First of all look for a report of that names that is owned by the current user

            if (_identityAccessor.User != null)
            {
                var report = reports.FirstOrDefault(p => p.OwnerUserId == _identityAccessor.User.Id);
                if(report != null)
                    return report;
            }
                
            // Then look for a report of that name that is owned by the developer

            return reports.FirstOrDefault(p => p.OwnerUserId == DeveloperId) ?? throw new InvalidOperationException($"The report '{name}' does not exist.");
        }
    }
}
