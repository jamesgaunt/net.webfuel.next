using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateApplicationStage: IRequest<ApplicationStage>
    {
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class CreateApplicationStageHandler : IRequestHandler<CreateApplicationStage, ApplicationStage>
    {
        private readonly IApplicationStageRepository _applicationStageRepository;
        
        
        public CreateApplicationStageHandler(IApplicationStageRepository applicationStageRepository)
        {
            _applicationStageRepository = applicationStageRepository;
        }
        
        public async Task<ApplicationStage> Handle(CreateApplicationStage request, CancellationToken cancellationToken)
        {
            return await _applicationStageRepository.InsertApplicationStage(new ApplicationStage {
                    Name = request.Name,
                    Default = request.Default,
                    Hidden = request.Hidden,
                    FreeText = request.FreeText,
                    SortOrder = await _applicationStageRepository.CountApplicationStage(),
                });
        }
    }
}

