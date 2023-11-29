using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class ReportProvider
    {
        public ReportProvider() { }
        
        public ReportProvider(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(ReportProvider.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(ReportProvider.Name):
                        Name = (string)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public string Name  { get; set; } = String.Empty;
        public ReportProvider Copy()
        {
            var entity = new ReportProvider();
            entity.Id = Id;
            entity.Name = Name;
            return entity;
        }
    }
}

