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
        private readonly IStaticDataCache _staticDataCache;
        
        
        public UpdateFundingBodyHandler(IFundingBodyRepository fundingBodyRepository, IStaticDataCache staticDataCache)
        {
            _fundingBodyRepository = fundingBodyRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<FundingBody> Handle(UpdateFundingBody request, CancellationToken cancellationToken)
        {
            var original = await _fundingBodyRepository.RequireFundingBody(request.Id);
            
            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Default = request.Default;
            updated.Hidden = request.Hidden;
            updated.FreeText = request.FreeText;
            
            updated = await _fundingBodyRepository.UpdateFundingBody(original: original, updated: updated);
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

