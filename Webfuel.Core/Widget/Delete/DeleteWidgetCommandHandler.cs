using MediatR;

namespace Webfuel
{

    internal class DeleteWidgetCommandHandler : IRequestHandler<DeleteWidgetCommand>
    {
        private readonly IWidgetRepository _widgetRepository;

        public DeleteWidgetCommandHandler(IWidgetRepository widgetRepository)
        {
            _widgetRepository = widgetRepository;
        }

        public async Task Handle(DeleteWidgetCommand request, CancellationToken cancellationToken)
        {
            await _widgetRepository.DeleteWidgetAsync(request.Id);
        }
    }
}
