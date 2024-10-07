using MediatR;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain
{
    public class UpdateWidget : IRequest<Widget>
    {
        public required Guid Id { get; set; }
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
            var original = await _widgetRepository.RequireWidget(request.Id);

            var updated = original.Copy();

            return await _widgetRepository.UpdateWidget(original: original, updated: updated);
        }
    }
}
