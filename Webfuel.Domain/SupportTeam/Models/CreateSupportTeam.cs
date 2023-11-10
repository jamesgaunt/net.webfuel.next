using FluentValidation;
using MediatR;

namespace Webfuel.Domain
{
    public class CreateSupportTeam : IRequest<SupportTeam>
    {
        public required string Name { get; set; }
    }
}
