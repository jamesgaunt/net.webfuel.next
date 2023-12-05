using FluentValidation;
using MediatR;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    public class RunReport : IRequest<ReportStep>
    {
        public required Guid ReportId { get; set; }

        public Dictionary<string, object?>? Arguments { get; set; }
    }
}
