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

        // Properties

        public ReportFilterGroupCondition Condition { get; set; } = ReportFilterGroupCondition.All;

        public List<ReportFilter> Filters { get; set; } = new List<ReportFilter>();

        // Serialization

        public override void ReadProperties(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {

            while (reader.Read())
            {
                if (reader.TokenType != JsonTokenType.PropertyName)
                    break;

                var propertyName = reader.GetString();
                reader.Read();

                switch (propertyName)
                {
                    case "condition":
                        Condition = (ReportFilterGroupCondition)reader.GetInt32();
                        break;

                    case "filters":

                        if (reader.TokenType != JsonTokenType.StartArray)
                            throw new JsonException();
                        reader.Read();

                        while (reader.TokenType != JsonTokenType.EndArray)
                        {
                            var filter = JsonSerializer.Deserialize<ReportFilter>(ref reader, options);
                            if (filter != null)
                                Filters.Add(filter);
                        }
                        break;

                    default:
                        reader.Skip();
                        break;
                }
            }
        }

        public override void WriteProperties(Utf8JsonWriter writer, JsonSerializerOptions options)
        {
            writer.WriteNumber("condition", (int)Condition);

            writer.WriteStartArray("filters");
            {
                foreach (var filter in Filters)
                {
                    JsonSerializer.Serialize(writer, filter, options);
                }
            }
            writer.WriteEndArray();
        }
    }
}
