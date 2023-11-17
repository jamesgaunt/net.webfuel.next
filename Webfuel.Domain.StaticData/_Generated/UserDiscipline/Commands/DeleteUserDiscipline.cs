using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteUserDiscipline: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteUserDisciplineHandler : IRequestHandler<DeleteUserDiscipline>
    {
        private readonly IUserDisciplineRepository _userDisciplineRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public DeleteUserDisciplineHandler(IUserDisciplineRepository userDisciplineRepository, IStaticDataCache staticDataCache)
        {
            _userDisciplineRepository = userDisciplineRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(DeleteUserDiscipline request, CancellationToken cancellationToken)
        {
            await _userDisciplineRepository.DeleteUserDiscipline(request.Id);
            _staticDataCache.FlushStaticData();
        }
    }
}

