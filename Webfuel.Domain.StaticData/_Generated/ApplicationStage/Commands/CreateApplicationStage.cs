using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateApplicationStage: IRequest<ApplicationStage>
    {
        public required string Name { get; set; }
        public bool Hidden { get; set; }
        public bool Default { get; set; }
        public bool FreeText { get; set; }
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
                    Hidden = request.Hidden,
                    Default = request.Default,
                    FreeText = request.FreeText,
                    SortOrder = await _applicationStageRepository.CountApplicationStage()
                });
        }
    }
}

