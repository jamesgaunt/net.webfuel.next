using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateFundingBody: IRequest<FundingBody>
    {
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class CreateFundingBodyHandler : IRequestHandler<CreateFundingBody, FundingBody>
    {
        private readonly IFundingBodyRepository _fundingBodyRepository;
        
        
        public CreateFundingBodyHandler(IFundingBodyRepository fundingBodyRepository)
        {
            _fundingBodyRepository = fundingBodyRepository;
        }
        
        public async Task<FundingBody> Handle(CreateFundingBody request, CancellationToken cancellationToken)
        {
            return await _fundingBodyRepository.InsertFundingBody(new FundingBody {
                    Name = request.Name,
                    Default = request.Default,
                    Hidden = request.Hidden,
                    FreeText = request.FreeText,
                    SortOrder = await _fundingBodyRepository.CountFundingBody(),
                });
        }
    }
}

