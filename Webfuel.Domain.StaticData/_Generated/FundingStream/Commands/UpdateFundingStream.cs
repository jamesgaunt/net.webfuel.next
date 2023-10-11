using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class UpdateFundingStream: IRequest<FundingStream>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        public bool Hidden { get; set; }
        public bool Default { get; set; }
    }
    internal class UpdateFundingStreamHandler : IRequestHandler<UpdateFundingStream, FundingStream>
    {
        private readonly IFundingStreamRepository _fundingStreamRepository;
        
        
        public UpdateFundingStreamHandler(IFundingStreamRepository fundingStreamRepository)
        {
            _fundingStreamRepository = fundingStreamRepository;
        }
        
        public async Task<FundingStream> Handle(UpdateFundingStream request, CancellationToken cancellationToken)
        {
            var original = await _fundingStreamRepository.RequireFundingStream(request.Id);
            
            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Code = request.Code;
            updated.Hidden = request.Hidden;
            updated.Default = request.Default;
            
            return await _fundingStreamRepository.UpdateFundingStream(original: original, updated: updated);
        }
    }
}

