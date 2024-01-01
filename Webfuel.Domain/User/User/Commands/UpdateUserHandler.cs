using MediatR;

namespace Webfuel.Domain
{
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

            updated.RSSJobTitle = request.RSSJobTitle;
            updated.UniversityJobTitle = request.UniversityJobTitle;
            updated.ProfessionalBackground = request.ProfessionalBackground;
            updated.Specialisation = request.Specialisation;
            updated.DisciplineIds = request.DisciplineIds;
            updated.DisciplineFreeText = request.DisciplineFreeText;
            updated.SiteId = request.SiteId;

            updated.StartDateForRSS= request.StartDateForRSS;
            updated.EndDateForRSS = request.EndDateForRSS;
            updated.FullTimeEquivalentForRSS = request.FullTimeEquivalentForRSS;

            updated.Hidden = request.Hidden;
            updated.Disabled = request.Disabled;

            return await _userRepository.UpdateUser(original: original, updated: updated);
        }
    }
}
