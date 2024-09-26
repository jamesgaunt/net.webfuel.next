using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateProfessionalBackgroundDetail: IRequest<ProfessionalBackgroundDetail>
    {
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class CreateProfessionalBackgroundDetailHandler : IRequestHandler<CreateProfessionalBackgroundDetail, ProfessionalBackgroundDetail>
    {
        private readonly IProfessionalBackgroundDetailRepository _professionalBackgroundDetailRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public CreateProfessionalBackgroundDetailHandler(IProfessionalBackgroundDetailRepository professionalBackgroundDetailRepository, IStaticDataCache staticDataCache)
        {
            _professionalBackgroundDetailRepository = professionalBackgroundDetailRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<ProfessionalBackgroundDetail> Handle(CreateProfessionalBackgroundDetail request, CancellationToken cancellationToken)
        {
            var updated = await _professionalBackgroundDetailRepository.InsertProfessionalBackgroundDetail(new ProfessionalBackgroundDetail {
                    Name = request.Name,
                    Default = request.Default,
                    Hidden = request.Hidden,
                    FreeText = request.FreeText,
                    SortOrder = await _professionalBackgroundDetailRepository.CountProfessionalBackgroundDetail(),
                });
            
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

