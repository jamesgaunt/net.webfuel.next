using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateWorkActivity: IRequest<WorkActivity>
    {
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class CreateWorkActivityHandler : IRequestHandler<CreateWorkActivity, WorkActivity>
    {
        private readonly IWorkActivityRepository _workActivityRepository;
        
        
        public CreateWorkActivityHandler(IWorkActivityRepository workActivityRepository)
        {
            _workActivityRepository = workActivityRepository;
        }
        
        public async Task<WorkActivity> Handle(CreateWorkActivity request, CancellationToken cancellationToken)
        {
            return await _workActivityRepository.InsertWorkActivity(new WorkActivity {
                    Name = request.Name,
                    Default = request.Default,
                    Hidden = request.Hidden,
                    FreeText = request.FreeText,
                    SortOrder = await _workActivityRepository.CountWorkActivity(),
                });
        }
    }
}

