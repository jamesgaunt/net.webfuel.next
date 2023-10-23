using MediatR;

namespace Webfuel.Domain
{
    public class GetSupportRequest : IRequest<SupportRequest?>
    {
        public Guid Id { get; set; }
    }
}
