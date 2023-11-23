using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class ProjectTeamSupport
    {
        public ProjectTeamSupport() { }
        
        public ProjectTeamSupport(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(ProjectTeamSupport.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(ProjectTeamSupport.ProjectPrefixedNumber):
                        ProjectPrefixedNumber = (string)value!;
                        break;
                    case nameof(ProjectTeamSupport.CreatedNotes):
                        CreatedNotes = (string)value!;
                        break;
                    case nameof(ProjectTeamSupport.CreatedAt):
                        CreatedAt = (DateTimeOffset)value!;
                        break;
                    case nameof(ProjectTeamSupport.CompletedNotes):
                        CompletedNotes = (string)value!;
                        break;
                    case nameof(ProjectTeamSupport.CompletedAt):
                        CompletedAt = value == DBNull.Value ? (DateTimeOffset?)null : (DateTimeOffset?)value;
                        break;
                    case nameof(ProjectTeamSupport.ProjectId):
                        ProjectId = (Guid)value!;
                        break;
                    case nameof(ProjectTeamSupport.SupportTeamId):
                        SupportTeamId = (Guid)value!;
                        break;
                    case nameof(ProjectTeamSupport.CreatedByUserId):
                        CreatedByUserId = (Guid)value!;
                        break;
                    case nameof(ProjectTeamSupport.CompletedByUserId):
                        CompletedByUserId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public string ProjectPrefixedNumber  { get; set; } = String.Empty;
        public string CreatedNotes  { get; set; } = String.Empty;
        public DateTimeOffset CreatedAt  { get; set; } = new DateTimeOffset(599266080000000000L, TimeSpan.Zero);
        public string CompletedNotes  { get; set; } = String.Empty;
        public DateTimeOffset? CompletedAt  { get; set; } = null;
        public Guid ProjectId { get; set; }
        public Guid SupportTeamId { get; set; }
        public Guid CreatedByUserId { get; set; }
        public Guid? CompletedByUserId { get; set; }
        public ProjectTeamSupport Copy()
        {
            var entity = new ProjectTeamSupport();
            entity.Id = Id;
            entity.ProjectPrefixedNumber = ProjectPrefixedNumber;
            entity.CreatedNotes = CreatedNotes;
            entity.CreatedAt = CreatedAt;
            entity.CompletedNotes = CompletedNotes;
            entity.CompletedAt = CompletedAt;
            entity.ProjectId = ProjectId;
            entity.SupportTeamId = SupportTeamId;
            entity.CreatedByUserId = CreatedByUserId;
            entity.CompletedByUserId = CompletedByUserId;
            return entity;
        }
    }
}

