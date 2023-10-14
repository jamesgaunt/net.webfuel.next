using MediatR;

namespace Webfuel.Domain
{
    public class GetProject : IRequest<Project?>
    {
        public Guid Id { get; set; }
    }
}
