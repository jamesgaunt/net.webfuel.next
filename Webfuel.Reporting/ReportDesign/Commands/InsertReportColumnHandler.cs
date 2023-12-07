using FluentValidation;
using MediatR;

namespace Webfuel.Reporting
{

    internal class InsertReportColumnHandler : IRequestHandler<InsertReportColumn, ReportDesign>
    {
        private readonly IReportDesignService _reportDesignService;

        public InsertReportColumnHandler(IReportDesignService reportDesignService)
        {
            _reportDesignService = reportDesignService;
        }

        public Task<ReportDesign> Handle(InsertReportColumn request, CancellationToken cancellationToken)
        {
            var schema = _reportDesignService.GetReportSchema(request.ReportProviderId);
            var field = schema.GetField(request.FieldId);

            request.Design.InsertColumn(new ReportColumn
            {
                FieldId = request.FieldId,
                Title = field.Name,
            });

            request.Design.ValidateDesign(schema);
            return Task.FromResult(request.Design);
        }
    }
}
