using MediatR;

namespace Webfuel.Domain;

public class DeleteTriageTemplate : IRequest
{
    public required Guid Id { get; set; }
}
internal class DeleteTriageTemplateHandler : IRequestHandler<DeleteTriageTemplate>
{
    private readonly ITriageTemplateRepository _triageTemplateRepository;

    public DeleteTriageTemplateHandler(ITriageTemplateRepository triageTemplateRepository)
    {
        _triageTemplateRepository = triageTemplateRepository;
    }

    public async Task Handle(DeleteTriageTemplate request, CancellationToken cancellationToken)
    {
        await _triageTemplateRepository.DeleteTriageTemplate(request.Id);
    }
}

