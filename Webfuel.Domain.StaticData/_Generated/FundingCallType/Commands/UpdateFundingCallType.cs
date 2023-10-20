using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class UpdateFundingCallType: IRequest<FundingCallType>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class UpdateFundingCallTypeHandler : IRequestHandler<UpdateFundingCallType, FundingCallType>
    {
        private readonly IFundingCallTypeRepository _fundingCallTypeRepository;
        
        
        public UpdateFundingCallTypeHandler(IFundingCallTypeRepository fundingCallTypeRepository)
        {
            _fundingCallTypeRepository = fundingCallTypeRepository;
        }
        
        public async Task<FundingCallType> Handle(UpdateFundingCallType request, CancellationToken cancellationToken)
        {
            var original = await _fundingCallTypeRepository.RequireFundingCallType(request.Id);
            
            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Default = request.Default;
            updated.Hidden = request.Hidden;
            updated.FreeText = request.FreeText;
            
            return await _fundingCallTypeRepository.UpdateFundingCallType(original: original, updated: updated);
        }
    }
}

