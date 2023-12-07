using FluentValidation;
using MediatR;

namespace Webfuel.Reporting
{
    internal class DeleteReportFilterHandler : IRequestHandler<DeleteReportFilter, ReportDesign>
    {
        private readonly IReportDesignService _reportDesignService;

        public DeleteReportFilterHandler(IReportDesignService reportDesignService)
        {
            _reportDesignService = reportDesignService;
        }

        public Task<ReportDesign> Handle(DeleteReportFilter request, CancellationToken cancellationToken)
        {
            var schema = _reportDesignService.GetReportSchema(request.ReportProviderId);

            request.Design.DeleteFilter(request.Id);

            request.Design.ValidateDesign(schema);
            return Task.FromResult(request.Design);
        }
    }
}
