using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteHowDidYouFindUs: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteHowDidYouFindUsHandler : IRequestHandler<DeleteHowDidYouFindUs>
    {
        private readonly IHowDidYouFindUsRepository _howDidYouFindUsRepository;
        
        
        public DeleteHowDidYouFindUsHandler(IHowDidYouFindUsRepository howDidYouFindUsRepository)
        {
            _howDidYouFindUsRepository = howDidYouFindUsRepository;
        }
        
        public async Task Handle(DeleteHowDidYouFindUs request, CancellationToken cancellationToken)
        {
            await _howDidYouFindUsRepository.DeleteHowDidYouFindUs(request.Id);
        }
    }
}

