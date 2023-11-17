using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteFundingCallType: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteFundingCallTypeHandler : IRequestHandler<DeleteFundingCallType>
    {
        private readonly IFundingCallTypeRepository _fundingCallTypeRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public DeleteFundingCallTypeHandler(IFundingCallTypeRepository fundingCallTypeRepository, IStaticDataCache staticDataCache)
        {
            _fundingCallTypeRepository = fundingCallTypeRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(DeleteFundingCallType request, CancellationToken cancellationToken)
        {
            await _fundingCallTypeRepository.DeleteFundingCallType(request.Id);
            _staticDataCache.FlushStaticData();
        }
    }
}

