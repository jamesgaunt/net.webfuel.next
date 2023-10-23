using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class ProjectSubmission
    {
        public ProjectSubmission() { }
        
        public ProjectSubmission(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(ProjectSubmission.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(ProjectSubmission.ProjectId):
                        ProjectId = (Guid)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public Guid ProjectId { get; set; }
        public ProjectSubmission Copy()
        {
            var entity = new ProjectSubmission();
            entity.Id = Id;
            entity.ProjectId = ProjectId;
            return entity;
        }
    }
}

