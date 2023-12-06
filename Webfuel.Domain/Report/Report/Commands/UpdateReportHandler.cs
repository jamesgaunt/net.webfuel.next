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
            var original = await _reportRepository.RequireReport(request.Id);

            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Design = request.Design;

            _reportDesignService.ValidateDesign(original.ReportProviderId, updated.Design);

            return await _reportRepository.UpdateReport(original: original, updated: updated); 
        }
    }
}
