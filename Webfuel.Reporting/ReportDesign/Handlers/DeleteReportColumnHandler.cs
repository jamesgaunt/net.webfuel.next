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
            request.Design.DeleteColumn(request.Id);

            return _reportDesignService.ValidateDesign(request.Design);
        }
    }
}
