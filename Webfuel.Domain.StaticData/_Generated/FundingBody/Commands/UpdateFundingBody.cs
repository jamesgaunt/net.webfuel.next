using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class UpdateFundingBody: IRequest<FundingBody>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        public bool Hidden { get; set; }
        public bool Default { get; set; }
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
            updated.Code = request.Code;
            updated.Hidden = request.Hidden;
            updated.Default = request.Default;
            
            return await _fundingBodyRepository.UpdateFundingBody(original: original, updated: updated);
        }
    }
}
