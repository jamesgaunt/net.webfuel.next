using MediatR;
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
        private readonly IWidgetTypeRepository _widgetTypeRepository;

        public UpdateWidgetHandler(
            IWidgetRepository widgetRepository,
            IWidgetTypeRepository widgetTypeRepository)
        {
            _widgetRepository = widgetRepository;
            _widgetTypeRepository = widgetTypeRepository;
        }

        public async Task<Widget> Handle(UpdateWidget request, CancellationToken cancellationToken)
        {
            var original = await _widgetRepository.RequireWidget(request.Id);
            var updated = original.Copy();

            updated.ConfigJson = request.ConfigJson;
            updated.DataCurrent = false;

            return await _widgetRepository.UpdateWidget(original: original, updated: updated);
        }
    }
}
