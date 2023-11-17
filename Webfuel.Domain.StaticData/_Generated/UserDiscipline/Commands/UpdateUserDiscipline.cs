using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class UpdateUserDiscipline: IRequest<UserDiscipline>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class UpdateUserDisciplineHandler : IRequestHandler<UpdateUserDiscipline, UserDiscipline>
    {
        private readonly IUserDisciplineRepository _userDisciplineRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public UpdateUserDisciplineHandler(IUserDisciplineRepository userDisciplineRepository, IStaticDataCache staticDataCache)
        {
            _userDisciplineRepository = userDisciplineRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<UserDiscipline> Handle(UpdateUserDiscipline request, CancellationToken cancellationToken)
        {
            var original = await _userDisciplineRepository.RequireUserDiscipline(request.Id);
            
            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Default = request.Default;
            updated.Hidden = request.Hidden;
            updated.FreeText = request.FreeText;
            
            updated = await _userDisciplineRepository.UpdateUserDiscipline(original: original, updated: updated);
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

