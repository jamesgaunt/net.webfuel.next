using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain
{
    public class UpdateWidget : IRequest<Widget>
    {
        public required Guid Id { get; set; }

        public string ConfigJson { get; set; } = String.Empty;
    }

    internal class UpdateWidgetHandler : IRequestHandler<UpdateWidget, Widget>
    {
        private readonly IWidgetRepository _widgetRepository;
        private readonly IServiceProvider _serviceProvider;
        private readonly IWidgetTypeRepository _widgetTypeRepository;

        public UpdateWidgetHandler(
            IWidgetRepository widgetRepository,
            IServiceProvider serviceProvider,
            IWidgetTypeRepository widgetTypeRepository)
        {
            _widgetRepository = widgetRepository;
            _serviceProvider = serviceProvider;
            _widgetTypeRepository = widgetTypeRepository;
        }

        public async Task<Widget> Handle(UpdateWidget request, CancellationToken cancellationToken)
        {
            var widget = await _widgetRepository.RequireWidget(request.Id);

            var provider = _serviceProvider.GetRequiredKeyedService<IWidgetDataProvider>(widget.WidgetTypeId);

            return await provider.UpdateConfig(widget, request.ConfigJson);
        }
    }
}
