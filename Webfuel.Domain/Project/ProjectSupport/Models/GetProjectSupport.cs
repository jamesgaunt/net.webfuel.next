using MediatR;

namespace Webfuel.Domain
{
    public class GetProjectSupport : IRequest<ProjectSupport?>
    {
        public Guid Id { get; set; }
    }
}
