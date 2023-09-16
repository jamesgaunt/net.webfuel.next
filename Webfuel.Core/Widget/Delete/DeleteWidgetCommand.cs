using MediatR;

namespace Webfuel
{
    public class DeleteWidgetCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
