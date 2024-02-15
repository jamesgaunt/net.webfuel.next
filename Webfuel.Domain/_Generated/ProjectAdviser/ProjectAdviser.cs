using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class ProjectAdviser
    {
        public ProjectAdviser() { }
        
        public ProjectAdviser(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(ProjectAdviser.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(ProjectAdviser.SortOrder):
                        SortOrder = (int)value!;
                        break;
                    case nameof(ProjectAdviser.ProjectId):
                        ProjectId = (Guid)value!;
                        break;
                    case nameof(ProjectAdviser.UserId):
                        UserId = (Guid)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public int SortOrder  { get; set; } = 0;
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }
        public ProjectAdviser Copy()
        {
            var entity = new ProjectAdviser();
            entity.Id = Id;
            entity.SortOrder = SortOrder;
            entity.ProjectId = ProjectId;
            entity.UserId = UserId;
            return entity;
        }
    }
}

