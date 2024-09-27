using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class SortStaffRole: IRequest
    {
        public required IEnumerable<Guid> Ids { get; set; }
    }
    internal class SortStaffRoleHandler : IRequestHandler<SortStaffRole>
    {
        private readonly IStaffRoleRepository _staffRoleRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public SortStaffRoleHandler(IStaffRoleRepository staffRoleRepository, IStaticDataCache staticDataCache)
        {
            _staffRoleRepository = staffRoleRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(SortStaffRole request, CancellationToken cancellationToken)
        {
            var items = await _staffRoleRepository.SelectStaffRole();
            
            var index = 0;
            foreach (var id in request.Ids)
            {
                var original = items.FirstOrDefault(p => p.Id == id);
                if (original != null && original.SortOrder != index)
                {
                    var updated = original.Copy();
                    updated.SortOrder = index;
                    await _staffRoleRepository.UpdateStaffRole(updated: updated, original: original);
                }
                index++;
            }
            _staticDataCache.FlushStaticData();
        }
    }
}

