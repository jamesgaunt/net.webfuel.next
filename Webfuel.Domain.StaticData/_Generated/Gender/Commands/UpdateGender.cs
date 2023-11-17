using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class UpdateGender: IRequest<Gender>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class UpdateGenderHandler : IRequestHandler<UpdateGender, Gender>
    {
        private readonly IGenderRepository _genderRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public UpdateGenderHandler(IGenderRepository genderRepository, IStaticDataCache staticDataCache)
        {
            _genderRepository = genderRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<Gender> Handle(UpdateGender request, CancellationToken cancellationToken)
        {
            var original = await _genderRepository.RequireGender(request.Id);
            
            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Default = request.Default;
            updated.Hidden = request.Hidden;
            updated.FreeText = request.FreeText;
            
            updated = await _genderRepository.UpdateGender(original: original, updated: updated);
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

