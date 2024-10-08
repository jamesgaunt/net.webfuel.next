using DocumentFormat.OpenXml.Office2010.Drawing;
using MediatR;

namespace Webfuel.Domain
{
    public class SelectWidgetType : IRequest<List<WidgetType>>
    {
        public required Guid UserId { get; set; }
    }

    internal class SelectWidgetTypeHandler : IRequestHandler<SelectWidgetType, List<WidgetType>>
    {
        private readonly IWidgetRepository _widgetRepository;
        private readonly IWidgetTypeRepository _widgetTypeRepository;

        public SelectWidgetTypeHandler(
            IWidgetRepository widgetRepository, 
            IWidgetTypeRepository widgetTypeRepository)
        {
            _widgetRepository = widgetRepository;
            _widgetTypeRepository = widgetTypeRepository;
        }

        public async Task<List<WidgetType>> Handle(SelectWidgetType request, CancellationToken cancellationToken)
        {
            var widgetTypes = await _widgetTypeRepository.SelectWidgetType();
            return widgetTypes;
        }
    }
}
