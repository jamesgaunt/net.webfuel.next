using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class UpdateProfessionalBackgroundDetail: IRequest<ProfessionalBackgroundDetail>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class UpdateProfessionalBackgroundDetailHandler : IRequestHandler<UpdateProfessionalBackgroundDetail, ProfessionalBackgroundDetail>
    {
        private readonly IProfessionalBackgroundDetailRepository _professionalBackgroundDetailRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public UpdateProfessionalBackgroundDetailHandler(IProfessionalBackgroundDetailRepository professionalBackgroundDetailRepository, IStaticDataCache staticDataCache)
        {
            _professionalBackgroundDetailRepository = professionalBackgroundDetailRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<ProfessionalBackgroundDetail> Handle(UpdateProfessionalBackgroundDetail request, CancellationToken cancellationToken)
        {
            var original = await _professionalBackgroundDetailRepository.RequireProfessionalBackgroundDetail(request.Id);
            
            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Default = request.Default;
            updated.Hidden = request.Hidden;
            updated.FreeText = request.FreeText;
            
            updated = await _professionalBackgroundDetailRepository.UpdateProfessionalBackgroundDetail(original: original, updated: updated);
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

