using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteGender: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteGenderHandler : IRequestHandler<DeleteGender>
    {
        private readonly IGenderRepository _genderRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public DeleteGenderHandler(IGenderRepository genderRepository, IStaticDataCache staticDataCache)
        {
            _genderRepository = genderRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(DeleteGender request, CancellationToken cancellationToken)
        {
            await _genderRepository.DeleteGender(request.Id);
            _staticDataCache.FlushStaticData();
        }
    }
}

