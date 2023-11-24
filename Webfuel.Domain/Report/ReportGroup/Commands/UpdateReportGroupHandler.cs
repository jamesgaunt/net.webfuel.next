using MediatR;

namespace Webfuel.Domain
{
    internal class UpdateReportGroupHandler : IRequestHandler<UpdateReportGroup, ReportGroup>
    {
        private readonly IReportGroupRepository _reportGroupRepository;

        public UpdateReportGroupHandler(IReportGroupRepository reportGroupRepository)
        {
            _reportGroupRepository = reportGroupRepository;
        }

        public async Task<ReportGroup> Handle(UpdateReportGroup request, CancellationToken cancellationToken)
        {
            var original = await _reportGroupRepository.RequireReportGroup(request.Id);

            var updated = original.Copy();
            updated.Name = request.Name;

            return await _reportGroupRepository.UpdateReportGroup(original: original, updated: updated); 
        }
    }
}
