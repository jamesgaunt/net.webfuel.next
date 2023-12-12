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

            var filter = MapReportFilter(schema, request);
            request.Design.InsertFilter(filter);

            request.Design.ValidateDesign(schema);
            return Task.FromResult(request.Design);
        }

        ReportFilter MapReportFilter(ReportSchema schema, InsertReportFilter request)
        {
            if(request.FieldId == ReportFilterTypeIdentifiers.Group)
                return new ReportFilterGroup();

            if (request.FieldId == ReportFilterTypeIdentifiers.Expression)
                return new ReportFilterExpression();

            var field = schema.GetField(request.FieldId);

            return field.FieldType switch
            {
                ReportFieldType.String =>
                    new ReportFilterString
                    {
                        FieldId = request.FieldId,
                    },

                ReportFieldType.Number =>
                    new ReportFilterNumber
                    {
                        FieldId = request.FieldId,
                    },

                ReportFieldType.Boolean =>
                    new ReportFilterBoolean
                    {
                        FieldId = request.FieldId,
                    },

                ReportFieldType.Reference =>
                    new ReportFilterReference
                    {
                        FieldId = request.FieldId,
                    },

                _ => throw new NotImplementedException()
            };
        }
    }
}
