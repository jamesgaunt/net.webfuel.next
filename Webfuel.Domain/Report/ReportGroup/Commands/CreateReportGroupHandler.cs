using MediatR;

namespace Webfuel.Domain
{
    internal class CreateReportGroupHandler : IRequestHandler<CreateReportGroup, ReportGroup>
    {
        private readonly IReportGroupRepository _reportGroupRepository;

        public CreateReportGroupHandler(IReportGroupRepository reportGroupRepository)
        {
            _reportGroupRepository = reportGroupRepository;
        }

        public async Task<ReportGroup> Handle(CreateReportGroup request, CancellationToken cancellationToken)
        {
            return await _reportGroupRepository.InsertReportGroup(new ReportGroup { Name = request.Name });
        }
    }
}
