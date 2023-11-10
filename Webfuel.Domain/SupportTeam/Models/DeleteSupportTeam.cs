using MediatR;

namespace Webfuel.Domain
{
    public class DeleteSupportTeam : IRequest
    {
        public Guid Id { get; set; }
    }
}
