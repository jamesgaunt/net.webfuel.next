using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateTitle: IRequest<Title>
    {
        public required string Name { get; set; }
        public required string Code { get; set; }
        public bool Hidden { get; set; }
        public bool Default { get; set; }
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
                    Code = request.Code,
                    Hidden = request.Hidden,
                    Default = request.Default,
                    SortOrder = await _titleRepository.CountTitle()
                });
        }
    }
}

