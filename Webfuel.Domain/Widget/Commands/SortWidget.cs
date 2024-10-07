using MediatR;

namespace Webfuel.Domain
{
    public class SortWidget: IRequest
    {
        public required Guid UserId { get; set; }

        public required IEnumerable<Guid> Ids { get; set; }
    }
    internal class SortWidgetHandler : IRequestHandler<SortWidget>
    {
        private readonly IWidgetRepository _widgetuserRepository;
        
        public SortWidgetHandler(IWidgetRepository widgetuserRepository)
        {
            _widgetuserRepository = widgetuserRepository;
        }
        
        public async Task Handle(SortWidget request, CancellationToken cancellationToken)
        {
            var items = await _widgetuserRepository.SelectWidgetByUserId(request.UserId);
            
            var index = 0;
            foreach (var id in request.Ids)
            {
                var original = items.FirstOrDefault(p => p.Id == id);
                if (original != null && original.SortOrder != index)
                {
                    var updated = original.Copy();
                    updated.SortOrder = index;
                    await _widgetuserRepository.UpdateWidget(updated: updated, original: original);
                }
                index++;
            }
        }
    }
}

