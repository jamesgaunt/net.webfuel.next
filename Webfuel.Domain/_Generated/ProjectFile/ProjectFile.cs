using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class ProjectFile
    {
        public ProjectFile() { }
        
        public ProjectFile(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(ProjectFile.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(ProjectFile.FileGroupId):
                        FileGroupId = (Guid)value!;
                        break;
                    case nameof(ProjectFile.FileName):
                        FileName = (string)value!;
                        break;
                    case nameof(ProjectFile.SizeBytes):
                        SizeBytes = (Int64)value!;
                        break;
                    case nameof(ProjectFile.UploadedAt):
                        UploadedAt = (DateTimeOffset)value!;
                        break;
                    case nameof(ProjectFile.UploadedBy):
                        UploadedBy = (string)value!;
                        break;
                    case nameof(ProjectFile.Description):
                        Description = (string)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public Guid FileGroupId  { get; set; } = Guid.Empty;
        public string FileName  { get; set; } = String.Empty;
        public Int64 SizeBytes  { get; set; } = 0L;
        public DateTimeOffset UploadedAt  { get; set; } = new DateTimeOffset(599266080000000000L, TimeSpan.Zero);
        public string UploadedBy  { get; set; } = String.Empty;
        public string Description  { get; set; } = String.Empty;
        public ProjectFile Copy()
        {
            var entity = new ProjectFile();
            entity.Id = Id;
            entity.FileGroupId = FileGroupId;
            entity.FileName = FileName;
            entity.SizeBytes = SizeBytes;
            entity.UploadedAt = UploadedAt;
            entity.UploadedBy = UploadedBy;
            entity.Description = Description;
            return entity;
        }
    }
}

