using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateFundingStream: IRequest<FundingStream>
    {
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
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
                    Default = request.Default,
                    Hidden = request.Hidden,
                    FreeText = request.FreeText,
                    SortOrder = await _fundingStreamRepository.CountFundingStream(),
                });
        }
    }
}

