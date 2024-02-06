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
        Task<Report> GetReportByName(string name, Guid reportProviderId);
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

        public async Task<Report> GetReportByName(string name, Guid reportProviderId)
        {
            var reports = await _reportRepository.SelectReportByNameAndReportProviderId(name, reportProviderId);
            if (reports.Count == 0)
                throw new InvalidOperationException($"The report '{name}' does not exist.");

            if (_identityAccessor.User != null)
            {
                var report = reports.FirstOrDefault(p => p.OwnerUserId == _identityAccessor.User.Id);
                if(report != null)
                    return report;
            }
                
            return reports.FirstOrDefault(p => p.PrimaryReport) ?? throw new InvalidOperationException($"The report '{name}' does not exist.");
        }
    }
}
