using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateFundingBody: IRequest<FundingBody>
    {
        public required string Name { get; set; }
        public required string Code { get; set; }
        public bool Hidden { get; set; }
        public bool Default { get; set; }
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
                    Code = request.Code,
                    Hidden = request.Hidden,
                    Default = request.Default,
                    SortOrder = await _fundingBodyRepository.CountFundingBody()
                });
        }
    }
}
