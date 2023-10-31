using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class UpdateEthnicity: IRequest<Ethnicity>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class UpdateEthnicityHandler : IRequestHandler<UpdateEthnicity, Ethnicity>
    {
        private readonly IEthnicityRepository _ethnicityRepository;
        
        
        public UpdateEthnicityHandler(IEthnicityRepository ethnicityRepository)
        {
            _ethnicityRepository = ethnicityRepository;
        }
        
        public async Task<Ethnicity> Handle(UpdateEthnicity request, CancellationToken cancellationToken)
        {
            var original = await _ethnicityRepository.RequireEthnicity(request.Id);
            
            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Default = request.Default;
            updated.Hidden = request.Hidden;
            updated.FreeText = request.FreeText;
            
            return await _ethnicityRepository.UpdateEthnicity(original: original, updated: updated);
        }
    }
}

