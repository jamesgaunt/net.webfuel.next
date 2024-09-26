using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class ProfessionalBackgroundDetail: IStaticData
    {
        public ProfessionalBackgroundDetail() { }
        
        public ProfessionalBackgroundDetail(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(ProfessionalBackgroundDetail.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(ProfessionalBackgroundDetail.Name):
                        Name = (string)value!;
                        break;
                    case nameof(ProfessionalBackgroundDetail.SortOrder):
                        SortOrder = (int)value!;
                        break;
                    case nameof(ProfessionalBackgroundDetail.Default):
                        Default = (bool)value!;
                        break;
                    case nameof(ProfessionalBackgroundDetail.Hidden):
                        Hidden = (bool)value!;
                        break;
                    case nameof(ProfessionalBackgroundDetail.FreeText):
                        FreeText = (bool)value!;
                        break;
                    case nameof(ProfessionalBackgroundDetail.ProfessionalBackgroundId):
                        ProfessionalBackgroundId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                }
            }
        }
        public Guid Id  { get; internal set; } = Guid.Empty;
        public string Name  { get; internal set; } = String.Empty;
        public int SortOrder  { get; internal set; } = 0;
        public bool Default  { get; internal set; } = false;
        public bool Hidden  { get; internal set; } = false;
        public bool FreeText  { get; internal set; } = false;
        public Guid? ProfessionalBackgroundId { get; internal set; }
        public ProfessionalBackgroundDetail Copy()
        {
            var entity = new ProfessionalBackgroundDetail();
            entity.Id = Id;
            entity.Name = Name;
            entity.SortOrder = SortOrder;
            entity.Default = Default;
            entity.Hidden = Hidden;
            entity.FreeText = FreeText;
            entity.ProfessionalBackgroundId = ProfessionalBackgroundId;
            return entity;
        }
    }
}

