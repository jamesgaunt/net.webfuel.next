using Webfuel.Reporting;

namespace Webfuel.Domain
{
    [Service(typeof(IReportMapper<ProjectSupport>))]
    internal class ProjectSupportReportMapper : IReportMapper<ProjectSupport>
    {
        private readonly IProjectSupportRepository _repository;
        
        public ProjectSupportReportMapper(IProjectSupportRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<object?> Get(Guid id)
        {
            return await _repository.GetProjectSupport(id);
        }

        public Task<QueryResult<ReferenceLookup>> Lookup(Query query)
        {
            throw new NotImplementedException();
        }
        
        public Guid Id(object reference)
        {
            if (reference is not ProjectSupport entity)
            throw new Exception($"Cannot get id of type {reference.GetType()}");
            return entity.Id;
        }
        
        public string Name(object reference)
        {
            throw new NotImplementedException();
        }
    }
}
