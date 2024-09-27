using MediatR;

namespace Webfuel.Domain
{
    public class UpdateUser : IRequest<User>
    {
        public Guid Id { get; set; }

        public required string Email { get; set; }
        public required string Title { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required Guid UserGroupId { get; set; }

        public required string StaffRole { get; set; }
        public required string StaffRoleFreeText { get; set; }

        public required string UniversityJobTitle { get; set; }
        public required List<Guid> DisciplineIds { get; set; }
        public required string DisciplineFreeText { get; set; }
        public Guid? SiteId { get; set; }

        public required Guid? ProfessionalBackgroundId { get; set; }
        public required string ProfessionalBackgroundFreeText { get; set; }

        public required Guid? ProfessionalBackgroundDetailId { get; set; }
        public required string ProfessionalBackgroundDetailFreeText { get; set; }

        public required DateOnly? StartDateForRSS { get; set; }
        public required DateOnly? EndDateForRSS { get; set; }
        public decimal? FullTimeEquivalentForRSS { get; set; }

        public bool Disabled { get; set; }
        public bool Hidden { get; set; }
    }

    internal class UpdateUserHandler : IRequestHandler<UpdateUser, User>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(UpdateUser request, CancellationToken cancellationToken)
        {
            var original = await _userRepository.RequireUser(request.Id);

            var updated = original.Copy();
            updated.Email = request.Email;
            updated.Title = request.Title;
            updated.FirstName = request.FirstName;
            updated.LastName = request.LastName;
            updated.FullName = request.FirstName + " " + request.LastName;
            updated.UserGroupId = request.UserGroupId;

            updated.StaffRole = request.StaffRole;
            updated.StaffRoleFreeText = request.StaffRoleFreeText;

            updated.UniversityJobTitle = request.UniversityJobTitle;
            updated.DisciplineIds = request.DisciplineIds;
            updated.DisciplineFreeText = request.DisciplineFreeText;
            updated.SiteId = request.SiteId;

            updated.ProfessionalBackgroundId = request.ProfessionalBackgroundId;
            updated.ProfessionalBackgroundFreeText = request.ProfessionalBackgroundFreeText;

            updated.ProfessionalBackgroundDetailId = request.ProfessionalBackgroundDetailId;
            updated.ProfessionalBackgroundDetailFreeText = request.ProfessionalBackgroundDetailFreeText;

            updated.StartDateForRSS= request.StartDateForRSS;
            updated.EndDateForRSS = request.EndDateForRSS;
            updated.FullTimeEquivalentForRSS = request.FullTimeEquivalentForRSS;

            updated.Hidden = request.Hidden;
            updated.Disabled = request.Disabled;

            return await _userRepository.UpdateUser(original: original, updated: updated);
        }
    }
}
