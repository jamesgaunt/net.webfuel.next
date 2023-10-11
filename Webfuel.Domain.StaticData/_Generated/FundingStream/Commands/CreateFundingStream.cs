using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateFundingStream: IRequest<FundingStream>
    {
        public required string Name { get; set; }
        public required string Code { get; set; }
        public bool Hidden { get; set; }
        public bool Default { get; set; }
    }
    internal class CreateFundingStreamHandler : IRequestHandler<CreateFundingStream, FundingStream>
    {
        private readonly IFundingStreamRepository _fundingStreamRepository;
        
        
        public CreateFundingStreamHandler(IFundingStreamRepository fundingStreamRepository)
        {
            _fundingStreamRepository = fundingStreamRepository;
        }
        
        public async Task<FundingStream> Handle(CreateFundingStream request, CancellationToken cancellationToken)
        {
            return await _fundingStreamRepository.InsertFundingStream(new FundingStream {
                    Name = request.Name,
                    Code = request.Code,
                    Hidden = request.Hidden,
                    Default = request.Default,
                    SortOrder = await _fundingStreamRepository.CountFundingStream()
                });
        }
    }
}

