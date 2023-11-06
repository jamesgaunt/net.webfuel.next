using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class UserActivityMetadata: IRepositoryMetadata<UserActivity>
    {
        // Data Access
        
        public static string DatabaseTable => "UserActivity";
        
        public static string DefaultOrderBy => "ORDER BY Date DESC";
        
        public static UserActivity DataReader(SqlDataReader dr) => new UserActivity(dr);
        
        public static List<SqlParameter> ExtractParameters(UserActivity entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(UserActivity.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(UserActivity.Id):
                        break;
                    case nameof(UserActivity.Date):
                        result.Add(new SqlParameter(nameof(UserActivity.Date), entity.Date));
                        break;
                    case nameof(UserActivity.Description):
                        result.Add(new SqlParameter(nameof(UserActivity.Description), entity.Description));
                        break;
                    case nameof(UserActivity.UserId):
                        result.Add(new SqlParameter(nameof(UserActivity.UserId), entity.UserId));
                        break;
                    case nameof(UserActivity.WorkActivityId):
                        result.Add(new SqlParameter(nameof(UserActivity.WorkActivityId), entity.WorkActivityId));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<UserActivity, UserActivityMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<UserActivity, UserActivityMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<UserActivity, UserActivityMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "Date";
                yield return "Description";
                yield return "UserId";
                yield return "WorkActivityId";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "Date";
                yield return "Description";
                yield return "UserId";
                yield return "WorkActivityId";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Date";
                yield return "Description";
                yield return "UserId";
                yield return "WorkActivityId";
            }
        }
        
        // Validation
        
        public static void Validate(UserActivity entity)
        {
            entity.Description = entity.Description ?? String.Empty;
            entity.Description = entity.Description.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static UserActivityRepositoryValidator Validator { get; } = new UserActivityRepositoryValidator();
        
        public const int Description_MaxLength = 1024;
        
        public static void Description_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Description_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public class UserActivityRepositoryValidator: AbstractValidator<UserActivity>
        {
            public UserActivityRepositoryValidator()
            {
                RuleFor(x => x.Description).Use(Description_ValidationRules);
            }
        }
    }
}

