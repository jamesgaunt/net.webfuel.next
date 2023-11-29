using MediatR;

namespace Webfuel.Domain
{
    public class GetReport : IRequest<Report?>
    {
        public Guid Id { get; set; }
    }
}
