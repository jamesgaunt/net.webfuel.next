using MediatR;

namespace Webfuel.Domain
{
    internal class GetReportGroupHandler : IRequestHandler<GetReportGroup, ReportGroup?>
    {
        private readonly IReportGroupRepository _reportGroupRepository;

        public GetReportGroupHandler(IReportGroupRepository reportGroupRepository)
        {
            _reportGroupRepository = reportGroupRepository;
        }

        public async Task<ReportGroup?> Handle(GetReportGroup request, CancellationToken cancellationToken)
        {
            return await _reportGroupRepository.GetReportGroup(request.Id);
        }
    }
}
