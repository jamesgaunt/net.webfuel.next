using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryFileTag: Query, IRequest<QueryResult<FileTag>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(FileTag.Name), Search);
            return this;
        }
    }
    internal class QueryFileTagHandler : IRequestHandler<QueryFileTag, QueryResult<FileTag>>
    {
        private readonly IFileTagRepository _fileTagRepository;
        
        
        public QueryFileTagHandler(IFileTagRepository fileTagRepository)
        {
            _fileTagRepository = fileTagRepository;
        }
        
        public async Task<QueryResult<FileTag>> Handle(QueryFileTag request, CancellationToken cancellationToken)
        {
            return await _fileTagRepository.QueryFileTag(request.ApplyCustomFilters());
        }
    }
}

