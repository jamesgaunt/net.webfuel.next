using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class UserMetadata: IRepositoryMetadata<User>
    {
        // Data Access
        
        public static string DatabaseTable => "User";
        
        public static string DefaultOrderBy => "ORDER BY LastName ASC";
        
        public static User DataReader(SqlDataReader dr) => new User(dr);
        
        public static List<SqlParameter> ExtractParameters(User entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(User.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(User.Id):
                        break;
                    case nameof(User.Email):
                        result.Add(new SqlParameter(nameof(User.Email), entity.Email));
                        break;
                    case nameof(User.Developer):
                        result.Add(new SqlParameter(nameof(User.Developer), entity.Developer));
                        break;
                    case nameof(User.Title):
                        result.Add(new SqlParameter(nameof(User.Title), entity.Title));
                        break;
                    case nameof(User.FirstName):
                        result.Add(new SqlParameter(nameof(User.FirstName), entity.FirstName));
                        break;
                    case nameof(User.LastName):
                        result.Add(new SqlParameter(nameof(User.LastName), entity.LastName));
                        break;
                    case nameof(User.FullName):
                        result.Add(new SqlParameter(nameof(User.FullName), entity.FullName));
                        break;
                    case nameof(User.StaffRoleFreeText):
                        result.Add(new SqlParameter(nameof(User.StaffRoleFreeText), entity.StaffRoleFreeText));
                        break;
                    case nameof(User.UniversityJobTitle):
                        result.Add(new SqlParameter(nameof(User.UniversityJobTitle), entity.UniversityJobTitle));
                        break;
                    case nameof(User.ProfessionalBackgroundFreeText):
                        result.Add(new SqlParameter(nameof(User.ProfessionalBackgroundFreeText), entity.ProfessionalBackgroundFreeText));
                        break;
                    case nameof(User.ProfessionalBackgroundDetailFreeText):
                        result.Add(new SqlParameter(nameof(User.ProfessionalBackgroundDetailFreeText), entity.ProfessionalBackgroundDetailFreeText));
                        break;
                    case nameof(User.DisciplineIds):
                        result.Add(new SqlParameter(nameof(User.DisciplineIds), entity.DisciplineIdsJson));
                        break;
                    case nameof(User.DisciplineFreeText):
                        result.Add(new SqlParameter(nameof(User.DisciplineFreeText), entity.DisciplineFreeText));
                        break;
                    case nameof(User.StartDateForRSS):
                        result.Add(new SqlParameter(nameof(User.StartDateForRSS), entity.StartDateForRSS ?? (object?)DBNull.Value));
                        break;
                    case nameof(User.EndDateForRSS):
                        result.Add(new SqlParameter(nameof(User.EndDateForRSS), entity.EndDateForRSS ?? (object?)DBNull.Value));
                        break;
                    case nameof(User.FullTimeEquivalentForRSS):
                        result.Add(new SqlParameter(nameof(User.FullTimeEquivalentForRSS), entity.FullTimeEquivalentForRSS ?? (object?)DBNull.Value));
                        break;
                    case nameof(User.Hidden):
                        result.Add(new SqlParameter(nameof(User.Hidden), entity.Hidden));
                        break;
                    case nameof(User.Disabled):
                        result.Add(new SqlParameter(nameof(User.Disabled), entity.Disabled));
                        break;
                    case nameof(User.LastLoginAt):
                        result.Add(new SqlParameter(nameof(User.LastLoginAt), entity.LastLoginAt ?? (object?)DBNull.Value));
                        break;
                    case nameof(User.PasswordHash):
                        result.Add(new SqlParameter(nameof(User.PasswordHash), entity.PasswordHash));
                        break;
                    case nameof(User.PasswordSalt):
                        result.Add(new SqlParameter(nameof(User.PasswordSalt), entity.PasswordSalt));
                        break;
                    case nameof(User.PasswordResetAt):
                        result.Add(new SqlParameter(nameof(User.PasswordResetAt), entity.PasswordResetAt));
                        break;
                    case nameof(User.PasswordResetToken):
                        result.Add(new SqlParameter(nameof(User.PasswordResetToken), entity.PasswordResetToken));
                        break;
                    case nameof(User.PasswordResetValidUntil):
                        result.Add(new SqlParameter(nameof(User.PasswordResetValidUntil), entity.PasswordResetValidUntil));
                        break;
                    case nameof(User.CreatedAt):
                        result.Add(new SqlParameter(nameof(User.CreatedAt), entity.CreatedAt));
                        break;
                    case nameof(User.SiteId):
                        result.Add(new SqlParameter(nameof(User.SiteId), entity.SiteId ?? (object?)DBNull.Value));
                        break;
                    case nameof(User.StaffRoleId):
                        result.Add(new SqlParameter(nameof(User.StaffRoleId), entity.StaffRoleId ?? (object?)DBNull.Value));
                        break;
                    case nameof(User.ProfessionalBackgroundId):
                        result.Add(new SqlParameter(nameof(User.ProfessionalBackgroundId), entity.ProfessionalBackgroundId ?? (object?)DBNull.Value));
                        break;
                    case nameof(User.ProfessionalBackgroundDetailId):
                        result.Add(new SqlParameter(nameof(User.ProfessionalBackgroundDetailId), entity.ProfessionalBackgroundDetailId ?? (object?)DBNull.Value));
                        break;
                    case nameof(User.UserGroupId):
                        result.Add(new SqlParameter(nameof(User.UserGroupId), entity.UserGroupId));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<User, UserMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<User, UserMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<User, UserMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "Email";
                yield return "Developer";
                yield return "Title";
                yield return "FirstName";
                yield return "LastName";
                yield return "FullName";
                yield return "StaffRoleFreeText";
                yield return "UniversityJobTitle";
                yield return "ProfessionalBackgroundFreeText";
                yield return "ProfessionalBackgroundDetailFreeText";
                yield return "DisciplineIds";
                yield return "DisciplineFreeText";
                yield return "StartDateForRSS";
                yield return "EndDateForRSS";
                yield return "FullTimeEquivalentForRSS";
                yield return "Hidden";
                yield return "Disabled";
                yield return "LastLoginAt";
                yield return "PasswordHash";
                yield return "PasswordSalt";
                yield return "PasswordResetAt";
                yield return "PasswordResetToken";
                yield return "PasswordResetValidUntil";
                yield return "CreatedAt";
                yield return "SiteId";
                yield return "StaffRoleId";
                yield return "ProfessionalBackgroundId";
                yield return "ProfessionalBackgroundDetailId";
                yield return "UserGroupId";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "Email";
                yield return "Developer";
                yield return "Title";
                yield return "FirstName";
                yield return "LastName";
                yield return "FullName";
                yield return "StaffRoleFreeText";
                yield return "UniversityJobTitle";
                yield return "ProfessionalBackgroundFreeText";
                yield return "ProfessionalBackgroundDetailFreeText";
                yield return "DisciplineIds";
                yield return "DisciplineFreeText";
                yield return "StartDateForRSS";
                yield return "EndDateForRSS";
                yield return "FullTimeEquivalentForRSS";
                yield return "Hidden";
                yield return "Disabled";
                yield return "LastLoginAt";
                yield return "PasswordHash";
                yield return "PasswordSalt";
                yield return "PasswordResetAt";
                yield return "PasswordResetToken";
                yield return "PasswordResetValidUntil";
                yield return "CreatedAt";
                yield return "SiteId";
                yield return "StaffRoleId";
                yield return "ProfessionalBackgroundId";
                yield return "ProfessionalBackgroundDetailId";
                yield return "UserGroupId";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Email";
                yield return "Developer";
                yield return "Title";
                yield return "FirstName";
                yield return "LastName";
                yield return "FullName";
                yield return "StaffRoleFreeText";
                yield return "UniversityJobTitle";
                yield return "ProfessionalBackgroundFreeText";
                yield return "ProfessionalBackgroundDetailFreeText";
                yield return "DisciplineIds";
                yield return "DisciplineFreeText";
                yield return "StartDateForRSS";
                yield return "EndDateForRSS";
                yield return "FullTimeEquivalentForRSS";
                yield return "Hidden";
                yield return "Disabled";
                yield return "LastLoginAt";
                yield return "PasswordHash";
                yield return "PasswordSalt";
                yield return "PasswordResetAt";
                yield return "PasswordResetToken";
                yield return "PasswordResetValidUntil";
                yield return "CreatedAt";
                yield return "SiteId";
                yield return "StaffRoleId";
                yield return "ProfessionalBackgroundId";
                yield return "ProfessionalBackgroundDetailId";
                yield return "UserGroupId";
            }
        }
        
        // Validation
        
        public static void Validate(User entity)
        {
            entity.Email = entity.Email ?? String.Empty;
            entity.Email = entity.Email.Trim();
            entity.Title = entity.Title ?? String.Empty;
            entity.Title = entity.Title.Trim();
            entity.FirstName = entity.FirstName ?? String.Empty;
            entity.FirstName = entity.FirstName.Trim();
            entity.LastName = entity.LastName ?? String.Empty;
            entity.LastName = entity.LastName.Trim();
            entity.FullName = entity.FullName ?? String.Empty;
            entity.FullName = entity.FullName.Trim();
            entity.StaffRoleFreeText = entity.StaffRoleFreeText ?? String.Empty;
            entity.StaffRoleFreeText = entity.StaffRoleFreeText.Trim();
            entity.UniversityJobTitle = entity.UniversityJobTitle ?? String.Empty;
            entity.UniversityJobTitle = entity.UniversityJobTitle.Trim();
            entity.ProfessionalBackgroundFreeText = entity.ProfessionalBackgroundFreeText ?? String.Empty;
            entity.ProfessionalBackgroundFreeText = entity.ProfessionalBackgroundFreeText.Trim();
            entity.ProfessionalBackgroundDetailFreeText = entity.ProfessionalBackgroundDetailFreeText ?? String.Empty;
            entity.ProfessionalBackgroundDetailFreeText = entity.ProfessionalBackgroundDetailFreeText.Trim();
            entity.DisciplineFreeText = entity.DisciplineFreeText ?? String.Empty;
            entity.DisciplineFreeText = entity.DisciplineFreeText.Trim();
            entity.PasswordHash = entity.PasswordHash ?? String.Empty;
            entity.PasswordHash = entity.PasswordHash.Trim();
            entity.PasswordSalt = entity.PasswordSalt ?? String.Empty;
            entity.PasswordSalt = entity.PasswordSalt.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static UserRepositoryValidator Validator { get; } = new UserRepositoryValidator();
        
        public const int Email_MaxLength = 64;
        public const int Title_MaxLength = 16;
        public const int FirstName_MaxLength = 128;
        public const int LastName_MaxLength = 128;
        public const int FullName_MaxLength = 128;
        public const int StaffRoleFreeText_MaxLength = 128;
        public const int UniversityJobTitle_MaxLength = 128;
        public const int ProfessionalBackgroundFreeText_MaxLength = 1024;
        public const int ProfessionalBackgroundDetailFreeText_MaxLength = 1024;
        public const int DisciplineFreeText_MaxLength = 1024;
        public const int PasswordHash_MaxLength = 256;
        public const int PasswordSalt_MaxLength = 256;
        
        public static void Email_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Email_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void Title_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Title_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void FirstName_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(FirstName_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void LastName_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(LastName_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void FullName_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(FullName_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void StaffRoleFreeText_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(StaffRoleFreeText_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void UniversityJobTitle_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(UniversityJobTitle_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void ProfessionalBackgroundFreeText_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(ProfessionalBackgroundFreeText_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void ProfessionalBackgroundDetailFreeText_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(ProfessionalBackgroundDetailFreeText_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void DisciplineFreeText_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(DisciplineFreeText_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void PasswordHash_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(PasswordHash_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void PasswordSalt_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(PasswordSalt_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class UserRepositoryValidator: AbstractValidator<User>
    {
        public UserRepositoryValidator()
        {
            RuleFor(x => x.Email).Use(UserMetadata.Email_ValidationRules);
            RuleFor(x => x.Title).Use(UserMetadata.Title_ValidationRules);
            RuleFor(x => x.FirstName).Use(UserMetadata.FirstName_ValidationRules);
            RuleFor(x => x.LastName).Use(UserMetadata.LastName_ValidationRules);
            RuleFor(x => x.FullName).Use(UserMetadata.FullName_ValidationRules);
            RuleFor(x => x.StaffRoleFreeText).Use(UserMetadata.StaffRoleFreeText_ValidationRules);
            RuleFor(x => x.UniversityJobTitle).Use(UserMetadata.UniversityJobTitle_ValidationRules);
            RuleFor(x => x.ProfessionalBackgroundFreeText).Use(UserMetadata.ProfessionalBackgroundFreeText_ValidationRules);
            RuleFor(x => x.ProfessionalBackgroundDetailFreeText).Use(UserMetadata.ProfessionalBackgroundDetailFreeText_ValidationRules);
            RuleFor(x => x.DisciplineFreeText).Use(UserMetadata.DisciplineFreeText_ValidationRules);
            RuleFor(x => x.PasswordHash).Use(UserMetadata.PasswordHash_ValidationRules);
            RuleFor(x => x.PasswordSalt).Use(UserMetadata.PasswordSalt_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

