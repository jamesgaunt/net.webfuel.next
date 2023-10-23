using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class SortHowDidYouFindUs: IRequest
    {
        public required IEnumerable<Guid> Ids { get; set; }
    }
    internal class SortHowDidYouFindUsHandler : IRequestHandler<SortHowDidYouFindUs>
    {
        private readonly IHowDidYouFindUsRepository _howDidYouFindUsRepository;
        
        
        public SortHowDidYouFindUsHandler(IHowDidYouFindUsRepository howDidYouFindUsRepository)
        {
            _howDidYouFindUsRepository = howDidYouFindUsRepository;
        }
        
        public async Task Handle(SortHowDidYouFindUs request, CancellationToken cancellationToken)
        {
            var items = await _howDidYouFindUsRepository.SelectHowDidYouFindUs();
            
            var index = 0;
            foreach (var id in request.Ids)
            {
                var original = items.FirstOrDefault(p => p.Id == id);
                if (original != null && original.SortOrder != index)
                {
                    var updated = original.Copy();
                    updated.SortOrder = index;
                    await _howDidYouFindUsRepository.UpdateHowDidYouFindUs(updated: updated, original: original);
                }
                index++;
            }
        }
    }
}

