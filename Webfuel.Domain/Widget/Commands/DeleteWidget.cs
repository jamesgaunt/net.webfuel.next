using MediatR;

namespace Webfuel.Domain
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
            await _widgetRepository.DeleteWidget(request.Id);
        }
    }
}
