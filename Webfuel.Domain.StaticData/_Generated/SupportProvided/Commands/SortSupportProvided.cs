using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class SortSupportProvided: IRequest
    {
        public required IEnumerable<Guid> Ids { get; set; }
    }
    internal class SortSupportProvidedHandler : IRequestHandler<SortSupportProvided>
    {
        private readonly ISupportProvidedRepository _supportProvidedRepository;
        
        
        public SortSupportProvidedHandler(ISupportProvidedRepository supportProvidedRepository)
        {
            _supportProvidedRepository = supportProvidedRepository;
        }
        
        public async Task Handle(SortSupportProvided request, CancellationToken cancellationToken)
        {
            var items = await _supportProvidedRepository.SelectSupportProvided();
            
            var index = 0;
            foreach (var id in request.Ids)
            {
                var original = items.FirstOrDefault(p => p.Id == id);
                if (original != null && original.SortOrder != index)
                {
                    var updated = original.Copy();
                    updated.SortOrder = index;
                    await _supportProvidedRepository.UpdateSupportProvided(updated: updated, original: original);
                }
                index++;
            }
        }
    }
}

