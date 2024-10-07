using DocumentFormat.OpenXml.Office2010.Drawing;
using MediatR;

namespace Webfuel.Domain
{
    public class SelectWidget : IRequest<List<Widget>>
    {
        public required Guid UserId { get; set; }
    }

    internal class SelectWidgetHandler : IRequestHandler<SelectWidget, List<Widget>>
    {
        private readonly IWidgetRepository _widgetRepository;

        public SelectWidgetHandler(IWidgetRepository widgetRepository)
        {
            _widgetRepository = widgetRepository;
        }

        public async Task<List<Widget>> Handle(SelectWidget request, CancellationToken cancellationToken)
        {
            var result = await _widgetRepository.SelectWidgetByUserId(request.UserId);
            if (result.Count > 0)
                return result;

            return await SetupDefaultWidgets(request.UserId);
        }

        async Task<List<Widget>> SetupDefaultWidgets(Guid userId)
        {
            var result = new List<Widget>();

            result.Add(await _widgetRepository.InsertWidget(new Widget
            {
                UserId = userId,
                WidgetTypeId = WidgetTypeEnum.ProjectSummary
            }));

            result.Add(await _widgetRepository.InsertWidget(new Widget
            {
                UserId = userId,
                WidgetTypeId = WidgetTypeEnum.TeamSupportSummary
            }));

            return result;
        }
    }
}
