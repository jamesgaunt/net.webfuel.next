using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class UpdateApplicationStage: IRequest<ApplicationStage>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class UpdateApplicationStageHandler : IRequestHandler<UpdateApplicationStage, ApplicationStage>
    {
        private readonly IApplicationStageRepository _applicationStageRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public UpdateApplicationStageHandler(IApplicationStageRepository applicationStageRepository, IStaticDataCache staticDataCache)
        {
            _applicationStageRepository = applicationStageRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<ApplicationStage> Handle(UpdateApplicationStage request, CancellationToken cancellationToken)
        {
            var original = await _applicationStageRepository.RequireApplicationStage(request.Id);
            
            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Default = request.Default;
            updated.Hidden = request.Hidden;
            updated.FreeText = request.FreeText;
            
            updated = await _applicationStageRepository.UpdateApplicationStage(original: original, updated: updated);
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

