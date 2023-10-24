using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryUserDiscipline: Query, IRequest<QueryResult<UserDiscipline>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(UserDiscipline.Name), Search);
            return this;
        }
    }
    internal class QueryUserDisciplineHandler : IRequestHandler<QueryUserDiscipline, QueryResult<UserDiscipline>>
    {
        private readonly IUserDisciplineRepository _userDisciplineRepository;
        
        
        public QueryUserDisciplineHandler(IUserDisciplineRepository userDisciplineRepository)
        {
            _userDisciplineRepository = userDisciplineRepository;
        }
        
        public async Task<QueryResult<UserDiscipline>> Handle(QueryUserDiscipline request, CancellationToken cancellationToken)
        {
            return await _userDisciplineRepository.QueryUserDiscipline(request.ApplyCustomFilters());
        }
    }
}

