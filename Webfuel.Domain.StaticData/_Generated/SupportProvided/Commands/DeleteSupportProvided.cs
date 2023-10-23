using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteSupportProvided: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteSupportProvidedHandler : IRequestHandler<DeleteSupportProvided>
    {
        private readonly ISupportProvidedRepository _supportProvidedRepository;
        
        
        public DeleteSupportProvidedHandler(ISupportProvidedRepository supportProvidedRepository)
        {
            _supportProvidedRepository = supportProvidedRepository;
        }
        
        public async Task Handle(DeleteSupportProvided request, CancellationToken cancellationToken)
        {
            await _supportProvidedRepository.DeleteSupportProvided(request.Id);
        }
    }
}

