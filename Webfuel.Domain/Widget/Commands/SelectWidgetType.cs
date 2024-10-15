using DocumentFormat.OpenXml.Office2010.Drawing;
using MediatR;

namespace Webfuel.Domain
{
    public class SelectAvailableWidgetType : IRequest<List<WidgetType>>
    {
    }

    internal class SelectWidgetTypeHandler : IRequestHandler<SelectAvailableWidgetType, List<WidgetType>>
    {
        private readonly IWidgetRepository _widgetRepository;
        private readonly IWidgetTypeRepository _widgetTypeRepository;
        private readonly IIdentityAccessor _identityAccessor;

        public SelectWidgetTypeHandler(
            IWidgetRepository widgetRepository, 
            IWidgetTypeRepository widgetTypeRepository,
            IIdentityAccessor identityAccessor)
        {
            _widgetRepository = widgetRepository;
            _widgetTypeRepository = widgetTypeRepository;
            _identityAccessor = identityAccessor;
        }

        public async Task<List<WidgetType>> Handle(SelectAvailableWidgetType request, CancellationToken cancellationToken)
        {
            var identity = _identityAccessor.User;
            if (identity == null)
                return new List<WidgetType>();

            var widgetTypes = await _widgetTypeRepository.SelectWidgetType();

            if (_identityAccessor.Claims.Developer)
                return widgetTypes;

            return widgetTypes.Where(p => p.Id != WidgetTypeEnum.TeamActivity).ToList();
        }
    }
}
