using MediatR;

namespace Webfuel.Domain
{
    public class SortWidget: IRequest
    {
        public required IEnumerable<Guid> Ids { get; set; }
    }
    internal class SortWidgetHandler : IRequestHandler<SortWidget>
    {
        private readonly IWidgetRepository _widgetuserRepository;
        private readonly IIdentityAccessor _identityAccessor;

        public SortWidgetHandler(
            IWidgetRepository widgetuserRepository,
            IIdentityAccessor identityAccessor)
        {
            _widgetuserRepository = widgetuserRepository;
            _identityAccessor = identityAccessor;
        }
        
        public async Task Handle(SortWidget request, CancellationToken cancellationToken)
        {
            var identity = _identityAccessor.User;
            if (identity == null)
                throw new InvalidOperationException("Invalid identity context");

            var items = await _widgetuserRepository.SelectWidgetByUserId(identity.Id);
            
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

