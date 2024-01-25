using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class UserActivity
    {
        public UserActivity() { }
        
        public UserActivity(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(UserActivity.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(UserActivity.Date):
                        Date = DateOnly.FromDateTime((DateTime)value!);
                        break;
                    case nameof(UserActivity.Description):
                        Description = (string)value!;
                        break;
                    case nameof(UserActivity.WorkTimeInHours):
                        WorkTimeInHours = (Decimal)value!;
                        break;
                    case nameof(UserActivity.ProjectPrefixedNumber):
                        ProjectPrefixedNumber = (string)value!;
                        break;
                    case nameof(UserActivity.ProjectSupportProvidedIds):
                        ProjectSupportProvidedIdsJson = (string)value!;
                        break;
                    case nameof(UserActivity.UserId):
                        UserId = (Guid)value!;
                        break;
                    case nameof(UserActivity.WorkActivityId):
                        WorkActivityId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(UserActivity.ProjectId):
                        ProjectId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(UserActivity.ProjectSupportId):
                        ProjectSupportId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public DateOnly Date  { get; set; } = new DateOnly(1900, 1, 1);
        public string Description  { get; set; } = String.Empty;
        public Decimal WorkTimeInHours  { get; set; } = 0M;
        public string ProjectPrefixedNumber  { get; set; } = String.Empty;
        public List<Guid> ProjectSupportProvidedIds
        {
            get { return _ProjectSupportProvidedIds ?? (_ProjectSupportProvidedIds = SafeJsonSerializer.Deserialize<List<Guid>>(_ProjectSupportProvidedIdsJson)); }
            set { _ProjectSupportProvidedIds = value; }
        }
        List<Guid>? _ProjectSupportProvidedIds = null;
        internal string ProjectSupportProvidedIdsJson
        {
            get { var result = _ProjectSupportProvidedIds == null ? _ProjectSupportProvidedIdsJson : (_ProjectSupportProvidedIdsJson = SafeJsonSerializer.Serialize(_ProjectSupportProvidedIds)); _ProjectSupportProvidedIds = null; return result; }
            set { _ProjectSupportProvidedIdsJson = value; _ProjectSupportProvidedIds = null; }
        }
        string _ProjectSupportProvidedIdsJson = String.Empty;
        public Guid UserId { get; set; }
        public Guid? WorkActivityId { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? ProjectSupportId { get; set; }
        public UserActivity Copy()
        {
            var entity = new UserActivity();
            entity.Id = Id;
            entity.Date = Date;
            entity.Description = Description;
            entity.WorkTimeInHours = WorkTimeInHours;
            entity.ProjectPrefixedNumber = ProjectPrefixedNumber;
            entity.ProjectSupportProvidedIdsJson = ProjectSupportProvidedIdsJson;
            entity.UserId = UserId;
            entity.WorkActivityId = WorkActivityId;
            entity.ProjectId = ProjectId;
            entity.ProjectSupportId = ProjectSupportId;
            return entity;
        }
    }
}

