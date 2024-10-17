using MediatR;

namespace Webfuel.Domain;

public class UpdateTriageTemplate : IRequest<TriageTemplate>
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Subject { get; set; }
    public required string HtmlTemplate { get; set; }
}
internal class UpdateTriageTemplateHandler : IRequestHandler<UpdateTriageTemplate, TriageTemplate>
{
    private readonly ITriageTemplateRepository _triageTemplateRepository;
    private readonly IHtmlSanitizerService _htmlSanitizerService;

    public UpdateTriageTemplateHandler(
        ITriageTemplateRepository triageTemplateRepository,
        IHtmlSanitizerService htmlSanitizerService)
    {
        _triageTemplateRepository = triageTemplateRepository;
        _htmlSanitizerService = htmlSanitizerService;
    }

    public async Task<TriageTemplate> Handle(UpdateTriageTemplate request, CancellationToken cancellationToken)
    {
        var original = await _triageTemplateRepository.RequireTriageTemplate(request.Id);

        var updated = original.Copy();
        updated.Name = request.Name;
        updated.Subject = request.Subject;
        updated.HtmlTemplate = _htmlSanitizerService.SanitizeHtml(request.HtmlTemplate);

        updated = await _triageTemplateRepository.UpdateTriageTemplate(original: original, updated: updated);
        return updated;
    }
}

