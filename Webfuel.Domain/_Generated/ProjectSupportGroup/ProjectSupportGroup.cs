using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class ProjectSupportGroup
    {
        public ProjectSupportGroup() { }
        
        public ProjectSupportGroup(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(ProjectSupportGroup.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(ProjectSupportGroup.CreatedAt):
                        CreatedAt = (DateTimeOffset)value!;
                        break;
                    case nameof(ProjectSupportGroup.FileStorageGroupId):
                        FileStorageGroupId = (Guid)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public DateTimeOffset CreatedAt  { get; set; } = new DateTimeOffset(599266080000000000L, TimeSpan.Zero);
        public Guid FileStorageGroupId { get; set; }
        public ProjectSupportGroup Copy()
        {
            var entity = new ProjectSupportGroup();
            entity.Id = Id;
            entity.CreatedAt = CreatedAt;
            entity.FileStorageGroupId = FileStorageGroupId;
            return entity;
        }
    }
}

