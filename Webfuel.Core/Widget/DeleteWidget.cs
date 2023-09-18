using MediatR;

namespace Webfuel
{
    public class DeleteWidget : IRequest
    {
        public Guid Id { get; set; }
    }

    internal class DeleteWidgetHandler : IRequestHandler<DeleteWidget>
    {
        private readonly IWidgetRepository _widgetRepository;

        public DeleteWidgetHandler(IWidgetRepository widgetRepository)
        {
            _widgetRepository = widgetRepository;
        }

        public async Task Handle(DeleteWidget request, CancellationToken cancellationToken)
        {
            await _widgetRepository.DeleteWidgetAsync(request.Id);
        }
    }
}
