using Webfuel.Reporting;

namespace Webfuel.Domain
{
    [Service(typeof(IReportMapper<User>))]
    internal class UserReportMapper : IReportMapper<User>
    {
        private readonly IUserRepository _repository;
        
        public UserReportMapper(IUserRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<object?> Get(Guid id)
        {
            return await _repository.GetUser(id);
        }
        
        public async Task<QueryResult<object>> Query(Query query)
        {
            query.Contains(nameof(User.FullName), query.Search);

            var result = await _repository.QueryUser(query);
            
            return new QueryResult<object>
            {
                TotalCount = result.TotalCount,
                Items = result.Items
            };
        }
        
        public Guid Id(object reference)
        {
            if (reference is not User entity)
            throw new Exception($"Cannot get id of type {reference.GetType()}");
            return entity.Id;
        }
        
        public string DisplayName(object reference)
        {
            if (reference is not User entity)
            throw new Exception($"Cannot get display name of type {reference.GetType()}");
            return entity.FullName;
        }
    }
}
