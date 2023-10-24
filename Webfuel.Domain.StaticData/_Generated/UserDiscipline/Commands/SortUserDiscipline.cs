using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class SortUserDiscipline: IRequest
    {
        public required IEnumerable<Guid> Ids { get; set; }
    }
    internal class SortUserDisciplineHandler : IRequestHandler<SortUserDiscipline>
    {
        private readonly IUserDisciplineRepository _userDisciplineRepository;
        
        
        public SortUserDisciplineHandler(IUserDisciplineRepository userDisciplineRepository)
        {
            _userDisciplineRepository = userDisciplineRepository;
        }
        
        public async Task Handle(SortUserDiscipline request, CancellationToken cancellationToken)
        {
            var items = await _userDisciplineRepository.SelectUserDiscipline();
            
            var index = 0;
            foreach (var id in request.Ids)
            {
                var original = items.FirstOrDefault(p => p.Id == id);
                if (original != null && original.SortOrder != index)
                {
                    var updated = original.Copy();
                    updated.SortOrder = index;
                    await _userDisciplineRepository.UpdateUserDiscipline(updated: updated, original: original);
                }
                index++;
            }
        }
    }
}

