using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class FundingStream: IStaticData
    {
        public FundingStream() { }
        
        public FundingStream(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(FundingStream.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(FundingStream.Name):
                        Name = (string)value!;
                        break;
                    case nameof(FundingStream.Code):
                        Code = (string)value!;
                        break;
                    case nameof(FundingStream.SortOrder):
                        SortOrder = (int)value!;
                        break;
                    case nameof(FundingStream.Default):
                        Default = (bool)value!;
                        break;
                    case nameof(FundingStream.Hidden):
                        Hidden = (bool)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; internal set; } = Guid.Empty;
        public string Name  { get; internal set; } = String.Empty;
        public string Code  { get; internal set; } = String.Empty;
        public int SortOrder  { get; internal set; } = 0;
        public bool Default  { get; internal set; } = false;
        public bool Hidden  { get; internal set; } = false;
        public FundingStream Copy()
        {
            var entity = new FundingStream();
            entity.Id = Id;
            entity.Name = Name;
            entity.Code = Code;
            entity.SortOrder = SortOrder;
            entity.Default = Default;
            entity.Hidden = Hidden;
            return entity;
        }
    }
}

