using MediatR;

namespace Webfuel.Domain
{
    public class DeleteUser : IRequest
    {
        public Guid Id { get; set; }
    }
}
