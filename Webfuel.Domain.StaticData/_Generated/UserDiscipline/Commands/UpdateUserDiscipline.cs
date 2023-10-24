using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class UpdateUserDiscipline: IRequest<UserDiscipline>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class UpdateUserDisciplineHandler : IRequestHandler<UpdateUserDiscipline, UserDiscipline>
    {
        private readonly IUserDisciplineRepository _userDisciplineRepository;
        
        
        public UpdateUserDisciplineHandler(IUserDisciplineRepository userDisciplineRepository)
        {
            _userDisciplineRepository = userDisciplineRepository;
        }
        
        public async Task<UserDiscipline> Handle(UpdateUserDiscipline request, CancellationToken cancellationToken)
        {
            var original = await _userDisciplineRepository.RequireUserDiscipline(request.Id);
            
            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Default = request.Default;
            updated.Hidden = request.Hidden;
            updated.FreeText = request.FreeText;
            
            return await _userDisciplineRepository.UpdateUserDiscipline(original: original, updated: updated);
        }
    }
}

