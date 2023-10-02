using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class UserGroupMetadata: IRepositoryMetadata<UserGroup>
    {
        // Data Access
        
        public static string DatabaseTable => "UserGroup";
        public static string DefaultOrderBy => "ORDER BY Id ASC";
        
        public static UserGroup DataReader(SqlDataReader dr) => new UserGroup(dr);
        
        public static  IEnumerable<SqlParameter> DataWriter(UserGroup entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter>();
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(UserGroup.Id):
                        result.Add(new SqlParameter(nameof(UserGroup.Id), entity.Id));
                        break;
                    case nameof(UserGroup.Name):
                        result.Add(new SqlParameter(nameof(UserGroup.Name), entity.Name));
                        break;
                }
            }
            return result;
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "Name";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "Name";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Name";
            }
        }
        
        // Validation
        
        public static void Validate(UserGroup entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static UserGroupRepositoryValidator Validator { get; } = new UserGroupRepositoryValidator();
        
        public const int Name_MaxLength = 64;
        
        public static void Name<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public class UserGroupRepositoryValidator: AbstractValidator<UserGroup>
        {
            public UserGroupRepositoryValidator()
            {
                RuleFor(x => x.Name).Use(UserGroupRepositoryValidationRules.Name);
            }
        }
    }
}

