using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class UpdateWorkActivity: IRequest<WorkActivity>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class UpdateWorkActivityHandler : IRequestHandler<UpdateWorkActivity, WorkActivity>
    {
        private readonly IWorkActivityRepository _workActivityRepository;
        
        
        public UpdateWorkActivityHandler(IWorkActivityRepository workActivityRepository)
        {
            _workActivityRepository = workActivityRepository;
        }
        
        public async Task<WorkActivity> Handle(UpdateWorkActivity request, CancellationToken cancellationToken)
        {
            var original = await _workActivityRepository.RequireWorkActivity(request.Id);
            
            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Default = request.Default;
            updated.Hidden = request.Hidden;
            updated.FreeText = request.FreeText;
            
            return await _workActivityRepository.UpdateWorkActivity(original: original, updated: updated);
        }
    }
}

