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
            var column = request.Design.GetColumn(request.Id);
            if(column == null)
                throw new Exception($"The specified column does not exist");

            column.Title = request.Title;

            return _reportDesignService.ValidateDesign(request.Design);
        }
    }
}
