using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class UserActivityMetadata: IRepositoryMetadata<UserActivity>
    {
        // Data Access
        
        public static string DatabaseTable => "UserActivity";
        
        public static string DefaultOrderBy => "ORDER BY Id ASC";
        
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
                    case nameof(UserActivity.UserId):
                        result.Add(new SqlParameter(nameof(UserActivity.UserId), entity.UserId));
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
                yield return "UserId";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "UserId";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "UserId";
            }
        }
        
        // Validation
        
        public static void Validate(UserActivity entity)
        {
            Validator.ValidateAndThrow(entity);
        }
        
        public static UserActivityRepositoryValidator Validator { get; } = new UserActivityRepositoryValidator();
        
        
        public class UserActivityRepositoryValidator: AbstractValidator<UserActivity>
        {
            public UserActivityRepositoryValidator()
            {
            }
        }
    }
}

