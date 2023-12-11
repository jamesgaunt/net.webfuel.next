using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    [ApiEnum]
    public enum ReportFilterGroupCondition
    {
        All = 10,
        Any = 20,
        None = 30,
    }

    [ApiType]
    public class ReportFilterGroup : ReportFilter
    {
        public override ReportFilterType FilterType => ReportFilterType.Group;

        public ReportFilterGroupCondition Condition { get; set; } = ReportFilterGroupCondition.All;

        public List<ReportFilter> Filters { get; set; } = new List<ReportFilter>();

        public override void ValidateFilter(ReportSchema schema)
        {
            if (!Enum.IsDefined(Condition))
                Condition = ReportFilterGroupCondition.All;

            foreach (var filter in Filters)
                filter.ValidateFilter(schema);

            DefaultName = $"{GetConditionDescription()} of these conditions are true:";
            base.ValidateFilter(schema);
        }

        string GetConditionDescription()
        {
            return Condition switch
            {
                ReportFilterGroupCondition.All => "All",
                ReportFilterGroupCondition.Any => "Any",
                ReportFilterGroupCondition.None => "None",
                _ => "All",
            };
        }

        public override async Task<bool> Apply(object context, ReportBuilder builder)
        {
            foreach(var filter in Filters)
            {
                var result = await filter.Apply(context, builder);

                if(result == true && Condition == ReportFilterGroupCondition.Any)
                    return true;

                if(result == true && Condition == ReportFilterGroupCondition.None)
                    return false;

                if (result == false && Condition == ReportFilterGroupCondition.All)
                    return false;
            }

            return true;
        }

        public override void Update(ReportFilter filter, ReportSchema schema)
        {
            if (filter is not ReportFilterGroup typed)
                throw new Exception($"Cannot apply filter of type {filter.FilterType} to filter of type {FilterType}");

            Condition = typed.Condition;
            base.Update(filter, schema);
        }

        // Serialization

        public override bool ReadProperty(string propertyName, ref Utf8JsonReader reader)
        {
            if (String.Compare(nameof(Condition), propertyName, true) == 0)
            {
                Condition = (ReportFilterGroupCondition)reader.GetInt32();
                return true;
            }

            if (String.Compare(nameof(Filters), propertyName, true) == 0)
            {
                if (reader.TokenType != JsonTokenType.StartArray)
                    throw new JsonException();
                reader.Read();

                while (reader.TokenType != JsonTokenType.EndArray)
                {
                    var filter = JsonSerializer.Deserialize<ReportFilter>(ref reader, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (filter != null)
                        Filters.Add(filter);
                }
                return true;
            }

            return base.ReadProperty(propertyName, ref reader);
        }

        public override void WriteProperties(Utf8JsonWriter writer)
        {
            base.WriteProperties(writer);
            writer.WriteNumber("condition", (int)Condition);
            writer.WriteStartArray("filters");
            {
                foreach (var filter in Filters)
                {
                    JsonSerializer.Serialize(writer, filter, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                }
            }
            writer.WriteEndArray();
        }
    }
}
