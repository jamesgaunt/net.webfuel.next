using FluentValidation;
using MediatR;

namespace Webfuel.Domain
{
    public class GetReportGroup : IRequest<ReportGroup?>
    {
        public Guid Id { get; set; }
    }
}
