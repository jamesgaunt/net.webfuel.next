using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Common
{
    public partial class FileStorageEntry
    {
        public FileStorageEntry() { }
        
        public FileStorageEntry(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(FileStorageEntry.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(FileStorageEntry.FileName):
                        FileName = (string)value!;
                        break;
                    case nameof(FileStorageEntry.SizeBytes):
                        SizeBytes = (Int64)value!;
                        break;
                    case nameof(FileStorageEntry.UploadedAt):
                        UploadedAt = value == DBNull.Value ? (DateTimeOffset?)null : (DateTimeOffset?)value;
                        break;
                    case nameof(FileStorageEntry.Description):
                        Description = (string)value!;
                        break;
                    case nameof(FileStorageEntry.FileStorageGroupId):
                        FileStorageGroupId = (Guid)value!;
                        break;
                    case nameof(FileStorageEntry.UploadedByUserId):
                        UploadedByUserId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public string FileName  { get; set; } = String.Empty;
        public Int64 SizeBytes  { get; set; } = 0L;
        public DateTimeOffset? UploadedAt  { get; set; } = null;
        public string Description  { get; set; } = String.Empty;
        public Guid FileStorageGroupId { get; set; }
        public Guid? UploadedByUserId { get; set; }
        public FileStorageEntry Copy()
        {
            var entity = new FileStorageEntry();
            entity.Id = Id;
            entity.FileName = FileName;
            entity.SizeBytes = SizeBytes;
            entity.UploadedAt = UploadedAt;
            entity.Description = Description;
            entity.FileStorageGroupId = FileStorageGroupId;
            entity.UploadedByUserId = UploadedByUserId;
            return entity;
        }
    }
}

