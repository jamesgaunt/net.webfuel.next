using MediatR;

namespace Webfuel
{
    internal class UpdateWidgetCommandHandler : IRequestHandler<UpdateWidgetCommand, Widget>
    {
        private readonly IWidgetRepository _widgetRepository;

        public UpdateWidgetCommandHandler(IWidgetRepository widgetRepository)
        {
            _widgetRepository = widgetRepository;
        }

        public async Task<Widget> Handle(UpdateWidgetCommand request, CancellationToken cancellationToken)
        {
            var original = await _widgetRepository.RequireWidgetAsync(request.Id);

            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Age = request.Age;

            return await _widgetRepository.UpdateWidgetAsync(original: original, updated: updated); ;
        }
    }
}
