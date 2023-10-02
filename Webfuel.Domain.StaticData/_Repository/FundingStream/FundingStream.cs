using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class FundingStream
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
                        SortOrder = (Double)value!;
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
        public Guid Id  { get; set; } = Guid.Empty;
        public string Name  { get; set; } = String.Empty;
        public string Code  { get; set; } = String.Empty;
        public Double SortOrder  { get; set; } = 1D;
        public bool Default  { get; set; } = false;
        public bool Hidden  { get; set; } = false;
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

