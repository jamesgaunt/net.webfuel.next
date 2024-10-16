using MediatR;

namespace Webfuel.Domain;

public class SortTriageTemplate: IRequest
{
    public required IEnumerable<Guid> Ids { get; set; }
}
internal class SortTriageTemplateHandler : IRequestHandler<SortTriageTemplate>
{
    private readonly ITriageTemplateRepository _TriageTemplateRepository;
    
    public SortTriageTemplateHandler(ITriageTemplateRepository TriageTemplateRepository)
    {
        _TriageTemplateRepository = TriageTemplateRepository;
    }
    
    public async Task Handle(SortTriageTemplate request, CancellationToken cancellationToken)
    {
        var items = await _TriageTemplateRepository.SelectTriageTemplate();
        
        var index = 0;
        foreach (var id in request.Ids)
        {
            var original = items.FirstOrDefault(p => p.Id == id);
            if (original != null && original.SortOrder != index)
            {
                var updated = original.Copy();
                updated.SortOrder = index;
                await _TriageTemplateRepository.UpdateTriageTemplate(updated: updated, original: original);
            }
            index++;
        }
    }
}

