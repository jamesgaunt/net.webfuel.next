using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteEthnicity: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteEthnicityHandler : IRequestHandler<DeleteEthnicity>
    {
        private readonly IEthnicityRepository _ethnicityRepository;
        
        
        public DeleteEthnicityHandler(IEthnicityRepository ethnicityRepository)
        {
            _ethnicityRepository = ethnicityRepository;
        }
        
        public async Task Handle(DeleteEthnicity request, CancellationToken cancellationToken)
        {
            await _ethnicityRepository.DeleteEthnicity(request.Id);
        }
    }
}

