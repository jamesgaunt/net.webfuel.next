using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteTitle: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteTitleHandler : IRequestHandler<DeleteTitle>
    {
        private readonly ITitleRepository _titleRepository;
        
        
        public DeleteTitleHandler(ITitleRepository titleRepository)
        {
            _titleRepository = titleRepository;
        }
        
        public async Task Handle(DeleteTitle request, CancellationToken cancellationToken)
        {
            await _titleRepository.DeleteTitle(request.Id);
        }
    }
}

