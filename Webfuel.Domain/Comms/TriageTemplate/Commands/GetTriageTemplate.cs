using MediatR;

namespace Webfuel.Domain;

public class GetTriageTemplate : IRequest<TriageTemplate?>
{
    public Guid Id { get; set; }
}

internal class GetTriageTemplateHandler : IRequestHandler<GetTriageTemplate, TriageTemplate?>
{
    private readonly ITriageTemplateRepository _reportRepository;

    public GetTriageTemplateHandler(ITriageTemplateRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<TriageTemplate?> Handle(GetTriageTemplate request, CancellationToken cancellationToken)
    {
        return await _reportRepository.GetTriageTemplate(request.Id);
    }
}
