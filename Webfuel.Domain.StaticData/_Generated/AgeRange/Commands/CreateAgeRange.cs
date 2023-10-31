using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateAgeRange: IRequest<AgeRange>
    {
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
    }
    internal class CreateAgeRangeHandler : IRequestHandler<CreateAgeRange, AgeRange>
    {
        private readonly IAgeRangeRepository _ageRangeRepository;
        
        
        public CreateAgeRangeHandler(IAgeRangeRepository ageRangeRepository)
        {
            _ageRangeRepository = ageRangeRepository;
        }
        
        public async Task<AgeRange> Handle(CreateAgeRange request, CancellationToken cancellationToken)
        {
            return await _ageRangeRepository.InsertAgeRange(new AgeRange {
                    Name = request.Name,
                    Default = request.Default,
                    SortOrder = await _ageRangeRepository.CountAgeRange(),
                });
        }
    }
}

