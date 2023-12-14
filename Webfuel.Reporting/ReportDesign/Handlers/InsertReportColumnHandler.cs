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

            foreach(var fieldId in request.FieldIds)
            {
                var field = schema.GetField(fieldId);
                if (field == null)
                    throw new Exception($"The specified field does not exist");

                request.Design.InsertColumn(new ReportColumn
                {
                    FieldId = fieldId,
                    Title = field.Name,
                });
            }

            return _reportDesignService.ValidateDesign(request.Design);
        }
    }
}
