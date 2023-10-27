using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateFundingCallType: IRequest<FundingCallType>
    {
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
    }
    internal class CreateFundingCallTypeHandler : IRequestHandler<CreateFundingCallType, FundingCallType>
    {
        private readonly IFundingCallTypeRepository _fundingCallTypeRepository;
        
        
        public CreateFundingCallTypeHandler(IFundingCallTypeRepository fundingCallTypeRepository)
        {
            _fundingCallTypeRepository = fundingCallTypeRepository;
        }
        
        public async Task<FundingCallType> Handle(CreateFundingCallType request, CancellationToken cancellationToken)
        {
            return await _fundingCallTypeRepository.InsertFundingCallType(new FundingCallType {
                    Name = request.Name,
                    Default = request.Default,
                    Hidden = request.Hidden,
                    SortOrder = await _fundingCallTypeRepository.CountFundingCallType(),
                });
        }
    }
}

