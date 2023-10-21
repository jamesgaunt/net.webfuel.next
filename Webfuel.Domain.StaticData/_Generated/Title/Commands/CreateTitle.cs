using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateTitle: IRequest<Title>
    {
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
    }
    internal class CreateTitleHandler : IRequestHandler<CreateTitle, Title>
    {
        private readonly ITitleRepository _titleRepository;
        
        
        public CreateTitleHandler(ITitleRepository titleRepository)
        {
            _titleRepository = titleRepository;
        }
        
        public async Task<Title> Handle(CreateTitle request, CancellationToken cancellationToken)
        {
            return await _titleRepository.InsertTitle(new Title {
                    Name = request.Name,
                    Default = request.Default,
                    SortOrder = await _titleRepository.CountTitle(),
                });
        }
    }
}

