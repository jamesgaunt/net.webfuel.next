using FluentValidation;
using MediatR;

namespace Webfuel.Domain
{
    public class CreateReportGroup : IRequest<ReportGroup>
    {
        public required string Name { get; set; }
    }
}
