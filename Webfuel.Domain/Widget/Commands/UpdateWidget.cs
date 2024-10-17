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

        public UpdateWidgetHandler(IWidgetRepository widgetRepository)
        {
            _widgetRepository = widgetRepository;
        }

        public async Task<Widget> Handle(UpdateWidget request, CancellationToken cancellationToken)
        {
            var widget = await _widgetRepository.RequireWidget(request.Id);
            widget.ConfigJson = request.ConfigJson;
            return await _widgetRepository.UpdateWidget(widget);
        }
    }
}
