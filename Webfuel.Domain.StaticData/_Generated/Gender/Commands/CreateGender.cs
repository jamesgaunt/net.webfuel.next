using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateGender: IRequest<Gender>
    {
        public required string Name { get; set; }
        public required string Code { get; set; }
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
                    Code = request.Code,
                    SortOrder = await _genderRepository.CountGender()
                });
        }
    }
}

