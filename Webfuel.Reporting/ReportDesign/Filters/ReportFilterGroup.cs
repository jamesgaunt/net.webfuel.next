using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public override ReportPrimativeType PrimativeType => ReportPrimativeType.None;

        public ReportFilterGroupCondition Condition { get; set; } = ReportFilterGroupCondition.All;

        public List<ReportFilter> Filters { get; set; } = new List<ReportFilter>();

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

        // Validation

        public override void ValidateFilter(ReportSchema schema)
        {
            if (!Enum.IsDefined(Condition))
                Condition = ReportFilterGroupCondition.All;

            foreach (var filter in Filters)
                filter.ValidateFilter(schema);

            base.ValidateFilter(schema);
        }

        // Description

        public override string GenerateDescription(ReportSchema schema)
        {
            return $"{GetConditionDescription()} of these conditions are true...";
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
    }
}
