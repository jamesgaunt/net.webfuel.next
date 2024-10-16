using MediatR;

namespace Webfuel.Domain;

public class QueryTriageTemplate : Query, IRequest<QueryResult<TriageTemplate>>
{
    public Query ApplyCustomFilters()
    {
        this.Contains(nameof(TriageTemplate.Name), Search);
        return this;
    }
}
internal class QueryTriageTemplateHandler : IRequestHandler<QueryTriageTemplate, QueryResult<TriageTemplate>>
{
    private readonly ITriageTemplateRepository _triageTemplateRepository;


    public QueryTriageTemplateHandler(ITriageTemplateRepository triageTemplateRepository)
    {
        _triageTemplateRepository = triageTemplateRepository;
    }

    public async Task<QueryResult<TriageTemplate>> Handle(QueryTriageTemplate request, CancellationToken cancellationToken)
    {
        return await _triageTemplateRepository.QueryTriageTemplate(request.ApplyCustomFilters());
    }
}

