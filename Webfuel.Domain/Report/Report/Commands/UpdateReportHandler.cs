using MediatR;

namespace Webfuel.Domain
{
    internal class UpdateReportHandler : IRequestHandler<UpdateReport, Report>
    {
        private readonly IReportRepository _reportRepository;

        public UpdateReportHandler(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<Report> Handle(UpdateReport request, CancellationToken cancellationToken)
        {
            var original = await _reportRepository.RequireReport(request.Id);

            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Design = request.Design;

            return await _reportRepository.UpdateReport(original: original, updated: updated); 
        }
    }
}
