using DocumentFormat.OpenXml.Office2010.Drawing;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Webfuel.Terminal;

namespace Webfuel.Domain
{
    public class SelectAvailableWidgetType : IRequest<List<WidgetType>>
    {
    }

    internal class SelectWidgetTypeHandler : IRequestHandler<SelectAvailableWidgetType, List<WidgetType>>
    {
        private readonly IWidgetRepository _widgetRepository;
        private readonly IWidgetTypeRepository _widgetTypeRepository;
        private readonly IServiceProvider _serviceProvider;

        public SelectWidgetTypeHandler(
            IWidgetRepository widgetRepository, 
            IWidgetTypeRepository widgetTypeRepository,
            IServiceProvider serviceProvider)
        {
            _widgetRepository = widgetRepository;
            _widgetTypeRepository = widgetTypeRepository;
            _serviceProvider = serviceProvider;
        }

        public async Task<List<WidgetType>> Handle(SelectAvailableWidgetType request, CancellationToken cancellationToken)
        {
            var result = new List<WidgetType>();
            var widgetTypes = await _widgetTypeRepository.SelectWidgetType();

            foreach(var widgetType in widgetTypes)
            {
                var provider = _serviceProvider.GetRequiredKeyedService<IWidgetProvider>(widgetType.Id);

                if (await provider.AuthoriseAccess())
                    result.Add(widgetType);
            }

            return result;
        }
    }
}
