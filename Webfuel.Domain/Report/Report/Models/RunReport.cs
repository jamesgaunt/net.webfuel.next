using FluentValidation;
using MediatR;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    public class RunReport : IRequest<ReportStep>
    {
        public required Guid ReportId { get; set; }

        public List<ReportArgument>? Arguments { get; set; }

        public object? TypedArguments { get; set; } // Only used when reports are initialised by the server
    }
}
