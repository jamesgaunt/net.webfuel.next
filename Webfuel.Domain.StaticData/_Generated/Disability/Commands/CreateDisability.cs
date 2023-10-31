using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateDisability: IRequest<Disability>
    {
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class CreateDisabilityHandler : IRequestHandler<CreateDisability, Disability>
    {
        private readonly IDisabilityRepository _disabilityRepository;
        
        
        public CreateDisabilityHandler(IDisabilityRepository disabilityRepository)
        {
            _disabilityRepository = disabilityRepository;
        }
        
        public async Task<Disability> Handle(CreateDisability request, CancellationToken cancellationToken)
        {
            return await _disabilityRepository.InsertDisability(new Disability {
                    Name = request.Name,
                    Default = request.Default,
                    Hidden = request.Hidden,
                    FreeText = request.FreeText,
                    SortOrder = await _disabilityRepository.CountDisability(),
                });
        }
    }
}

