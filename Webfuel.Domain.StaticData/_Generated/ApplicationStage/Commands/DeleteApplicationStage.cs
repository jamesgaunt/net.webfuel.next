using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteApplicationStage: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteApplicationStageHandler : IRequestHandler<DeleteApplicationStage>
    {
        private readonly IApplicationStageRepository _applicationStageRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public DeleteApplicationStageHandler(IApplicationStageRepository applicationStageRepository, IStaticDataCache staticDataCache)
        {
            _applicationStageRepository = applicationStageRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(DeleteApplicationStage request, CancellationToken cancellationToken)
        {
            await _applicationStageRepository.DeleteApplicationStage(request.Id);
            _staticDataCache.FlushStaticData();
        }
    }
}

