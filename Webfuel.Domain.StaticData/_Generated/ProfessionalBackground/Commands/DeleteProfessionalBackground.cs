using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteProfessionalBackground: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteProfessionalBackgroundHandler : IRequestHandler<DeleteProfessionalBackground>
    {
        private readonly IProfessionalBackgroundRepository _professionalBackgroundRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public DeleteProfessionalBackgroundHandler(IProfessionalBackgroundRepository professionalBackgroundRepository, IStaticDataCache staticDataCache)
        {
            _professionalBackgroundRepository = professionalBackgroundRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(DeleteProfessionalBackground request, CancellationToken cancellationToken)
        {
            await _professionalBackgroundRepository.DeleteProfessionalBackground(request.Id);
            _staticDataCache.FlushStaticData();
        }
    }
}

