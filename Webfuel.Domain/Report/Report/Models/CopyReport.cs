using FluentValidation;
using MediatR;

namespace Webfuel.Domain
{
    public class CopyReport : IRequest<Report>
    {
        public required Guid Id { get; set; }

        public required string Name { get; set; }
    }
}
