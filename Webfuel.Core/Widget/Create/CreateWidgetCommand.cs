using MediatR;

namespace Webfuel
{
    public class CreateWidgetCommand : IRequest<Widget>
    {
        public string Name { get; set; } = String.Empty;

        public int Age { get; set; }
    }
}
