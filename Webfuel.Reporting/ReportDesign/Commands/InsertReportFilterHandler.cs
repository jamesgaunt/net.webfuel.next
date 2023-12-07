using FluentValidation;
using MediatR;

namespace Webfuel.Reporting
{
    internal class InsertReportFilterHandler : IRequestHandler<InsertReportFilter, ReportDesign>
    {
        private readonly IReportDesignService _reportDesignService;

        public InsertReportFilterHandler(IReportDesignService reportDesignService)
        {
            _reportDesignService = reportDesignService;
        }

        public Task<ReportDesign> Handle(InsertReportFilter request, CancellationToken cancellationToken)
        {
            var schema = _reportDesignService.GetReportSchema(request.ReportProviderId);
            var field = schema.GetField(request.FieldId);

            var filter = field.FieldType switch
            {
                ReportFieldType.String =>
                    request.Design.InsertFilter(new ReportFilterString
                    {
                        FieldId = request.FieldId,
                    }),

                ReportFieldType.Number =>
                    request.Design.InsertFilter(new ReportFilterNumber
                    {
                        FieldId = request.FieldId,
                    }),

                _ => throw new NotImplementedException()
            };

            request.Design.ValidateDesign(schema);
            return Task.FromResult(request.Design);
        }
    }
}
