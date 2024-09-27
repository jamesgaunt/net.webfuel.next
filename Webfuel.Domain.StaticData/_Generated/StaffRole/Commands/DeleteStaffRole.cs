using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteStaffRole: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteStaffRoleHandler : IRequestHandler<DeleteStaffRole>
    {
        private readonly IStaffRoleRepository _staffRoleRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public DeleteStaffRoleHandler(IStaffRoleRepository staffRoleRepository, IStaticDataCache staticDataCache)
        {
            _staffRoleRepository = staffRoleRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(DeleteStaffRole request, CancellationToken cancellationToken)
        {
            await _staffRoleRepository.DeleteStaffRole(request.Id);
            _staticDataCache.FlushStaticData();
        }
    }
}

