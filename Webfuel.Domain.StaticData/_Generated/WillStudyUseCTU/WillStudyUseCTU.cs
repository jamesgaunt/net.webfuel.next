using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class WillStudyUseCTU: IStaticData
    {
        public WillStudyUseCTU() { }
        
        public WillStudyUseCTU(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(WillStudyUseCTU.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(WillStudyUseCTU.Name):
                        Name = (string)value!;
                        break;
                    case nameof(WillStudyUseCTU.SortOrder):
                        SortOrder = (int)value!;
                        break;
                    case nameof(WillStudyUseCTU.Default):
                        Default = (bool)value!;
                        break;
                    case nameof(WillStudyUseCTU.FreeText):
                        FreeText = (bool)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; internal set; } = Guid.Empty;
        public string Name  { get; internal set; } = String.Empty;
        public int SortOrder  { get; internal set; } = 0;
        public bool Default  { get; internal set; } = false;
        public bool FreeText  { get; internal set; } = false;
        public WillStudyUseCTU Copy()
        {
            var entity = new WillStudyUseCTU();
            entity.Id = Id;
            entity.Name = Name;
            entity.SortOrder = SortOrder;
            entity.Default = Default;
            entity.FreeText = FreeText;
            return entity;
        }
    }
}

