using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateUserDiscipline: IRequest<UserDiscipline>
    {
        public required string Name { get; set; }
        public string Alias { get; set; } = String.Empty;
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class CreateUserDisciplineHandler : IRequestHandler<CreateUserDiscipline, UserDiscipline>
    {
        private readonly IUserDisciplineRepository _userDisciplineRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public CreateUserDisciplineHandler(IUserDisciplineRepository userDisciplineRepository, IStaticDataCache staticDataCache)
        {
            _userDisciplineRepository = userDisciplineRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<UserDiscipline> Handle(CreateUserDiscipline request, CancellationToken cancellationToken)
        {
            var updated = await _userDisciplineRepository.InsertUserDiscipline(new UserDiscipline {
                    Name = request.Name,
                    Alias = request.Alias,
                    Default = request.Default,
                    Hidden = request.Hidden,
                    FreeText = request.FreeText,
                    SortOrder = await _userDisciplineRepository.CountUserDiscipline(),
                });
            
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

