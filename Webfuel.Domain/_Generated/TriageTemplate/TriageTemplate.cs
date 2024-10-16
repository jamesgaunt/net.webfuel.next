using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class TriageTemplate
    {
        public TriageTemplate() { }
        
        public TriageTemplate(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(TriageTemplate.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(TriageTemplate.Name):
                        Name = (string)value!;
                        break;
                    case nameof(TriageTemplate.SortOrder):
                        SortOrder = (int)value!;
                        break;
                    case nameof(TriageTemplate.Subject):
                        Subject = (string)value!;
                        break;
                    case nameof(TriageTemplate.HtmlTemplate):
                        HtmlTemplate = (string)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public string Name  { get; set; } = String.Empty;
        public int SortOrder  { get; set; } = 0;
        public string Subject  { get; set; } = String.Empty;
        public string HtmlTemplate  { get; set; } = String.Empty;
        public TriageTemplate Copy()
        {
            var entity = new TriageTemplate();
            entity.Id = Id;
            entity.Name = Name;
            entity.SortOrder = SortOrder;
            entity.Subject = Subject;
            entity.HtmlTemplate = HtmlTemplate;
            return entity;
        }
    }
}

