using MediatR;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    internal class UpdateReportHandler : IRequestHandler<UpdateReport, Report>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IReportDesignService _reportDesignService;
        private readonly IIdentityAccessor _identityAccessor;

        public UpdateReportHandler(IReportRepository reportRepository, IReportDesignService reportDesignService, IIdentityAccessor identityAccessor)
        {
            _reportRepository = reportRepository;
            _reportDesignService = reportDesignService;
            _identityAccessor = identityAccessor;
        }

        public async Task<Report> Handle(UpdateReport request, CancellationToken cancellationToken)
        {
            await _reportDesignService.ValidateDesign(request.Design);

            var original = await _reportRepository.RequireReport(request.Id);

            if (original.ReportProviderId != request.Design.ReportProviderId)
                throw new DomainException("Cannot change the report provider of a report");

            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Design = request.Design;

            // Only developers can update the primary report flag
            if(_identityAccessor.Claims.Developer)
                updated.PrimaryReport = request.PrimaryReport;

            // Clear primary report flag on any conflicting reports (only need to check if name or primary report flag has changed)
            if (updated.PrimaryReport)
            {
                if (original.PrimaryReport == false || original.Name != updated.Name)
                {
                    var conflicts = await _reportRepository.SelectReportByNameAndReportProviderId(name: updated.Name, reportProviderId: updated.ReportProviderId);
                    foreach(var conflict in conflicts)
                    {
                        if (conflict.Id == updated.Id || conflict.PrimaryReport == false)
                            continue;

                        var originalConflict = conflict.Copy();
                        conflict.PrimaryReport = false;
                        await _reportRepository.UpdateReport(original: originalConflict, updated: conflict);
                    }
                }
            }

            return await _reportRepository.UpdateReport(original: original, updated: updated);
        }
    }
}
