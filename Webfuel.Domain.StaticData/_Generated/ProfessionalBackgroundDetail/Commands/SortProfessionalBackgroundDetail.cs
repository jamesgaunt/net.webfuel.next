using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class SortProfessionalBackgroundDetail: IRequest
    {
        public required IEnumerable<Guid> Ids { get; set; }
    }
    internal class SortProfessionalBackgroundDetailHandler : IRequestHandler<SortProfessionalBackgroundDetail>
    {
        private readonly IProfessionalBackgroundDetailRepository _professionalBackgroundDetailRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public SortProfessionalBackgroundDetailHandler(IProfessionalBackgroundDetailRepository professionalBackgroundDetailRepository, IStaticDataCache staticDataCache)
        {
            _professionalBackgroundDetailRepository = professionalBackgroundDetailRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(SortProfessionalBackgroundDetail request, CancellationToken cancellationToken)
        {
            var items = await _professionalBackgroundDetailRepository.SelectProfessionalBackgroundDetail();
            
            var index = 0;
            foreach (var id in request.Ids)
            {
                var original = items.FirstOrDefault(p => p.Id == id);
                if (original != null && original.SortOrder != index)
                {
                    var updated = original.Copy();
                    updated.SortOrder = index;
                    await _professionalBackgroundDetailRepository.UpdateProfessionalBackgroundDetail(updated: updated, original: original);
                }
                index++;
            }
            _staticDataCache.FlushStaticData();
        }
    }
}

