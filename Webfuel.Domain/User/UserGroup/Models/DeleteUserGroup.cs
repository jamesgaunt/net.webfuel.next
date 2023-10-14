using MediatR;

namespace Webfuel.Domain
{
    public class DeleteUserGroup : IRequest
    {
        public Guid Id { get; set; }
    }
}
