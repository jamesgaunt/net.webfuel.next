using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateStaffRole: IRequest<StaffRole>
    {
        public required string Name { get; set; }
        public string Alias { get; set; } = String.Empty;
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class CreateStaffRoleHandler : IRequestHandler<CreateStaffRole, StaffRole>
    {
        private readonly IStaffRoleRepository _staffRoleRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public CreateStaffRoleHandler(IStaffRoleRepository staffRoleRepository, IStaticDataCache staticDataCache)
        {
            _staffRoleRepository = staffRoleRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<StaffRole> Handle(CreateStaffRole request, CancellationToken cancellationToken)
        {
            var updated = await _staffRoleRepository.InsertStaffRole(new StaffRole {
                    Name = request.Name,
                    Alias = request.Alias,
                    Default = request.Default,
                    Hidden = request.Hidden,
                    FreeText = request.FreeText,
                    SortOrder = await _staffRoleRepository.CountStaffRole(),
                });
            
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

