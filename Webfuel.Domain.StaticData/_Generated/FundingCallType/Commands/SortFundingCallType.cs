using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class SortFundingCallType: IRequest
    {
        public required IEnumerable<Guid> Ids { get; set; }
    }
    internal class SortFundingCallTypeHandler : IRequestHandler<SortFundingCallType>
    {
        private readonly IFundingCallTypeRepository _fundingCallTypeRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public SortFundingCallTypeHandler(IFundingCallTypeRepository fundingCallTypeRepository, IStaticDataCache staticDataCache)
        {
            _fundingCallTypeRepository = fundingCallTypeRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(SortFundingCallType request, CancellationToken cancellationToken)
        {
            var items = await _fundingCallTypeRepository.SelectFundingCallType();
            
            var index = 0;
            foreach (var id in request.Ids)
            {
                var original = items.FirstOrDefault(p => p.Id == id);
                if (original != null && original.SortOrder != index)
                {
                    var updated = original.Copy();
                    updated.SortOrder = index;
                    await _fundingCallTypeRepository.UpdateFundingCallType(updated: updated, original: original);
                }
                index++;
            }
            _staticDataCache.FlushStaticData();
        }
    }
}

