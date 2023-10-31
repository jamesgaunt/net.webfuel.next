using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteAgeRange: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteAgeRangeHandler : IRequestHandler<DeleteAgeRange>
    {
        private readonly IAgeRangeRepository _ageRangeRepository;
        
        
        public DeleteAgeRangeHandler(IAgeRangeRepository ageRangeRepository)
        {
            _ageRangeRepository = ageRangeRepository;
        }
        
        public async Task Handle(DeleteAgeRange request, CancellationToken cancellationToken)
        {
            await _ageRangeRepository.DeleteAgeRange(request.Id);
        }
    }
}

