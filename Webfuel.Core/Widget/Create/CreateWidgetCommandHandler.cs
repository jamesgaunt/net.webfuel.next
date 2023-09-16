using MediatR;

namespace Webfuel
{
    internal class CreateWidgetCommandHandler : IRequestHandler<CreateWidgetCommand, Widget>
    {
        private readonly IWidgetRepository _widgetRepository;

        public CreateWidgetCommandHandler(IWidgetRepository widgetRepository)
        {
            _widgetRepository = widgetRepository;
        }

        public async Task<Widget> Handle(CreateWidgetCommand request, CancellationToken cancellationToken)
        {
            return await _widgetRepository.InsertWidgetAsync(new Widget { Name = request.Name, Age = request.Age });
        }
    }
}
