using FluentValidation;
using MediatR;

namespace Webfuel.Domain
{
    public class UpdateReport : IRequest<Report>
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public required ReportDesign Design { get; set; }
    }
}
