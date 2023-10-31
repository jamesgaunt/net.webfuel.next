using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class UpdateAgeRange: IRequest<AgeRange>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
    }
    internal class UpdateAgeRangeHandler : IRequestHandler<UpdateAgeRange, AgeRange>
    {
        private readonly IAgeRangeRepository _ageRangeRepository;
        
        
        public UpdateAgeRangeHandler(IAgeRangeRepository ageRangeRepository)
        {
            _ageRangeRepository = ageRangeRepository;
        }
        
        public async Task<AgeRange> Handle(UpdateAgeRange request, CancellationToken cancellationToken)
        {
            var original = await _ageRangeRepository.RequireAgeRange(request.Id);
            
            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Default = request.Default;
            
            return await _ageRangeRepository.UpdateAgeRange(original: original, updated: updated);
        }
    }
}

