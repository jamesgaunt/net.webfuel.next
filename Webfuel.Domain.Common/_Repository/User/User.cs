using FluentValidation;

namespace Webfuel.Domain.Common
{
    public partial class User
    {
        public Guid Id  { get; set; } = Guid.Empty;
        public string Email  { get; set; } = String.Empty;
        public string FirstName  { get; set; } = String.Empty;
        public string LastName  { get; set; } = String.Empty;
        public bool IsDeveloper  { get; set; } = false;
        public User Copy()
        {
            var entity = new User();
            entity.Id = Id;
            entity.Email = Email;
            entity.FirstName = FirstName;
            entity.LastName = LastName;
            entity.IsDeveloper = IsDeveloper;
            return entity;
        }
    }
}

