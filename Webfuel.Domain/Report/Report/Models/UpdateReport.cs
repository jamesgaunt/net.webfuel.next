using FluentValidation;
using MediatR;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    public class UpdateReport : IRequest<Report>
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public bool PrimaryReport { get; set; }

        public required ReportDesign Design { get; set; }
    }
}
