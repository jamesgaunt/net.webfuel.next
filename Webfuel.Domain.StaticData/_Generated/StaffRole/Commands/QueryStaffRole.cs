using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryStaffRole: Query, IRequest<QueryResult<StaffRole>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(StaffRole.Name), Search);
            return this;
        }
    }
    internal class QueryStaffRoleHandler : IRequestHandler<QueryStaffRole, QueryResult<StaffRole>>
    {
        private readonly IStaffRoleRepository _staffRoleRepository;
        
        
        public QueryStaffRoleHandler(IStaffRoleRepository staffRoleRepository)
        {
            _staffRoleRepository = staffRoleRepository;
        }
        
        public async Task<QueryResult<StaffRole>> Handle(QueryStaffRole request, CancellationToken cancellationToken)
        {
            return await _staffRoleRepository.QueryStaffRole(request.ApplyCustomFilters());
        }
    }
}

