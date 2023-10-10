using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class SortTitle: IRequest
    {
        public required IEnumerable<Guid> Ids { get; set; }
    }
    internal class SortTitleHandler : IRequestHandler<SortTitle>
    {
        private readonly ITitleRepository _titleRepository;
        
        
        public SortTitleHandler(ITitleRepository titleRepository)
        {
            _titleRepository = titleRepository;
        }
        
        public async Task Handle(SortTitle request, CancellationToken cancellationToken)
        {
            var items = await _titleRepository.SelectTitle();
            
            var index = 0;
            foreach (var id in request.Ids)
            {
                var original = items.FirstOrDefault(p => p.Id == id);
                if (original != null && original.SortOrder != index)
                {
                    var updated = original.Copy();
                    updated.SortOrder = index;
                    await _titleRepository.UpdateTitle(updated: updated, original: original);
                }
                index++;
            }
        }
    }
}

