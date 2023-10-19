using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class UpdateApplicationStage: IRequest<ApplicationStage>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public bool Hidden { get; set; }
        public bool Default { get; set; }
        public bool FreeText { get; set; }
    }
    internal class UpdateApplicationStageHandler : IRequestHandler<UpdateApplicationStage, ApplicationStage>
    {
        private readonly IApplicationStageRepository _applicationStageRepository;
        
        
        public UpdateApplicationStageHandler(IApplicationStageRepository applicationStageRepository)
        {
            _applicationStageRepository = applicationStageRepository;
        }
        
        public async Task<ApplicationStage> Handle(UpdateApplicationStage request, CancellationToken cancellationToken)
        {
            var original = await _applicationStageRepository.RequireApplicationStage(request.Id);
            
            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Hidden = request.Hidden;
            updated.Default = request.Default;
            updated.FreeText = request.FreeText;
            
            return await _applicationStageRepository.UpdateApplicationStage(original: original, updated: updated);
        }
    }
}

