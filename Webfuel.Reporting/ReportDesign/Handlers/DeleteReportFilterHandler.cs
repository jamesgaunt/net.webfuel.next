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
            request.Design.DeleteFilter(request.Id);
            
            return _reportDesignService.ValidateDesign(request.Design);
        }
    }
}
