using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class UpdateStaffRole: IRequest<StaffRole>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public string Alias { get; set; } = String.Empty;
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class UpdateStaffRoleHandler : IRequestHandler<UpdateStaffRole, StaffRole>
    {
        private readonly IStaffRoleRepository _staffRoleRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public UpdateStaffRoleHandler(IStaffRoleRepository staffRoleRepository, IStaticDataCache staticDataCache)
        {
            _staffRoleRepository = staffRoleRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<StaffRole> Handle(UpdateStaffRole request, CancellationToken cancellationToken)
        {
            var original = await _staffRoleRepository.RequireStaffRole(request.Id);
            
            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Alias = request.Alias;
            updated.Default = request.Default;
            updated.Hidden = request.Hidden;
            updated.FreeText = request.FreeText;
            
            updated = await _staffRoleRepository.UpdateStaffRole(original: original, updated: updated);
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

