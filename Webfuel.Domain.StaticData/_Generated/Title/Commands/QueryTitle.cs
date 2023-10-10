using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryTitle: Query, IRequest<QueryResult<Title>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(Title.Name), Search);
            return this;
        }
    }
    internal class QueryTitleHandler : IRequestHandler<QueryTitle, QueryResult<Title>>
    {
        private readonly ITitleRepository _titleRepository;
        
        
        public QueryTitleHandler(ITitleRepository titleRepository)
        {
            _titleRepository = titleRepository;
        }
        
        public async Task<QueryResult<Title>> Handle(QueryTitle request, CancellationToken cancellationToken)
        {
            return await _titleRepository.QueryTitle(request.ApplyCustomFilters());
        }
    }
}

