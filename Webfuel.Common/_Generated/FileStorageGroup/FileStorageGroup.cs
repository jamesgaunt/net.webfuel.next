using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Common
{
    public partial class FileStorageGroup
    {
        public FileStorageGroup() { }
        
        public FileStorageGroup(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(FileStorageGroup.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(FileStorageGroup.CreatedAt):
                        CreatedAt = (DateTimeOffset)value!;
                        break;
                    case nameof(FileStorageGroup.FileTagIds):
                        FileTagIdsJson = (string)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public DateTimeOffset CreatedAt  { get; set; } = new DateTimeOffset(599266080000000000L, TimeSpan.Zero);
        public List<Guid> FileTagIds
        {
            get { return _FileTagIds ?? (_FileTagIds = SafeJsonSerializer.Deserialize<List<Guid>>(_FileTagIdsJson)); }
            set { _FileTagIds = value; }
        }
        List<Guid>? _FileTagIds = null;
        internal string FileTagIdsJson
        {
            get { var result = _FileTagIds == null ? _FileTagIdsJson : (_FileTagIdsJson = SafeJsonSerializer.Serialize(_FileTagIds)); _FileTagIds = null; return result; }
            set { _FileTagIdsJson = value; _FileTagIds = null; }
        }
        string _FileTagIdsJson = String.Empty;
        public FileStorageGroup Copy()
        {
            var entity = new FileStorageGroup();
            entity.Id = Id;
            entity.CreatedAt = CreatedAt;
            entity.FileTagIdsJson = FileTagIdsJson;
            return entity;
        }
    }
}

