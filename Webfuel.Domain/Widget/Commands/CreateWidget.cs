using MediatR;

namespace Webfuel.Domain
{
    public class CreateWidget : IRequest<Widget>
    {
        public required Guid UserId { get; set; }

        public required Guid WidgetTypeId { get; set; }
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
            var existing = await _widgetRepository.SelectWidgetByUserId(request.UserId);

            return await _widgetRepository.InsertWidget(new Widget
            {
                UserId = request.UserId,
                WidgetTypeId = request.WidgetTypeId,
                SortOrder = existing.Count(),
            });
        }
    }
}
