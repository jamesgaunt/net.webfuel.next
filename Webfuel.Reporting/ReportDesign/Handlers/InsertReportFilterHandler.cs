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


            if(request.ParentId.HasValue)
            {
                var parent = request.Design.GetFilter(request.ParentId.Value);
                if(parent is not ReportFilterGroup group)
                    throw new ValidationException($"Parent filter  group with id {request.ParentId} not found");

                request.Design.InsertFilter(filter, group);
            }
            else
            {
                request.Design.InsertFilter(filter, null);
            }

            return _reportDesignService.ValidateDesign(request.Design);
        }

        ReportFilter MapReportFilter(ReportSchema schema, InsertReportFilter request)
        {
            if(request.FieldId == ReportFilterTypeIdentifiers.Group)
                return new ReportFilterGroup();

            if (request.FieldId == ReportFilterTypeIdentifiers.Expression)
                return new ReportFilterExpression();

            var field = schema.GetField(request.FieldId);
            if (field == null)
                throw new ValidationException($"The specified field does not exist");

            return field.FieldType switch
            {
                // Primatives

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

                ReportFieldType.DateTime =>
                    new ReportFilterDate
                    {
                        FieldId = request.FieldId,
                    },

                ReportFieldType.Date =>
                    new ReportFilterDate
                    {
                        FieldId = request.FieldId,
                    },

                // Complex

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
