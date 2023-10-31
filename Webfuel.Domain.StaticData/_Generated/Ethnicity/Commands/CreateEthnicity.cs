using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateEthnicity: IRequest<Ethnicity>
    {
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class CreateEthnicityHandler : IRequestHandler<CreateEthnicity, Ethnicity>
    {
        private readonly IEthnicityRepository _ethnicityRepository;
        
        
        public CreateEthnicityHandler(IEthnicityRepository ethnicityRepository)
        {
            _ethnicityRepository = ethnicityRepository;
        }
        
        public async Task<Ethnicity> Handle(CreateEthnicity request, CancellationToken cancellationToken)
        {
            return await _ethnicityRepository.InsertEthnicity(new Ethnicity {
                    Name = request.Name,
                    Default = request.Default,
                    Hidden = request.Hidden,
                    FreeText = request.FreeText,
                    SortOrder = await _ethnicityRepository.CountEthnicity(),
                });
        }
    }
}

