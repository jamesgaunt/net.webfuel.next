using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class UpdateGender: IRequest<Gender>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
    }
    internal class UpdateGenderHandler : IRequestHandler<UpdateGender, Gender>
    {
        private readonly IGenderRepository _genderRepository;
        
        
        public UpdateGenderHandler(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }
        
        public async Task<Gender> Handle(UpdateGender request, CancellationToken cancellationToken)
        {
            var original = await _genderRepository.RequireGender(request.Id);
            
            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Code = request.Code;
            
            return await _genderRepository.UpdateGender(original: original, updated: updated);
        }
    }
}

