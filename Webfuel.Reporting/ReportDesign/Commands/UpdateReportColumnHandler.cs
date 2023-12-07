using FluentValidation;
using MediatR;
using System.Data.Common;

namespace Webfuel.Reporting
{
    internal class UpdateReportColumnHandler : IRequestHandler<UpdateReportColumn, ReportDesign>
    {
        private readonly IReportDesignService _reportDesignService;

        public UpdateReportColumnHandler(IReportDesignService reportDesignService)
        {
            _reportDesignService = reportDesignService;
        }

        public Task<ReportDesign> Handle(UpdateReportColumn request, CancellationToken cancellationToken)
        {
            var schema = _reportDesignService.GetReportSchema(request.ReportProviderId);
            var column = request.Design.GetColumn(request.Id);

            column.Title = request.Title;

            request.Design.ValidateDesign(schema);
            return Task.FromResult(request.Design);
        }
    }
}
