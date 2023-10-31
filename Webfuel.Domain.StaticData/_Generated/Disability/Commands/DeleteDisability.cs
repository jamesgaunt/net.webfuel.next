using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteDisability: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteDisabilityHandler : IRequestHandler<DeleteDisability>
    {
        private readonly IDisabilityRepository _disabilityRepository;
        
        
        public DeleteDisabilityHandler(IDisabilityRepository disabilityRepository)
        {
            _disabilityRepository = disabilityRepository;
        }
        
        public async Task Handle(DeleteDisability request, CancellationToken cancellationToken)
        {
            await _disabilityRepository.DeleteDisability(request.Id);
        }
    }
}

