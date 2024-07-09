using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class SupportTeamUserMetadata: IRepositoryMetadata<SupportTeamUser>
    {
        // Data Access
        
        public static string DatabaseTable => "SupportTeamUser";
        
        public static string DefaultOrderBy => "ORDER BY Id ASC";
        
        public static SupportTeamUser DataReader(SqlDataReader dr) => new SupportTeamUser(dr);
        
        public static List<SqlParameter> ExtractParameters(SupportTeamUser entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(SupportTeamUser.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(SupportTeamUser.Id):
                        break;
                    case nameof(SupportTeamUser.IsTeamLead):
                        result.Add(new SqlParameter(nameof(SupportTeamUser.IsTeamLead), entity.IsTeamLead));
                        break;
                    case nameof(SupportTeamUser.UserId):
                        result.Add(new SqlParameter(nameof(SupportTeamUser.UserId), entity.UserId));
                        break;
                    case nameof(SupportTeamUser.SupportTeamId):
                        result.Add(new SqlParameter(nameof(SupportTeamUser.SupportTeamId), entity.SupportTeamId));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<SupportTeamUser, SupportTeamUserMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<SupportTeamUser, SupportTeamUserMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<SupportTeamUser, SupportTeamUserMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "IsTeamLead";
                yield return "UserId";
                yield return "SupportTeamId";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "IsTeamLead";
                yield return "UserId";
                yield return "SupportTeamId";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "IsTeamLead";
                yield return "UserId";
                yield return "SupportTeamId";
            }
        }
        
        // Validation
        
        public static void Validate(SupportTeamUser entity)
        {
            Validator.ValidateAndThrow(entity);
        }
        
        public static SupportTeamUserRepositoryValidator Validator { get; } = new SupportTeamUserRepositoryValidator();
        
    }
    
    public partial class SupportTeamUserRepositoryValidator: AbstractValidator<SupportTeamUser>
    {
        public SupportTeamUserRepositoryValidator()
        {
            Validation();
        }
        
        partial void Validation();
    }
}

