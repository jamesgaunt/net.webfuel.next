using MediatR;

namespace Webfuel
{
    public class UpdateWidgetCommand : IRequest<Widget>
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public int Age { get; set; }
    }
}
