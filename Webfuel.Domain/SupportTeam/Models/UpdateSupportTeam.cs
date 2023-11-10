using FluentValidation;
using MediatR;

namespace Webfuel.Domain
{
    public class UpdateSupportTeam : IRequest<SupportTeam>
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = String.Empty;
    }
}
