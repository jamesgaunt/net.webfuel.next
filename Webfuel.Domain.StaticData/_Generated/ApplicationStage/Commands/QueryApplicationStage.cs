using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryApplicationStage: Query, IRequest<QueryResult<ApplicationStage>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(ApplicationStage.Name), Search);
            return this;
        }
    }
    internal class QueryApplicationStageHandler : IRequestHandler<QueryApplicationStage, QueryResult<ApplicationStage>>
    {
        private readonly IApplicationStageRepository _applicationStageRepository;
        
        
        public QueryApplicationStageHandler(IApplicationStageRepository applicationStageRepository)
        {
            _applicationStageRepository = applicationStageRepository;
        }
        
        public async Task<QueryResult<ApplicationStage>> Handle(QueryApplicationStage request, CancellationToken cancellationToken)
        {
            return await _applicationStageRepository.QueryApplicationStage(request.ApplyCustomFilters());
        }
    }
}

