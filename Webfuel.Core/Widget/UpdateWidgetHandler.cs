using MediatR;
using System.Windows.Input;

namespace Webfuel
{
    public class UpdateWidgetCommand: IRequest<Widget>
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public int Age {  get; set; }
    }


    internal class UpdateWidgetHandler : IRequestHandler<UpdateWidgetCommand, Widget>
    {
        private readonly IWidgetRepository _widgetRepository;

        public UpdateWidgetHandler(IWidgetRepository widgetRepository) 
        {
            _widgetRepository = widgetRepository;
        }

        public async Task<Widget> Handle(UpdateWidgetCommand request, CancellationToken cancellationToken)
        {
            var original = await _widgetRepository.RequireWidgetAsync(request.Id);

            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Age = request.Age;

            return await _widgetRepository.UpdateWidgetAsync(original: original, updated: updated);;
        }
    }
}
