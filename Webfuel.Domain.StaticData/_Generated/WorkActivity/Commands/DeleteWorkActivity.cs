using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteWorkActivity: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteWorkActivityHandler : IRequestHandler<DeleteWorkActivity>
    {
        private readonly IWorkActivityRepository _workActivityRepository;
        
        
        public DeleteWorkActivityHandler(IWorkActivityRepository workActivityRepository)
        {
            _workActivityRepository = workActivityRepository;
        }
        
        public async Task Handle(DeleteWorkActivity request, CancellationToken cancellationToken)
        {
            await _workActivityRepository.DeleteWorkActivity(request.Id);
        }
    }
}

