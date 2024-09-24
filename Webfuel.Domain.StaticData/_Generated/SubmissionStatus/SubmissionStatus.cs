using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class SubmissionStatus: IStaticData
    {
        public SubmissionStatus() { }
        
        public SubmissionStatus(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(SubmissionStatus.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(SubmissionStatus.Name):
                        Name = (string)value!;
                        break;
                    case nameof(SubmissionStatus.SortOrder):
                        SortOrder = (int)value!;
                        break;
                    case nameof(SubmissionStatus.Default):
                        Default = (bool)value!;
                        break;
                    case nameof(SubmissionStatus.Hidden):
                        Hidden = (bool)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; internal set; } = Guid.Empty;
        public string Name  { get; internal set; } = String.Empty;
        public int SortOrder  { get; internal set; } = 0;
        public bool Default  { get; internal set; } = false;
        public bool Hidden  { get; internal set; } = false;
        public SubmissionStatus Copy()
        {
            var entity = new SubmissionStatus();
            entity.Id = Id;
            entity.Name = Name;
            entity.SortOrder = SortOrder;
            entity.Default = Default;
            entity.Hidden = Hidden;
            return entity;
        }
    }
}

