using FluentValidation;
using MediatR;

namespace Webfuel.Reporting
{
    internal class DeleteReportColumnHandler : IRequestHandler<DeleteReportColumn, ReportDesign>
    {
        private readonly IReportDesignService _reportDesignService;

        public DeleteReportColumnHandler(IReportDesignService reportDesignService)
        {
            _reportDesignService = reportDesignService;
        }

        public Task<ReportDesign> Handle(DeleteReportColumn request, CancellationToken cancellationToken)
        {
            var schema = _reportDesignService.GetReportSchema(request.ReportProviderId);

            request.Design.DeleteColumn(request.Id);

            request.Design.ValidateDesign(schema);
            return Task.FromResult(request.Design);
        }
    }
}
