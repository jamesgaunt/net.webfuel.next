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
        
        
        public DeleteApplicationStageHandler(IApplicationStageRepository applicationStageRepository)
        {
            _applicationStageRepository = applicationStageRepository;
        }
        
        public async Task Handle(DeleteApplicationStage request, CancellationToken cancellationToken)
        {
            await _applicationStageRepository.DeleteApplicationStage(request.Id);
        }
    }
}

