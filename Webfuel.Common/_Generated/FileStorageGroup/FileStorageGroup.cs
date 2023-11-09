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
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public DateTimeOffset CreatedAt  { get; set; } = new DateTimeOffset(599266080000000000L, TimeSpan.Zero);
        public FileStorageGroup Copy()
        {
            var entity = new FileStorageGroup();
            entity.Id = Id;
            entity.CreatedAt = CreatedAt;
            return entity;
        }
    }
}

