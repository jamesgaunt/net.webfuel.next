using FluentValidation;
using MediatR;
using Microsoft.Identity.Client;

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

        public required string RSSJobTitle { get; set; }

        public required string UniversityJobTitle { get; set; }

        public required string ProfessionalBackground { get; set; }

        public required string Specialisation { get; set; }

        public required List<Guid> DisciplineIds { get; set; }  

        public required DateOnly? StartDateForRSS { get; set; } 

        public required DateOnly? EndDateForRSS { get; set; }

        public decimal? FullTimeEquivalentForRSS { get; set; }

        public Guid? SiteId { get; set; }

        public bool Disabled { get; set; }

        public bool Hidden { get; set; }
    }
}
