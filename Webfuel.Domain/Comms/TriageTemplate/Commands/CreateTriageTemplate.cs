using MediatR;

namespace Webfuel.Domain;

public class CreateTriageTemplate : IRequest<TriageTemplate>
{
    public required string Name { get; set; }
}
internal class CreateTriageTemplateHandler : IRequestHandler<CreateTriageTemplate, TriageTemplate>
{
    private readonly ITriageTemplateRepository _triageTemplateRepository;

    public CreateTriageTemplateHandler(ITriageTemplateRepository triageTemplateRepository)
    {
        _triageTemplateRepository = triageTemplateRepository;
    }
    public async Task<TriageTemplate> Handle(CreateTriageTemplate request, CancellationToken cancellationToken)
    {
        var updated = await _triageTemplateRepository.InsertTriageTemplate(new TriageTemplate
        {
            Name = request.Name,
        });

        return updated;
    }
}

