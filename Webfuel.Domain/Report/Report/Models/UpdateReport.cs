using FluentValidation;
using MediatR;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    public class UpdateReport : IRequest<Report>
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public bool IsPublic { get; set; }

        public Guid ReportGroupId { get; set; }

        public required ReportDesign Design { get; set; }
    }
}
