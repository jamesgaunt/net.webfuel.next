using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateSupportProvided: IRequest<SupportProvided>
    {
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class CreateSupportProvidedHandler : IRequestHandler<CreateSupportProvided, SupportProvided>
    {
        private readonly ISupportProvidedRepository _supportProvidedRepository;
        
        
        public CreateSupportProvidedHandler(ISupportProvidedRepository supportProvidedRepository)
        {
            _supportProvidedRepository = supportProvidedRepository;
        }
        
        public async Task<SupportProvided> Handle(CreateSupportProvided request, CancellationToken cancellationToken)
        {
            return await _supportProvidedRepository.InsertSupportProvided(new SupportProvided {
                    Name = request.Name,
                    Default = request.Default,
                    Hidden = request.Hidden,
                    FreeText = request.FreeText,
                    SortOrder = await _supportProvidedRepository.CountSupportProvided(),
                });
        }
    }
}

