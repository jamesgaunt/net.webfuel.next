using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteProfessionalBackgroundDetail: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteProfessionalBackgroundDetailHandler : IRequestHandler<DeleteProfessionalBackgroundDetail>
    {
        private readonly IProfessionalBackgroundDetailRepository _professionalBackgroundDetailRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public DeleteProfessionalBackgroundDetailHandler(IProfessionalBackgroundDetailRepository professionalBackgroundDetailRepository, IStaticDataCache staticDataCache)
        {
            _professionalBackgroundDetailRepository = professionalBackgroundDetailRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(DeleteProfessionalBackgroundDetail request, CancellationToken cancellationToken)
        {
            await _professionalBackgroundDetailRepository.DeleteProfessionalBackgroundDetail(request.Id);
            _staticDataCache.FlushStaticData();
        }
    }
}

