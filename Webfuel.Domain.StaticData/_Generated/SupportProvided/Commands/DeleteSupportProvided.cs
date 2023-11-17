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
        private readonly IStaticDataCache _staticDataCache;
        
        
        public DeleteSupportProvidedHandler(ISupportProvidedRepository supportProvidedRepository, IStaticDataCache staticDataCache)
        {
            _supportProvidedRepository = supportProvidedRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(DeleteSupportProvided request, CancellationToken cancellationToken)
        {
            await _supportProvidedRepository.DeleteSupportProvided(request.Id);
            _staticDataCache.FlushStaticData();
        }
    }
}

