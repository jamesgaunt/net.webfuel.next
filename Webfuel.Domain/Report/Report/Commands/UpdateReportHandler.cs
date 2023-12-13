using MediatR;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    internal class UpdateReportHandler : IRequestHandler<UpdateReport, Report>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IReportDesignService _reportDesignService;


        public UpdateReportHandler(IReportRepository reportRepository, IReportDesignService reportDesignService)
        {
            _reportRepository = reportRepository;
            _reportDesignService = reportDesignService;
        }

        public async Task<Report> Handle(UpdateReport request, CancellationToken cancellationToken)
        {
            await _reportDesignService.ValidateDesign(request.Design);

            var original = await _reportRepository.RequireReport(request.Id);

            if(original.ReportProviderId != request.Design.ReportProviderId)
                throw new DomainException("Cannot change the report provider of a report");

            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Design = request.Design;

            return await _reportRepository.UpdateReport(original: original, updated: updated); 
        }
    }
}
