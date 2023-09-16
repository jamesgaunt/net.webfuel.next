using MediatR;
using System.Windows.Input;

namespace Webfuel
{
    public class DeleteWidgetCommand: IRequest
    {
        public Guid Id { get; set; }
    }

    internal class DeleteWidgetHandler : IRequestHandler<DeleteWidgetCommand>
    {
        private readonly IWidgetRepository _widgetRepository;

        public DeleteWidgetHandler(IWidgetRepository widgetRepository) 
        {
            _widgetRepository = widgetRepository;
        }

        public async Task Handle(DeleteWidgetCommand request, CancellationToken cancellationToken)
        {
            await _widgetRepository.DeleteWidgetAsync(request.Id);
        }
    }
}
