using FluentValidation;
using MediatR;
using System.Text.Json.Serialization;

namespace Webfuel
{
    public class CreateWidget : IRequest<Widget>
    {
        public string Name { get; set; } = String.Empty;

        public int Age { get; set; }
    }


    internal class CreateWidgetHandler : IRequestHandler<CreateWidget, Widget>
    {
        private readonly IWidgetRepository _widgetRepository;

        public CreateWidgetHandler(IWidgetRepository widgetRepository)
        {
            _widgetRepository = widgetRepository;
        }

        public async Task<Widget> Handle(CreateWidget request, CancellationToken cancellationToken)
        {
            return await _widgetRepository.InsertWidgetAsync(new Widget { Name = request.Name, Age = request.Age });
        }
    }
}
