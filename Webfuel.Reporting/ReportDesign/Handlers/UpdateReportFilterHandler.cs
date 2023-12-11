using FluentValidation;
using MediatR;
using System.Data.Common;

namespace Webfuel.Reporting
{
    internal class UpdateReportFilterHandler : IRequestHandler<UpdateReportFilter, ReportDesign>
    {
        private readonly IReportDesignService _reportDesignService;

        public UpdateReportFilterHandler(IReportDesignService reportDesignService)
        {
            _reportDesignService = reportDesignService;
        }

        public Task<ReportDesign> Handle(UpdateReportFilter request, CancellationToken cancellationToken)
        {
            var schema = _reportDesignService.GetReportSchema(request.ReportProviderId);
            var filter = request.Design.GetFilter(request.Filter.Id);
            filter.Apply(request.Filter, schema);
            request.Design.ValidateDesign(schema);
            return Task.FromResult(request.Design);
        }
    }
}
