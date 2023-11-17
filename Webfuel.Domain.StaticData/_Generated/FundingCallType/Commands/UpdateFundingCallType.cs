using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class UpdateFundingCallType: IRequest<FundingCallType>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
    }
    internal class UpdateFundingCallTypeHandler : IRequestHandler<UpdateFundingCallType, FundingCallType>
    {
        private readonly IFundingCallTypeRepository _fundingCallTypeRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public UpdateFundingCallTypeHandler(IFundingCallTypeRepository fundingCallTypeRepository, IStaticDataCache staticDataCache)
        {
            _fundingCallTypeRepository = fundingCallTypeRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<FundingCallType> Handle(UpdateFundingCallType request, CancellationToken cancellationToken)
        {
            var original = await _fundingCallTypeRepository.RequireFundingCallType(request.Id);
            
            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Default = request.Default;
            updated.Hidden = request.Hidden;
            
            updated = await _fundingCallTypeRepository.UpdateFundingCallType(original: original, updated: updated);
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

