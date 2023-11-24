using FluentValidation;
using MediatR;

namespace Webfuel.Domain
{
    public class UpdateReportGroup : IRequest<ReportGroup>
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = String.Empty;
    }
}
