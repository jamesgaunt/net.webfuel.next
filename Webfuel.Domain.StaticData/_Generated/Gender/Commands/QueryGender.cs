using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryGender: Query, IRequest<QueryResult<Gender>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(Gender.Name), Search);
            return this;
        }
    }
    internal class QueryGenderHandler : IRequestHandler<QueryGender, QueryResult<Gender>>
    {
        private readonly IGenderRepository _genderRepository;
        
        
        public QueryGenderHandler(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }
        
        public async Task<QueryResult<Gender>> Handle(QueryGender request, CancellationToken cancellationToken)
        {
            return await _genderRepository.QueryGender(request.ApplyCustomFilters());
        }
    }
}

