using FluentValidation;
using MediatR;

namespace Webfuel.Domain
{
    public class CreateReport : IRequest<Report>
    {
        public required string Name { get; set; }
    }
}
