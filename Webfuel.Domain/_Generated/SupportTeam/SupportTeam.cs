using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class SupportTeam
    {
        public SupportTeam() { }
        
        public SupportTeam(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(SupportTeam.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(SupportTeam.Name):
                        Name = (string)value!;
                        break;
                    case nameof(SupportTeam.SortOrder):
                        SortOrder = (int)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public string Name  { get; set; } = String.Empty;
        public int SortOrder  { get; set; } = 0;
        public SupportTeam Copy()
        {
            var entity = new SupportTeam();
            entity.Id = Id;
            entity.Name = Name;
            entity.SortOrder = SortOrder;
            return entity;
        }
    }
}

