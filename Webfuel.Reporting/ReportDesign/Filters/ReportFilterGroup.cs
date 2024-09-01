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

        public override string DisplayName => "Group";

        public override IEnumerable<ReportFilterCondition> Conditions => new List<ReportFilterCondition>
        {
            new ReportFilterCondition { Value = (int)ReportFilterGroupCondition.All, Description = "All of the following are true", Unary = true },
            new ReportFilterCondition { Value = (int)ReportFilterGroupCondition.Any, Description = "Any of the following are true", Unary = true },
            new ReportFilterCondition { Value = (int)ReportFilterGroupCondition.None, Description = "None of the following are true", Unary = true },
        };

        public List<ReportFilter> Filters { get; set; } = new List<ReportFilter>();

        public override async Task<bool> Validate(ReportSchema schema, IServiceProvider services)
        {
            if (!await base.Validate(schema, services))
                return false;

            foreach (var filter in Filters) {
                if (!await filter.Validate(schema, services))
                    filter.Id = Guid.Empty;
            }
            Filters.RemoveAll(f => f.Id == Guid.Empty);

            Description = $"{ConditionDescription}";
            return true;
        }

        public override async Task<bool> Apply(object context, ReportBuilder builder)
        {
            if (Condition == (int)ReportFilterGroupCondition.Any)
            {
                foreach (var filter in Filters)
                {
                    var result = await filter.Apply(context, builder);
                    if (result == true)
                        return true;
                }
                return false;
            }

            if (Condition == (int)ReportFilterGroupCondition.All)
            {
                foreach (var filter in Filters)
                {
                    var result = await filter.Apply(context, builder);
                    if (result == false)
                        return false;
                }
                return true;
            }

            if (Condition == (int)ReportFilterGroupCondition.None)
            {
                foreach (var filter in Filters)
                {
                    var result = await filter.Apply(context, builder);
                    if (result == true)
                        return false;
                }
                return true;
            }

            throw new Exception($"Unrecognised group condition {Condition}");
        }

        public override void Update(ReportFilter filter, ReportSchema schema)
        {
            if (filter is not ReportFilterGroup typed)
                throw new Exception($"Cannot apply filter of type {filter.FilterType} to filter of type {FilterType}");

            base.Update(filter, schema);
        }

        // Serialization

        public override bool ReadProperty(string propertyName, ref Utf8JsonReader reader)
        {
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
                    reader.Read();
                }
                return reader.Read();
            }

            return base.ReadProperty(propertyName, ref reader);
        }

        public override void WriteProperties(Utf8JsonWriter writer)
        {
            base.WriteProperties(writer);
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
