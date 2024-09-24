using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class ProjectSupport
    {
        public ProjectSupport() { }
        
        public ProjectSupport(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(ProjectSupport.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(ProjectSupport.Date):
                        Date = DateOnly.FromDateTime((DateTime)value!);
                        break;
                    case nameof(ProjectSupport.Description):
                        Description = (string)value!;
                        break;
                    case nameof(ProjectSupport.WorkTimeInHours):
                        WorkTimeInHours = (Decimal)value!;
                        break;
                    case nameof(ProjectSupport.TeamIds):
                        TeamIdsJson = (string)value!;
                        break;
                    case nameof(ProjectSupport.AdviserIds):
                        AdviserIdsJson = (string)value!;
                        break;
                    case nameof(ProjectSupport.SupportProvidedIds):
                        SupportProvidedIdsJson = (string)value!;
                        break;
                    case nameof(ProjectSupport.SupportRequestedAt):
                        SupportRequestedAt = value == DBNull.Value ? (DateOnly?)null : DateOnly.FromDateTime((DateTime)value!);
                        break;
                    case nameof(ProjectSupport.SupportRequestedCompletedAt):
                        SupportRequestedCompletedAt = value == DBNull.Value ? (DateTimeOffset?)null : (DateTimeOffset?)value;
                        break;
                    case nameof(ProjectSupport.SupportRequestedCompletedNotes):
                        SupportRequestedCompletedNotes = (string)value!;
                        break;
                    case nameof(ProjectSupport.Files):
                        FilesJson = (string)value!;
                        break;
                    case nameof(ProjectSupport.CalculatedMinutes):
                        CalculatedMinutes = (int)value!;
                        break;
                    case nameof(ProjectSupport.ProjectId):
                        ProjectId = (Guid)value!;
                        break;
                    case nameof(ProjectSupport.IsPrePostAwardId):
                        IsPrePostAwardId = (Guid)value!;
                        break;
                    case nameof(ProjectSupport.SupportRequestedTeamId):
                        SupportRequestedTeamId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(ProjectSupport.SupportRequestedCompletedByUserId):
                        SupportRequestedCompletedByUserId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public DateOnly Date  { get; set; } = new DateOnly(1900, 1, 1);
        public string Description  { get; set; } = String.Empty;
        public Decimal WorkTimeInHours  { get; set; } = 0M;
        public List<Guid> TeamIds
        {
            get { return _TeamIds ?? (_TeamIds = SafeJsonSerializer.Deserialize<List<Guid>>(_TeamIdsJson)); }
            set { _TeamIds = value; }
        }
        List<Guid>? _TeamIds = null;
        internal string TeamIdsJson
        {
            get { var result = _TeamIds == null ? _TeamIdsJson : (_TeamIdsJson = SafeJsonSerializer.Serialize(_TeamIds)); _TeamIds = null; return result; }
            set { _TeamIdsJson = value; _TeamIds = null; }
        }
        string _TeamIdsJson = String.Empty;
        public List<Guid> AdviserIds
        {
            get { return _AdviserIds ?? (_AdviserIds = SafeJsonSerializer.Deserialize<List<Guid>>(_AdviserIdsJson)); }
            set { _AdviserIds = value; }
        }
        List<Guid>? _AdviserIds = null;
        internal string AdviserIdsJson
        {
            get { var result = _AdviserIds == null ? _AdviserIdsJson : (_AdviserIdsJson = SafeJsonSerializer.Serialize(_AdviserIds)); _AdviserIds = null; return result; }
            set { _AdviserIdsJson = value; _AdviserIds = null; }
        }
        string _AdviserIdsJson = String.Empty;
        public List<Guid> SupportProvidedIds
        {
            get { return _SupportProvidedIds ?? (_SupportProvidedIds = SafeJsonSerializer.Deserialize<List<Guid>>(_SupportProvidedIdsJson)); }
            set { _SupportProvidedIds = value; }
        }
        List<Guid>? _SupportProvidedIds = null;
        internal string SupportProvidedIdsJson
        {
            get { var result = _SupportProvidedIds == null ? _SupportProvidedIdsJson : (_SupportProvidedIdsJson = SafeJsonSerializer.Serialize(_SupportProvidedIds)); _SupportProvidedIds = null; return result; }
            set { _SupportProvidedIdsJson = value; _SupportProvidedIds = null; }
        }
        string _SupportProvidedIdsJson = String.Empty;
        public DateOnly? SupportRequestedAt  { get; set; } = null;
        public DateTimeOffset? SupportRequestedCompletedAt  { get; set; } = null;
        public string SupportRequestedCompletedNotes  { get; set; } = String.Empty;
        public List<ProjectSupportFile> Files
        {
            get { return _Files ?? (_Files = SafeJsonSerializer.Deserialize<List<ProjectSupportFile>>(_FilesJson)); }
            set { _Files = value; }
        }
        List<ProjectSupportFile>? _Files = null;
        internal string FilesJson
        {
            get { var result = _Files == null ? _FilesJson : (_FilesJson = SafeJsonSerializer.Serialize(_Files)); _Files = null; return result; }
            set { _FilesJson = value; _Files = null; }
        }
        string _FilesJson = String.Empty;
        public int CalculatedMinutes  { get; set; } = 0;
        public Guid ProjectId { get; set; }
        public Guid IsPrePostAwardId { get; set; } = Guid.Parse("d8c2fe26-3a35-4a49-b61d-b5abc41611f6");
        public Guid? SupportRequestedTeamId { get; set; }
        public Guid? SupportRequestedCompletedByUserId { get; set; }
        public ProjectSupport Copy()
        {
            var entity = new ProjectSupport();
            entity.Id = Id;
            entity.Date = Date;
            entity.Description = Description;
            entity.WorkTimeInHours = WorkTimeInHours;
            entity.TeamIdsJson = TeamIdsJson;
            entity.AdviserIdsJson = AdviserIdsJson;
            entity.SupportProvidedIdsJson = SupportProvidedIdsJson;
            entity.SupportRequestedAt = SupportRequestedAt;
            entity.SupportRequestedCompletedAt = SupportRequestedCompletedAt;
            entity.SupportRequestedCompletedNotes = SupportRequestedCompletedNotes;
            entity.FilesJson = FilesJson;
            entity.CalculatedMinutes = CalculatedMinutes;
            entity.ProjectId = ProjectId;
            entity.IsPrePostAwardId = IsPrePostAwardId;
            entity.SupportRequestedTeamId = SupportRequestedTeamId;
            entity.SupportRequestedCompletedByUserId = SupportRequestedCompletedByUserId;
            return entity;
        }
    }
}

