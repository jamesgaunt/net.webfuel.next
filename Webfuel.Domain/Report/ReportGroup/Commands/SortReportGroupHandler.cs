using MediatR;

namespace Webfuel.Domain
{
    internal class SortReportGroupHandler : IRequestHandler<SortReportGroup>
    {
        private readonly IReportGroupRepository _genderRepository;
        
        public SortReportGroupHandler(IReportGroupRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }
        
        public async Task Handle(SortReportGroup request, CancellationToken cancellationToken)
        {
            var items = await _genderRepository.SelectReportGroup();
            
            var index = 0;
            foreach (var id in request.Ids)
            {
                var original = items.FirstOrDefault(p => p.Id == id);
                if (original != null && original.SortOrder != index)
                {
                    var updated = original.Copy();
                    updated.SortOrder = index;
                    await _genderRepository.UpdateReportGroup(updated: updated, original: original);
                }
                index++;
            }
        }
    }
}

