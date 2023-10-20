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
        
        
        public DeleteFundingCallTypeHandler(IFundingCallTypeRepository fundingCallTypeRepository)
        {
            _fundingCallTypeRepository = fundingCallTypeRepository;
        }
        
        public async Task Handle(DeleteFundingCallType request, CancellationToken cancellationToken)
        {
            await _fundingCallTypeRepository.DeleteFundingCallType(request.Id);
        }
    }
}

