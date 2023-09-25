using FluentValidation;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class FundingStream
    {
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

