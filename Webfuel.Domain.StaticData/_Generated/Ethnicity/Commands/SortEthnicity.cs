using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class SortEthnicity: IRequest
    {
        public required IEnumerable<Guid> Ids { get; set; }
    }
    internal class SortEthnicityHandler : IRequestHandler<SortEthnicity>
    {
        private readonly IEthnicityRepository _ethnicityRepository;
        
        
        public SortEthnicityHandler(IEthnicityRepository ethnicityRepository)
        {
            _ethnicityRepository = ethnicityRepository;
        }
        
        public async Task Handle(SortEthnicity request, CancellationToken cancellationToken)
        {
            var items = await _ethnicityRepository.SelectEthnicity();
            
            var index = 0;
            foreach (var id in request.Ids)
            {
                var original = items.FirstOrDefault(p => p.Id == id);
                if (original != null && original.SortOrder != index)
                {
                    var updated = original.Copy();
                    updated.SortOrder = index;
                    await _ethnicityRepository.UpdateEthnicity(updated: updated, original: original);
                }
                index++;
            }
        }
    }
}

