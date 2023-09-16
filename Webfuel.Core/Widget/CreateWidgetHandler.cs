using MediatR;
using System.Windows.Input;

namespace Webfuel
{
    public class CreateWidgetCommand: IRequest<Widget>
    {
        public string Name { get; set; } = String.Empty;

        public int Age {  get; set; }
    }

    internal class CreateWidgetHandler : IRequestHandler<CreateWidgetCommand, Widget>
    {
        private readonly IWidgetRepository _widgetRepository;

        public CreateWidgetHandler(IWidgetRepository widgetRepository) 
        {
            _widgetRepository = widgetRepository;
        }

        public async Task<Widget> Handle(CreateWidgetCommand request, CancellationToken cancellationToken)
        {
            return await _widgetRepository.InsertWidgetAsync(new Widget { Name = request.Name, Age = request.Age });
        }
    }
}
