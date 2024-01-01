using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class User
    {
        public User() { }
        
        public User(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(User.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(User.Email):
                        Email = (string)value!;
                        break;
                    case nameof(User.Developer):
                        Developer = (bool)value!;
                        break;
                    case nameof(User.Title):
                        Title = (string)value!;
                        break;
                    case nameof(User.FirstName):
                        FirstName = (string)value!;
                        break;
                    case nameof(User.LastName):
                        LastName = (string)value!;
                        break;
                    case nameof(User.FullName):
                        FullName = (string)value!;
                        break;
                    case nameof(User.RSSJobTitle):
                        RSSJobTitle = (string)value!;
                        break;
                    case nameof(User.UniversityJobTitle):
                        UniversityJobTitle = (string)value!;
                        break;
                    case nameof(User.ProfessionalBackground):
                        ProfessionalBackground = (string)value!;
                        break;
                    case nameof(User.Specialisation):
                        Specialisation = (string)value!;
                        break;
                    case nameof(User.DisciplineIds):
                        DisciplineIdsJson = (string)value!;
                        break;
                    case nameof(User.DisciplineFreeText):
                        DisciplineFreeText = (string)value!;
                        break;
                    case nameof(User.StartDateForRSS):
                        StartDateForRSS = value == DBNull.Value ? (DateOnly?)null : DateOnly.FromDateTime((DateTime)value!);
                        break;
                    case nameof(User.EndDateForRSS):
                        EndDateForRSS = value == DBNull.Value ? (DateOnly?)null : DateOnly.FromDateTime((DateTime)value!);
                        break;
                    case nameof(User.FullTimeEquivalentForRSS):
                        FullTimeEquivalentForRSS = value == DBNull.Value ? (Decimal?)null : (Decimal?)value;
                        break;
                    case nameof(User.Hidden):
                        Hidden = (bool)value!;
                        break;
                    case nameof(User.Disabled):
                        Disabled = (bool)value!;
                        break;
                    case nameof(User.LastLoginAt):
                        LastLoginAt = value == DBNull.Value ? (DateTimeOffset?)null : (DateTimeOffset?)value;
                        break;
                    case nameof(User.PasswordHash):
                        PasswordHash = (string)value!;
                        break;
                    case nameof(User.PasswordSalt):
                        PasswordSalt = (string)value!;
                        break;
                    case nameof(User.PasswordResetAt):
                        PasswordResetAt = (DateTimeOffset)value!;
                        break;
                    case nameof(User.PasswordResetToken):
                        PasswordResetToken = (Guid)value!;
                        break;
                    case nameof(User.PasswordResetValidUntil):
                        PasswordResetValidUntil = (DateTimeOffset)value!;
                        break;
                    case nameof(User.CreatedAt):
                        CreatedAt = (DateTimeOffset)value!;
                        break;
                    case nameof(User.SiteId):
                        SiteId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(User.UserGroupId):
                        UserGroupId = (Guid)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public string Email  { get; set; } = String.Empty;
        public bool Developer  { get; set; } = false;
        public string Title  { get; set; } = String.Empty;
        public string FirstName  { get; set; } = String.Empty;
        public string LastName  { get; set; } = String.Empty;
        public string FullName  { get; set; } = String.Empty;
        public string RSSJobTitle  { get; set; } = String.Empty;
        public string UniversityJobTitle  { get; set; } = String.Empty;
        public string ProfessionalBackground  { get; set; } = String.Empty;
        public string Specialisation  { get; set; } = String.Empty;
        public List<Guid> DisciplineIds
        {
            get { return _DisciplineIds ?? (_DisciplineIds = SafeJsonSerializer.Deserialize<List<Guid>>(_DisciplineIdsJson)); }
            set { _DisciplineIds = value; }
        }
        List<Guid>? _DisciplineIds = null;
        internal string DisciplineIdsJson
        {
            get { var result = _DisciplineIds == null ? _DisciplineIdsJson : (_DisciplineIdsJson = SafeJsonSerializer.Serialize(_DisciplineIds)); _DisciplineIds = null; return result; }
            set { _DisciplineIdsJson = value; _DisciplineIds = null; }
        }
        string _DisciplineIdsJson = String.Empty;
        public string DisciplineFreeText  { get; set; } = String.Empty;
        public DateOnly? StartDateForRSS  { get; set; } = null;
        public DateOnly? EndDateForRSS  { get; set; } = null;
        public Decimal? FullTimeEquivalentForRSS  { get; set; } = null;
        public bool Hidden  { get; set; } = false;
        public bool Disabled  { get; set; } = false;
        public DateTimeOffset? LastLoginAt  { get; set; } = null;
        [JsonIgnore]
        public string PasswordHash  { get; set; } = String.Empty;
        [JsonIgnore]
        public string PasswordSalt  { get; set; } = String.Empty;
        [JsonIgnore]
        public DateTimeOffset PasswordResetAt  { get; set; } = new DateTimeOffset(599266080000000000L, TimeSpan.Zero);
        [JsonIgnore]
        public Guid PasswordResetToken  { get; set; } = Guid.Empty;
        [JsonIgnore]
        public DateTimeOffset PasswordResetValidUntil  { get; set; } = new DateTimeOffset(599266080000000000L, TimeSpan.Zero);
        public DateTimeOffset CreatedAt  { get; set; } = new DateTimeOffset(599266080000000000L, TimeSpan.Zero);
        public Guid? SiteId { get; set; }
        public Guid UserGroupId { get; set; }
        public User Copy()
        {
            var entity = new User();
            entity.Id = Id;
            entity.Email = Email;
            entity.Developer = Developer;
            entity.Title = Title;
            entity.FirstName = FirstName;
            entity.LastName = LastName;
            entity.FullName = FullName;
            entity.RSSJobTitle = RSSJobTitle;
            entity.UniversityJobTitle = UniversityJobTitle;
            entity.ProfessionalBackground = ProfessionalBackground;
            entity.Specialisation = Specialisation;
            entity.DisciplineIdsJson = DisciplineIdsJson;
            entity.DisciplineFreeText = DisciplineFreeText;
            entity.StartDateForRSS = StartDateForRSS;
            entity.EndDateForRSS = EndDateForRSS;
            entity.FullTimeEquivalentForRSS = FullTimeEquivalentForRSS;
            entity.Hidden = Hidden;
            entity.Disabled = Disabled;
            entity.LastLoginAt = LastLoginAt;
            entity.PasswordHash = PasswordHash;
            entity.PasswordSalt = PasswordSalt;
            entity.PasswordResetAt = PasswordResetAt;
            entity.PasswordResetToken = PasswordResetToken;
            entity.PasswordResetValidUntil = PasswordResetValidUntil;
            entity.CreatedAt = CreatedAt;
            entity.SiteId = SiteId;
            entity.UserGroupId = UserGroupId;
            return entity;
        }
    }
}

