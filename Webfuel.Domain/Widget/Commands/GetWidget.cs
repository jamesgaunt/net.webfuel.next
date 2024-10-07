using MediatR;

namespace Webfuel.Domain
{
    public class GetWidget : IRequest<Widget?>
    {
        public Guid Id { get; set; }
    }

    internal class GetWidgetHandler : IRequestHandler<GetWidget, Widget?>
    {
        private readonly IWidgetRepository _widgetRepository;

        public GetWidgetHandler(IWidgetRepository widgetRepository)
        {
            _widgetRepository = widgetRepository;
        }

        public async Task<Widget?> Handle(GetWidget request, CancellationToken cancellationToken)
        {
            return await _widgetRepository.GetWidget(request.Id);
        }
    }
}
