using FluentValidation;
using MediatR;

namespace Webfuel.Domain
{
    public class GetSupportTeam : IRequest<SupportTeam?>
    {
        public Guid Id { get; set; }
    }
}
