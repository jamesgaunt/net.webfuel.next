using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class UpdateFundingBody: IRequest<FundingBody>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class UpdateFundingBodyHandler : IRequestHandler<UpdateFundingBody, FundingBody>
    {
        private readonly IFundingBodyRepository _fundingBodyRepository;
        
        
        public UpdateFundingBodyHandler(IFundingBodyRepository fundingBodyRepository)
        {
            _fundingBodyRepository = fundingBodyRepository;
        }
        
        public async Task<FundingBody> Handle(UpdateFundingBody request, CancellationToken cancellationToken)
        {
            var original = await _fundingBodyRepository.RequireFundingBody(request.Id);
            
            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Default = request.Default;
            updated.Hidden = request.Hidden;
            updated.FreeText = request.FreeText;
            
            return await _fundingBodyRepository.UpdateFundingBody(original: original, updated: updated);
        }
    }
}

