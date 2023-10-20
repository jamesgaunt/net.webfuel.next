using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateGender: IRequest<Gender>
    {
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class CreateGenderHandler : IRequestHandler<CreateGender, Gender>
    {
        private readonly IGenderRepository _genderRepository;
        
        
        public CreateGenderHandler(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }
        
        public async Task<Gender> Handle(CreateGender request, CancellationToken cancellationToken)
        {
            return await _genderRepository.InsertGender(new Gender {
                    Name = request.Name,
                    Default = request.Default,
                    Hidden = request.Hidden,
                    FreeText = request.FreeText,
                    SortOrder = await _genderRepository.CountGender(),
                });
        }
    }
}

