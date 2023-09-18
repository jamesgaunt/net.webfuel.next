using FluentValidation;
using MediatR;

namespace Webfuel
{
    public class UpdateWidget : IRequest<Widget>
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public int Age { get; set; }
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
            var original = await _widgetRepository.RequireWidgetAsync(request.Id);

            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Age = request.Age;

            return await _widgetRepository.UpdateWidgetAsync(original: original, updated: updated); ;
        }
    }
}
